using System;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    /// <summary>
    /// Event that is triggered when the map finishes generating.
    /// </summary>
    public event Action OnMapGenerated;

    /// <summary>
    /// Constant value of the max cell size in the Y axis.
    /// </summary>
    private const float MAX_Y_SIZE = 15f;

    /// <summary>
    /// Constant value of the max cell size in the X axis.
    /// </summary>
    private const float MAX_X_SIZE = 30f;

    /// <summary>
    /// Variable that stores the game object that represents a desert cell.
    /// </summary>
    [SerializeField] private Button _desertCell;

    /// <summary>
    /// Variable that stores the game object that represents a hills cell.
    /// </summary>
    [SerializeField] private Button _hillsCell;

    /// <summary>
    /// Variable that stores the game object that represents a mountain cell.
    /// </summary>
    [SerializeField] private Button _mountainCell;

    /// <summary>
    /// Variable that stores the game object that represents a plains cell.
    /// </summary>
    [SerializeField] private Button _plainsCell;

    /// <summary>
    /// Variable that stores the game object that represents a water cell.
    /// </summary>
    [SerializeField] private Button _waterCell;

    /// <summary>
    /// Reference to grid layout group component in the current object.
    /// </summary>
    private GridLayoutGroup _gridLayout;

    /// <summary>
    /// Reference to self rect transform.
    /// </summary>
    public RectTransform RectTransform {get; private set;}

    /// <summary>
    /// Called by controller on Awake, gets grid layout reference.
    /// </summary>
    public void Initialize()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        RectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Generates and instantiates the game map.
    /// </summary>
    /// <param name="grid"></param>
    public void GenerateMap(MapData p_map)
    {
        Vector2 m_newCellSize;

        // Calculate cell size based on the map dimensions, 
        // using the max X and Y cell sizes as references.
        m_newCellSize.y = MAX_Y_SIZE / p_map.Dimensions_Y;
        m_newCellSize.x = MAX_X_SIZE / p_map.Dimensions_X;

        // Set both the X and Y to the lowest value out of the 2, making a square.
        if (m_newCellSize.y < m_newCellSize.x) m_newCellSize.x = m_newCellSize.y;
        else m_newCellSize.y = m_newCellSize.x;

        // Resize grid layout group default cell size.
        _gridLayout.cellSize = m_newCellSize;

        // Constraint the grid layout group to a max of X columns.
        _gridLayout.constraintCount = p_map.Dimensions_X;

        // Iterate every game tile in Map Data.
        foreach (GameTile tile in p_map.GameTiles)
        {
            // Check the Type of each tile.
            switch (tile.Type)
            {
                case TileType.Desert:

                    // Instantiates a Desert Cell as a child of this game object.
                    Instantiate(_desertCell, transform);
                    break;

                case TileType.Hills:

                    // Instantiates a Hills Cell as a child of this game object.
                    Instantiate(_hillsCell, gameObject.transform);
                    break;

                case TileType.Mountain:

                    // Instantiates a Mountain Cell as a child of this game object.
                    Instantiate(_mountainCell, gameObject.transform);
                    break;

                case TileType.Plains:

                    // Instantiates a Desert Cell as a child of this game object.
                    Instantiate(_plainsCell, gameObject.transform);
                    break;

                case TileType.Water:

                    // Instantiates a Desert Cell as a child of this game object.
                    Instantiate(_waterCell, gameObject.transform);
                    break;
            }
        }

        // Send out event that map was generated (to controller).
        OnMapGenerated?.Invoke();
    }
}
