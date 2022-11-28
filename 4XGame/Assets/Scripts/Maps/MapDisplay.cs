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

    private const float PAN_SPEED = 0.01f;
    private const float ZOOM_SPEED = 0.01f;
    private const float MIN_ZOOM = 1;
    private const float MAX_ZOOM = 10;

    /// <summary>
    /// Variable that stores the game object that represents a desert cell.
    /// </summary>
    [SerializeField] private GameObject _desertCell;

    /// <summary>
    /// Variable that stores the game object that represents a hills cell.
    /// </summary>
    [SerializeField] private GameObject _hillsCell;

    /// <summary>
    /// Variable that stores the game object that represents a mountain cell.
    /// </summary>
    [SerializeField] private GameObject _mountainCell;

    /// <summary>
    /// Variable that stores the game object that represents a plains cell.
    /// </summary>
    [SerializeField] private GameObject _plainsCell;

    /// <summary>
    /// Variable that stores the game object that represents a water cell.
    /// </summary>
    [SerializeField] private GameObject _waterCell;

    /// <summary>
    /// Reference to grid layout group component in the current object.
    /// </summary>
    private GridLayoutGroup _gridLayout;

    /// <summary>
    /// Reference to self rect transform.
    /// </summary>
    private RectTransform _rectTransform;

    private float _xPivotLimit;
    private float _yPivotLimit;

    /// <summary>
    /// Called by controller on Awake, gets grid layout reference.
    /// </summary>
    public void Initialize()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Generates and instantiates the game map.
    /// </summary>
    /// <param name="grid"></param>
    public void GenerateMap(MapData p_map)
    {
        Vector2 m_newCellSize;
        GridCell m_GridCell = null;

        _xPivotLimit = 1 / (float)(p_map.Dimensions_X * 2);
        _yPivotLimit = 1 / (float)(p_map.Dimensions_Y * 2);

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
            switch (tile)
            {
                case DesertTile:

                    // Instantiates a Desert Cell as a child of this game object.
                    m_GridCell = Instantiate(_desertCell, transform).GetComponent<GridCell>();
                    break;

                case HillsTile:

                    // Instantiates a Hills Cell as a child of this game object.
                    m_GridCell = Instantiate(_hillsCell, transform).GetComponent<GridCell>();
                    break;

                case MountainTile:

                    // Instantiates a Mountain Cell as a child of this game object.
                    m_GridCell = Instantiate(_mountainCell, transform).GetComponent<GridCell>();
                    break;

                case PlainsTile:

                    // Instantiates a Plains Cell as a child of this game object.
                    m_GridCell = Instantiate(_plainsCell, transform).GetComponent<GridCell>();
                    break;

                case WaterTile:

                    // Instantiates a Water Cell as a child of this game object.
                    m_GridCell = Instantiate(_waterCell, transform).GetComponent<GridCell>();
                    break;
            }

            m_GridCell.Initialize(tile);
        }

        // Send out event that map was generated (to controller).
        OnMapGenerated?.Invoke();
    }

    public void TryMove(Vector2 p_direction)
    {
        if (p_direction == Vector2.down)
        {
            if (_rectTransform.pivot.y >= _yPivotLimit)
            {
                // Move map using it's pivot.
                _rectTransform.pivot += Vector2.down * PAN_SPEED;
                _rectTransform.localPosition = Vector3.zero;

                // Rectify pivot if it goes over the limit.
                if (_rectTransform.pivot.y < _yPivotLimit)
                {
                    Vector2 m_pivot = _rectTransform.pivot;
                    m_pivot.y = _yPivotLimit;
                    _rectTransform.pivot = m_pivot;
                }
            }
        }

        else if (p_direction == Vector2.up)
        {
            if (_rectTransform.pivot.y <= (1 - _yPivotLimit))
            {
                // Move map using it's pivot.
                _rectTransform.pivot += Vector2.up * PAN_SPEED;
                _rectTransform.localPosition = Vector3.zero;

                // Rectify pivot if it goes over the limit.
                if (_rectTransform.pivot.y > (1 - _yPivotLimit))
                {
                    Vector2 m_pivot = _rectTransform.pivot;
                    m_pivot.y = (1 - _yPivotLimit);
                    _rectTransform.pivot = m_pivot;
                }
            }
        }

        else if (p_direction == Vector2.left)
        {
            if (_rectTransform.pivot.y >= _xPivotLimit)
            {
                // Move map using it's pivot.
                _rectTransform.pivot += Vector2.left * PAN_SPEED;
                _rectTransform.localPosition = Vector3.zero;

                // Rectify pivot if it goes over the limit.
                if (_rectTransform.pivot.x < _xPivotLimit)
                {
                    Vector2 m_pivot = _rectTransform.pivot;
                    m_pivot.x = _xPivotLimit;
                    _rectTransform.pivot = m_pivot;
                }
            }
        }

        else if (p_direction == Vector2.right)
        {
            if (_rectTransform.pivot.y <= (1 - _xPivotLimit))
            {
                // Move map using it's pivot.
                _rectTransform.pivot += Vector2.right * PAN_SPEED;
                _rectTransform.localPosition = Vector3.zero;

                // Rectify pivot if it goes over the limit.
                if (_rectTransform.pivot.x > (1 - _xPivotLimit))
                {
                    Vector2 m_pivot = _rectTransform.pivot;
                    m_pivot.x = (1 - _xPivotLimit);
                    _rectTransform.pivot = m_pivot;
                }
            }

            // COMMENTED IN CASE WE NEED TO GO BACK TO MOVING THROUGH LOCAL POS
            // if ((_rectTransform.localPosition.x + ((_rectTransform.rect.width *
            //     _rectTransform.localScale.x - _gridLayout.cellSize.x) / 2)) > 0)
            // {
            //     _rectTransform.localPosition += 
            //         Vector3.right * _rectTransform.localScale.x * PAN_SPEED;
            // }
        }
    }

    public void TryZoom(int p_direction)
    {
        // Zoom in.
        if (p_direction > 0 && _rectTransform.localScale.x < MAX_ZOOM)
            _rectTransform.localScale += _rectTransform.localScale * ZOOM_SPEED;

        // Zoom out.
        else if (p_direction < 0 && _rectTransform.localScale.x > MIN_ZOOM)
            _rectTransform.localScale += _rectTransform.localScale * -ZOOM_SPEED;
    }
}
