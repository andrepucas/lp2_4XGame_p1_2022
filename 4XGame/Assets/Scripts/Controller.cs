using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    private const float PAN_SPEED = 0.1f;
    private const float SCREEN_DEADZONE = 0.95f;

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
        _mapDisplay.OnMapGenerated += () => ChangeGameState(GameStates.DISPLAY_MAP);
    }

    private void OnDisable()
    {
        UIPanelPreStart.OnPromptRevealed -= () => StopCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData -= SaveMap;
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
            // Move map left.
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                if (_mapDisplay.RectTransform.localPosition.x < _screenWidth * SCREEN_DEADZONE)
                    _mapDisplay.RectTransform.localPosition += Vector3.right * PAN_SPEED;

            // Move map right.
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                if (_mapDisplay.RectTransform.localPosition.x > -_screenWidth * SCREEN_DEADZONE)
                    _mapDisplay.RectTransform.localPosition += Vector3.left * PAN_SPEED;

            // Move map up.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                if (_mapDisplay.RectTransform.localPosition.y > -_screenHeight * SCREEN_DEADZONE)
                    _mapDisplay.RectTransform.localPosition += Vector3.down * PAN_SPEED;

            // Move map down.
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                if (_mapDisplay.RectTransform.localPosition.y < _screenHeight * SCREEN_DEADZONE)
                    _mapDisplay.RectTransform.localPosition += Vector3.up * PAN_SPEED;
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
