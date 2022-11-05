using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridDisplay : MonoBehaviour
{
    private const float MAX_CELL_SIZE = 700f;
    private Grid grid;
    private List<GameTile> generatedTiles;
    private GridLayoutGroup gridLayout;
    [SerializeField] private Button desertCell;
    [SerializeField] private Button hillsCell;
    [SerializeField] private Button mountainCell;
    [SerializeField] private Button plainsCell;
    [SerializeField] private Button waterCell;


    // NECESSARY CODE BECAUSE OF MISSING FILE READING FEATURE
    private int random;
    // ----------------------------------------------

    private void Start()
    {
        generatedTiles = new List<GameTile>();
        grid = new Grid(10, 5);

        // NECESSARY CODE BECAUSE OF MISSING FILE READING FEATURE
        for (int i = 0; i < 50; i++)
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

        gridLayout = GetComponent<GridLayoutGroup>();

        CreateVisualGrid(grid);
    }

    private void CreateVisualGrid(Grid grid)
    {
        // DEBUG CODE
        // int dNumber = 0;
        // int hNumber = 0;
        // int mNumber = 0;
        // int pNumber = 0;
        // int wNumber = 0;

        Vector2 newCellSize;
        newCellSize.y = MAX_CELL_SIZE / grid.Y;
        newCellSize.x = newCellSize.y;

        Debug.Log(newCellSize);

        gridLayout.cellSize = newCellSize;
        gridLayout.constraintCount = grid.X;

        foreach (GameTile tile in generatedTiles)
        {
            switch (tile.Type)
            {
                case TileType.Desert:
                    Debug.Log("Desert");
                    Instantiate(desertCell, gameObject.transform);
                    // dNumber++; -> DEBUG CODE
                    break;

                case TileType.Hills:
                    Debug.Log("Hills");
                    Instantiate(hillsCell, gameObject.transform);
                    // hNumber++; -> DEBUG CODE
                    break;

                case TileType.Mountain:
                    Debug.Log("Mountain");
                    Instantiate(mountainCell, gameObject.transform);
                    // mNumber++; -> DEBUG CODE
                    break;

                case TileType.Plains:
                    Debug.Log("Plains");
                    Instantiate(plainsCell, gameObject.transform);
                    // pNumber++; -> DEBUG CODE
                    break;

                case TileType.Water:
                    Debug.Log("Water");
                    Instantiate(waterCell, gameObject.transform);
                    // wNumber++; -> DEBUG CODE
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
