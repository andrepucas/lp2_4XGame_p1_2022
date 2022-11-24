using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridDisplay : MonoBehaviour
{
    /// <summary>
    /// Variable that stores the max cell size.
    /// </summary>
    private const float MAX_Y_SIZE = 15f;
    private const float MAX_X_SIZE = 30f;

    /// <summary>
    /// Reference to the grid where all the tiles will be visualized. 
    /// </summary>
    private Grid grid;

    /// <summary>
    /// Reference to grid layout group component in current object.
    /// </summary>
    private GridLayoutGroup gridLayout;

    /// <summary>
    /// A list that stores all the randomly generated game tiles.
    /// </summary>
    private List<GameTile> generatedTiles;

    /// <summary>
    /// Variable that stores the game object that represents a desert cell.
    /// </summary>
    [SerializeField] private Button desertCell;

    /// <summary>
    /// Variable that stores the game object that represents a hills cell.
    /// </summary>
    [SerializeField] private Button hillsCell;

    /// <summary>
    /// Variable that stores the game object that represents a mountain cell.
    /// </summary>
    [SerializeField] private Button mountainCell;

    /// <summary>
    /// Variable that stores the game object that represents a plains cell.
    /// </summary>
    [SerializeField] private Button plainsCell;

    /// <summary>
    /// Variable that stores the game object that represents a water cell.
    /// </summary>
    [SerializeField] private Button waterCell;


    // NECESSARY CODE BECAUSE OF MISSING FILE READING FEATURE
    private int random;
    // ----------------------------------------------

    /// <summary>
    /// Method that runs at the start of the program.
    /// </summary>
    private void Start()
    {
        /// <summary>
        /// Instantiates a list.
        /// </summary>
        /// <typeparam name="GameTile"></typeparam>
        /// <returns></returns>
        generatedTiles = new List<GameTile>();

        /// <summary>
        /// Instantiates a grid with the randomly generated tile sizes.
        /// </summary>
        /// <returns></returns>
        grid = new Grid(10, 5); // MUDAR AQUI PARA VARIAVEIS DEPOIS

        // NECESSARY CODE BECAUSE OF MISSING FILE READING FEATURE
        for (int i = 0; i < (grid.X * grid.Y); i++)
        {
            random = Random.Range(1, 6);

            switch (random)
            {
                case 1:
                    generatedTiles.Add(new DesertTile());
                    break;

                case 2:
                    generatedTiles.Add(new HillsTile());
                    break;

                case 3:
                    generatedTiles.Add(new MountainTile());
                    break;

                case 4:
                    generatedTiles.Add(new PlainsTile());
                    break;

                case 5:
                    generatedTiles.Add(new WaterTile());
                    break;
                default:
                    Debug.Log("You messed up");
                    break;
            }
        }
        // -----------------------------------------------

        /// <summary>
        /// Variable that stores the specified component.
        /// </summary>
        /// <typeparam name="GridLayoutGroup"></typeparam>
        /// <returns></returns>
        gridLayout = GetComponent<GridLayoutGroup>();

        // Creates the visual representation of the game tile grid.
        CreateVisualGrid(grid);
    }

    /// <summary>
    /// Creates the visual representation of the game tile grid.
    /// </summary>
    /// <param name="grid"></param>
    private void CreateVisualGrid(Grid grid)
    {
        // DEBUG CODE
        // int dNumber = 0;
        // int hNumber = 0;
        // int mNumber = 0;
        // int pNumber = 0;
        // int wNumber = 0;

        // Instantiates a vector2 - Need help explaining this part
        Vector2 newCellSize;

        // Calculates bla bla bla
        newCellSize.y = MAX_Y_SIZE / grid.Y;
        newCellSize.x = MAX_X_SIZE / grid.X;

        // DEBUG CODE
        // Debug.Log(newCellSize);

        // Updates the component's cell size to match the calculated cell size.
        gridLayout.cellSize = newCellSize;

        // Updates the components column count to match the grid's X dimentions.
        gridLayout.constraintCount = grid.X;

        // Goes through each game tile in the list.
        foreach (GameTile tile in generatedTiles)
        {
            // Checks the Type of each tile.
            switch (tile.Type)
            {
                // If the current game tile type is Desert.
                case TileType.Desert:

                    // Instantiates a Desert Cell game object as a child of the
                    // current game object.
                    Instantiate(desertCell, gameObject.transform);

                    // DEBUG CODE
                    // Debug.Log("Desert");
                    // dNumber++;
                    break;

                // If the current game tile type is Hills.
                case TileType.Hills:

                    // Instantiates a Hills Cell game object as a child of the
                    // current game object.
                    Instantiate(hillsCell, gameObject.transform);

                    // DEBUG CODE
                    // Debug.Log("Hills");
                    // hNumber++;
                    break;

                // If the current game tile type is Mountain.
                case TileType.Mountain:

                    // Instantiates a Mountain Cell game object as a child of 
                    // the current game object.
                    Instantiate(mountainCell, gameObject.transform);

                    // DEBUG CODE
                    // Debug.Log("Mountain");
                    // mNumber++;
                    break;

                // If the current game tile type is Plains.
                case TileType.Plains:

                    // Instantiates a Plains Cell game object as a child of the
                    // current game object.
                    Instantiate(plainsCell, gameObject.transform);

                    // DEBUG CODE
                    // Debug.Log("Plains");
                    // pNumber++;
                    break;

                // If the current game tile type is Water.
                case TileType.Water:

                    // Instantiates a Water Cell game object as a child of the
                    // current game object.
                    Instantiate(waterCell, gameObject.transform);

                    // DEBUG CODE
                    // Debug.Log("Water");
                    // wNumber++;
                    break;
            }
        }
        // DEBUG CODE
        // Debug.Log("Number of Desert Tiles:");
        // Debug.Log(dNumber);

        // Debug.Log("Number of Hills Tiles:");
        // Debug.Log(hNumber);

        // Debug.Log("Number of Mountain Tiles:");
        // Debug.Log(mNumber);

        // Debug.Log("Number of Plains Tiles:");
        // Debug.Log(pNumber);

        // Debug.Log("Number of Water Tiles:");
        // Debug.Log(wNumber);

        // DEBUG CODE
        // Debug.Log(generatedTiles[0]);
        // Debug.Log(generatedTiles[1]);
        // Debug.Log(generatedTiles[2]);
        // Debug.Log(generatedTiles[3]);
        // Debug.Log(generatedTiles[4]);
        // Debug.Log(generatedTiles[10]);
    }
}
