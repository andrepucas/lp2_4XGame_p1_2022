using System;
using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private MapDisplay _mapDisplay;

    private IUserInterface _userInterface;
    private MapData _selectedMap;
    private GameStates _currentState;
    private float _screenWidth;
    private float _screenHeight;
    private bool _isMapDisplayed;
    private bool _isAnalytics;

    private void Awake()
    {
        // Can't create panels user interface with the new keyword, since it
        // extends mono behaviour.
        _userInterface = FindObjectOfType<PanelsUserInterface>();
        _userInterface.Initialize();
        _mapDisplay.Initialize();
    }

    private void OnEnable()
    {
        UIPanelPreStart.OnPromptRevealed += () => StartCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData += SaveMap;
        UIPanelGameplay.OnRestart += () => ChangeGameState(GameStates.PRE_START);
        _mapDisplay.OnMapGenerated += () => ChangeGameState(GameStates.GAMEPLAY);
        MapCell.OnInspect += () => ChangeGameState(GameStates.PAUSE);
    }

    private void OnDisable()
    {
        UIPanelPreStart.OnPromptRevealed -= () => StopCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData -= SaveMap;
        UIPanelGameplay.OnRestart -= () => ChangeGameState(GameStates.PRE_START);
        _mapDisplay.OnMapGenerated -= () => ChangeGameState(GameStates.GAMEPLAY);
        MapCell.OnInspect -= () => ChangeGameState(GameStates.PAUSE);
    }

    private void Start()
    {
        ChangeGameState(GameStates.PRE_START);
    }

    private void ChangeGameState(GameStates p_gameState)
    {
        _currentState = p_gameState;
        
        switch (_currentState)
        {
            case GameStates.PRE_START:

                _isMapDisplayed = false;
                _userInterface.ChangeUIState(UIStates.PRE_START);

                break;

            case GameStates.MAP_BROWSER:

                _userInterface.ChangeUIState(UIStates.MAP_BROWSER);

                break;

            case GameStates.LOAD_MAP:

                _userInterface.ChangeUIState(UIStates.LOAD_MAP);

                _mapDisplay.transform.localPosition = Vector3.zero;
                _mapDisplay.transform.localScale = Vector3.one;

                _screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
                _screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;

                _selectedMap.LoadGameTilesInfo();
                _mapDisplay.GenerateMap(_selectedMap);

                break;

            case GameStates.GAMEPLAY:

                if (!_isMapDisplayed)
                {
                    _userInterface.ChangeUIState(UIStates.DISPLAY_MAP);
                    _isMapDisplayed = true;
                }

                else if (_isAnalytics)
                {
                    _isAnalytics = false;
                    _userInterface.ChangeUIState(UIStates.RESUME_FROM_ANALYTICS);
                }

                else _userInterface.ChangeUIState(UIStates.RESUME_FROM_INSPECTOR);

                break;

            case GameStates.PAUSE:

                if (_isAnalytics) _userInterface.ChangeUIState(UIStates.ANALYTICS);

                else _userInterface.ChangeUIState(UIStates.INSPECTOR);

                break;
        }
    }

    private void FixedUpdate()
    {
        // Input for Gameplay (when the Map is displayed and controllable).
        if (_currentState == GameStates.GAMEPLAY)
        {
            // Try to move map left.
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                _mapDisplay.TryMove(Vector2.left);

            // Try to move map right.
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                _mapDisplay.TryMove(Vector2.right);

            // Try to move map up.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                _mapDisplay.TryMove(Vector2.up);

            // Try to move map down.
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                _mapDisplay.TryMove(Vector2.down);

            // Try to zoom in.
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Plus))
                _mapDisplay.TryZoom(1);

            // Try to zoom out.
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Minus))
                _mapDisplay.TryZoom(-1);
        }
    }

    private void Update()
    {
        // Input for the inspect menu.
        if (_currentState == GameStates.PAUSE)
        {
            // Back out from inspect menu.
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
            {
                ChangeGameState(GameStates.GAMEPLAY);
            }
        }
    }

    private void SaveMap(MapData p_map)
    {
        _selectedMap = p_map;
        ChangeGameState(GameStates.LOAD_MAP);
    }

    private IEnumerator WaitForPreStartKey()
    {
        while (!Input.anyKey) yield return null;

        ChangeGameState(GameStates.MAP_BROWSER);
    }

    // LINQ BUTTONS
    public void EnableAnalyticsButton(int p_index)
    {
        _isAnalytics = true;
        ChangeGameState(GameStates.PAUSE);

        _userInterface.UpdateAnalyticsData(p_index, _selectedMap);
    }
}
