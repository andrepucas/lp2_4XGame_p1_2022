# 4X GAME

In this phase, **4X Game** is a Unity 2020.3 LTS game that allows to generate,
manipulate and pick a `.map4x` file from the Desktop to be loaded and displayed
as an interactive game map.

[`METADATA`](#metadata) [`CODE ARCHITECTURE`](#code-architecture)

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

![Inspector](Images/inspector.png "Desert tile with 4 resources")

[`METADATA`](#metadata) [`BACK TO TOP`](#4x-game)

---

## Code Architecture

The game code is structured around the [`Controller`] class which manages
[`GameStates`] and the player input, accordingly.

### UML Diagram

![UML](Images/uml.png "UML Diagram")

## References

* [Zombies vs Humanos - Nuno Fachada]
* [Virus Simulation - Afonso Lage e André Santos]

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

[`BACK TO TOP`](#4x-game) [`CODE ARCHITECTURE`](#code-architecture)

[`Controller`]:4XGame/Assets/Scripts/Controller.cs
[`GameStates`]:4XGame/Assets/Scripts/Enums/GameStates.cs

[Zombies vs Humanos - Nuno Fachada]:https://github.com/VideojogosLusofona/lp1_2018_p2_solucao
[Virus Simulation - Afonso Lage e André Santos]:https://github.com/AfonsoLage-boop/VirusSim_2020
[Afonso Lage (a21901381)]:https://github.com/AfonsoLage-boop
[André Santos (a21901767)]:https://github.com/andrepucas
[Nuno Fachada]:https://github.com/nunofachada
[Bachelor in Videogames]:https://www.ulusofona.pt/en/undergraduate/videogames
[Lusófona University]:https://www.ulusofona.pt/en/
