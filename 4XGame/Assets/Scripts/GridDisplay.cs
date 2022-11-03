using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridDisplay : MonoBehaviour
{

    private const float MAX_CELL_SIZE = 700f;

    private Grid grid;
    private List<GameObject> parcels;
    private GridLayoutGroup gridLayout;
    [SerializeField] private GameObject gridParcel;

    private void Start()
    {
        parcels = new List<GameObject>();
        grid = new Grid(10, 5);

        gridLayout = GetComponent<GridLayoutGroup>();

        CreateVisualGrid(grid);
    }

    private void CreateVisualGrid(Grid grid)
    {
        Vector2 newCellSize;
        newCellSize.y = MAX_CELL_SIZE / grid.Y;
        newCellSize.x = newCellSize.y;

        Debug.Log(newCellSize);

        gridLayout.cellSize = newCellSize;
        gridLayout.constraintCount = grid.X;

        for (int i = 0; i < grid.Y; i++)
        {

            for (int d = 0; d < grid.X; d++)
            {
                Instantiate(gridParcel, gameObject.transform);

                // parcels.Add(currentParcel);

            }
        }
    }

}
