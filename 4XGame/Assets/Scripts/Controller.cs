using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    [SerializeField] private MapDisplay _mapDisplay;

    private IUserInterface _userInterface;
    private MapData _selectedMap;
    private GameStates _currentState;
    private float _screenWidth;
    private float _screenHeight;

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
        _mapDisplay.OnMapGenerated += () => ChangeGameState(GameStates.DISPLAY_MAP);
    }

    private void OnDisable()
    {
        UIPanelPreStart.OnPromptRevealed -= () => StopCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData -= SaveMap;
        UIPanelGameplay.OnRestart -= () => ChangeGameState(GameStates.PRE_START);
        _mapDisplay.OnMapGenerated -= () => ChangeGameState(GameStates.DISPLAY_MAP);
    }

    private void Start()
    {
        ChangeGameState(GameStates.PRE_START);
    }

    private void FixedUpdate()
    {
        if (_currentState == GameStates.DISPLAY_MAP)
        {
            if (!Input.anyKey) return;

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
            else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Minus))
                _mapDisplay.TryZoom(-1);
        }
    }

    private void ChangeGameState(GameStates p_gameState)
    {
        _currentState = p_gameState;
        
        switch (_currentState)
        {
            case GameStates.PRE_START:

                _userInterface.ChangeUIState(UIStates.PRE_START);
                break;

            case GameStates.MAP_BROWSER:

                _userInterface.ChangeUIState(UIStates.MAP_BROWSER);
                break;

            case GameStates.LOAD_MAP:

                _mapDisplay.transform.localPosition = Vector3.zero;
                _mapDisplay.transform.localScale = Vector3.one;

                _screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
                _screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;

                _userInterface.ChangeUIState(UIStates.LOAD_MAP);
                _selectedMap.LoadGameTilesInfo();
                _mapDisplay.GenerateMap(_selectedMap);

                foreach (GameTile f_tile in _selectedMap.GameTiles)
                    Debug.Log(f_tile);

                break;

            case GameStates.DISPLAY_MAP:

                _userInterface.ChangeUIState(UIStates.DISPLAY_MAP);
                break;

            // case GameStates.PAUSE:
                // something;
                // break;
        }
    }

    private IEnumerator WaitForPreStartKey()
    {
        while (!Input.anyKey) yield return null;

        ChangeGameState(GameStates.MAP_BROWSER);
    }

    private void SaveMap(MapData p_map)
    {
        _selectedMap = p_map;
        ChangeGameState(GameStates.LOAD_MAP);
    }
}
