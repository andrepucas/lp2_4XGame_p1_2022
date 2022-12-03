# 4X GAME

In this phase, **4X Game** is a Unity 2020.3 LTS game that allows to generate,
manipulate and pick a `.map4x` file from the Desktop to be loaded and displayed
as an interactive game map.

[**`• CODE ARCHITECTURE •`**](#code-architecture) [**`• METADATA •`**](#metadata)

---

## The Game Map

![Game Map](Images/map.png "40x20 Game Map")

Represented by a grid of squared tiles, each visually composed by a terrain with
zero to six different resource types, all of which are clickable, displaying its
properties: **Terrain, Resources, Coin** and **Food**.

For bigger maps, zooming and panning is also made available, through typical
keyboard binds. Furthermore, there are 5 numbered buttons (1-5) at the top,
used to display map statistics.

### Terrains

![Terrains](Images/terrains_all.png "The 5 terrain types")

There are **5 terrain types**, all visibly distinct, represented by two color
tones only. As the foundation of each game tile, terrains are responsible for
establishing its base **Coin** and **Food** values.

### Resources

![Resources](Images/resources_all.png "The 6 resource types, across all terrains")

There are **6 resource types**, also visibly distinct, but not only from
each other. On some cases they will vary depending on the terrain they're on.
One terrain can have up to 6 resources, each with  **Coin** and **Food** values,
which stack.

Below are the pre-defined **Coin** and **Food** values for each resource:

![Water Resources](Images/water_resources_all.png "All 6 resources values")

And here's a practical example of a game tile with 4 resources:

![Inspector](Images/inspector_close.png "Desert tile with 4 resources")

---

## Code Architecture

The game is structured around 5 Main Sections, managed by the [`Controller`].

[**`• PRE-START •`**](#pre-start) [**`• MAP BROWSER •`**](#map-browser) [**`• MAP DISPLAY •`**](#map-display) [**`• INSPECTOR •`**](#inspector) [**`• ANALYTICS •`**](#analytics)

---

### Controller

Manages the game, handling its [`GameStates`] and Player Input. It starts by
setting the `CurrentState` to `Pre Start`, which in turn makes the
[`PanelsUserInterface`] update it's display, sending it the respective
[`UIStates`]. These are fairly similar to [`GameStates`], but more specific. For
example, the `Inspector` and `Analytics` UI states, which are both `Pause` game
states, have different internal behaviours.

The `CurrentState` is updated through subscribed events, meaning other classes
don't need a [`Controller`] reference.

### User Interface

The game's [`PanelsUserInterface`] (which implements [`IUserInterface`]) focuses on
enabling and disabling single responsibility panels, visually reflecting the
current game section.

In turn, each panel handles their respective game behaviours,
and raises events when the `CurrentState` needs to be updated. All panels extend
an abstract [`UI Panel`], which contains the opening and closing panel behaviours.

---

### Pre-start

![Pre-Start](Images/pre_start.png "Pre-start screen")

In this [`panel`][PreStartPanel], an event is raised when the input prompt
"Press any key to start" is revealed. This event is subscribed by the
[`Controller`], which in turn starts a coroutine that will update the GameState
to `MapBrowser` after any key is pressed.

### Map Browser

![Map Browser](Images/map_browser.png "Map Browser screen")

The map browser panel displays all existing map files inside the Desktop's
'maps4xfiles' folder in a scrollable menu.*

It starts by using the static MapFilesBrowser class to create a Map Data
instance for each file, and return them. A MapData instance, at this initial
stage, contains a string array with all the file lines, a name (that matches
the file, without the extension), and the X and Y map dimensions, which are read
right away from the first file line.

For each MapData returned, a MapFileWidget is instantiated, serving as its visual
representation while referencing it, displaying the map's name and dimensions. The displayed name can be edited by the player, which internally updates the
MapData and file's name as well. Because the file name is editable, cautions have to be taken to not allow for illegal path characters, verified by the static MapFileNameValidator, which replaces illegal characters for `_` using [Regex], or duplicate names, which is verified by the MapFilesBrowser and adds a `_N` to the duplicate file.

After the MapFileWidgets are instantiated, a MapFileGeneratorWidget is instantiated at the end of the list, allowing direct map files creation, using [Nuno Fachada's Map Generator][Generator].

> Regarding the Map Generator's code, we've made only 2 small adjustments:
>
> + Increased the chance of generating resources from 0.3 to 0.5, to generate more diverse maps.
> + Fixed a small bug that prevented small maps (with x * y > 10) from being generated and made slightly larger maps have only one or 2 terrains.
>
> ```c#
> int totalTiles = rows * cols;
>
> int numCenterPoints = (totalTiles > 50)
>    ? (int)(totalTiles * centerPointsDensity) 
>    : (int)(totalTiles * centerPointsDensity * (100/totalTiles));
> ```

When the Load Button is pressed, an event is raised with the selected map (yellow outline), causing the controller to change its GameState to LoadMap.

\* The scrollable menu was originally using the Unity UI Element Scroll Rect component, however due to a mouse scroll wheel bug, is now using UpgradedScrollRect, a custom extension.

#### Load Map

Before being ready to display, the MapData has to convert its array of lines to GameTiles, a class that represents that map's position terrain, which may or not hold Resources.

This is done by iterating all lines, starting at the second (the first line held the map dimensions, which have already been handled, when MapData was instantiated). In each line, it starts by looking for a `#`, by trying to get its index. If its greater than 0, then that line has a comment that needs to be ignored, using a substring.

```c#
m_commentIndex = m_line.IndexOf("#");
if (m_commentIndex >= 0) m_line = m_line.Substring(0, m_commentIndex);
```

It then splits the line into an array of strings, each being a keyword. The first will always represent a Terrain, so it's compared with Terrain keywords and instantiates a GameTile accordingly, adding it to this Maps GameTiles List.

```c#
case "desert":
    GameTiles.Insert(i - 1, new DesertTile()); 
    break;
```

If there are any other words in the string array, they are resources to add to the GameTile we just instantiated. Again, each keyword is compared with Resources keywords, and added accordingly.

```c#
if (m_lineStrings.Length > 0)
    for (int s = 1; s < m_lineStrings.Length; s++)
        switch (m_lineStrings[s])
        {
            case "plants":
                GameTiles[i - 1].AddResource(new PlantsResource());
                break;

            (...)
```

### Map Display

![Map Display](Images/map_display.png "Map Display screen")

Now that MapData has a GameTiles list and is ready to be loaded, the controller sends it to MapDisplay, responsible for generating the map.

The map is generated using the Grid Layout and Content Size Fitter components. The only adjustments needed is setting the Grid Layout's cell size and the column count constraint, both calculated with the map dimensions.

```c#
m_newCellSize.y = MAX_Y_SIZE / p_map.Dimensions_Y;
m_newCellSize.x = MAX_X_SIZE / p_map.Dimensions_X;

// Sets both the X and Y to the lowest value out of the 2, making a square.
if (m_newCellSize.y < m_newCellSize.x) m_newCellSize.x = m_newCellSize.y;
else m_newCellSize.y = m_newCellSize.x;

_cellSize = m_newCellSize.x;

// Constraints the grid layout group to a max of X columns.
_gridLayout.constraintCount = p_map.Dimensions_X;
```

With the grid set, it then iterates every GameTile in the MapData's list and instantiates a MapCell prefab for each. A MapCell represents a game tile visually, only holding its terrain and resources sprites.

Once all are instantiated, MapDisplay raises an event that makes the controller tell the UserInterface that it can now enable the MapDisplayPanel, making the map visible, and disables the Grid Layout and Content Size Fitter components, boosting performance by reducing automatic component calls.

In this state, the map can be moved and zoomed in/out, using the key binds shown on the bottom of the screen. The player's input is handled by the controller, who then passes the directional info to the MapDisplay that tries to move the map using the Rect Transform's pivot. Using the pivot to move is more performant heavy, but allows for centered zooming in and out when scaled.

Each MapCell can also be hovered and clicked by the mouse, using Unity's Event Trigger component, updating its base sprite to look hovered and raising two events when clicked, one triggers the controller to display the inspector menu, the other send out the info needed to inspect.

### Inspector

![Inspector](Images/inspector.png "Inspector screen")

This panel is responsible for displaying the selected map cell's properties. It does so by syncing its name, coin and food values, and terrain and resources sprites with the clicked tile's. Furthermore, it also enables text components accordingly to the shown resources, to add extra written info. This written info is equal for every tile, since the Coin and Food values of resources and terrains are constant. The only values that vary are the tile's sum.

Merely a "show" type of panel, when the user presses escape or clicks away, the controller updates its state back to Gameplay.

### Analytics

![Analytics](Images/analytics.png "Analytics screen")

The analytics panel is triggered by clicking any of the numbered buttons on the top of the screen. When clicked, an event on the button itself is raised, containing the button index, which makes the controller display this panel and displaying info according to the index.

> The panel itself, originally wasn't supposed to show anything, but we've decided to implement the suggested LINQ template functionalities. As such, pressing each of the buttons will display:
>
> + Number of tiles without resources.
> + Coin average in mountain tiles.
> + List of existing terrains, alphabetically.
> + The terrain, resources and coordinates of the tile with the least Coin value.
> + Number of unique tiles.

---

### UML Diagram

![UML](Images/uml.png "UML Diagram")

## References

+ [Regex Class - Microsoft Docs][Regex]
+ [4X Map Generator - Nuno Fachada][Generator]
+ [Zombies vs Humanos - Nuno Fachada]
+ [Virus Simulation - Afonso Lage e André Santos]

## Metadata

|       [Afonso Lage (a21901381)]      |        [André Santos (a21901767)]         |
|:------------------------------------:|:-----------------------------------------:|
|                                      |                                           |
| `GameTile` & `Resource` + subclasses | Controller with `GameStates` & `UIStates` |
|      Visual Map Grid Generation      |              Files Browser                |
|               Map Cells              |         Map Control (Zoom & Pan)          |
|            Linq Operations           |       `UIPanel` Classes + Visual UI       |
|        XML Documentation (50%)       |          XML Documentation (50%)          |
|              README (50%)            |               README (50%)                |

> Game created as 1st Project for Programming Languages II, 2022/23.  
> Professor: [Nuno Fachada].  
> [Bachelor in Videogames] at [Lusófona University].

[**`• BACK TO TOP •`**](#4x-game)

[`Controller`]:4XGame/Assets/Scripts/Controller.cs
[`GameStates`]:4XGame/Assets/Scripts/Enums/GameStates.cs
[`UIStates`]:4XGame/Assets/Scripts/Enums/UIStates.cs
[`IUserInterface`]:4XGame/Assets/Scripts/UI/IUserInterface.cs
[`PanelsUserInterface`]:4XGame/Assets/Scripts/UI/PanelsUserInterface.cs
[`UI Panel`]:4XGame/Assets/Scripts/UI/Panels/UIPanel.cs
[PreStartPanel]:4XGame/Assets/Scripts/UI/Panels/UIPanelPreStart.cs

[Regex]:https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-7.0
[Generator]:https://github.com/VideojogosLusofona/lp2_2022_p1/tree/main/Generator
[Zombies vs Humanos - Nuno Fachada]:https://github.com/VideojogosLusofona/lp1_2018_p2_solucao
[Virus Simulation - Afonso Lage e André Santos]:https://github.com/AfonsoLage-boop/VirusSim_2020
[Afonso Lage (a21901381)]:https://github.com/AfonsoLage-boop
[André Santos (a21901767)]:https://github.com/andrepucas
[Nuno Fachada]:https://github.com/nunofachada
[Bachelor in Videogames]:https://www.ulusofona.pt/en/undergraduate/videogames
[Lusófona University]:https://www.ulusofona.pt/en/
