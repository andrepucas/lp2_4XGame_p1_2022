using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    private IUserInterface _userInterface;
    private MapData _selectedMap;

    private void Awake()
    {
        // Can't create panels user interface with the new keyword, since it
        // extends mono behaviour.
        _userInterface = FindObjectOfType<PanelsUserInterface>();
        _userInterface.Initialize();
    }

    private void OnEnable()
    {
        UIPanelPreStart.OnPromptRevealed += () => StartCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData += SaveMap;
    }

    private void OnDisable()
    {
        UIPanelPreStart.OnPromptRevealed -= () => StopCoroutine(WaitForPreStartKey());
        UIPanelMapBrowser.OnSendMapData -= SaveMap;
    }

    private void Start()
    {
        ChangeGameState(GameStates.PRE_START);
    }

    private void ChangeGameState(GameStates p_gameState)
    {
        switch (p_gameState)
        {
            case GameStates.PRE_START:

                _userInterface.ChangeUIState(UIStates.PRE_START);
                break;

            case GameStates.MAP_BROWSER:

                _userInterface.ChangeUIState(UIStates.MAP_BROWSER);
                break;

            case GameStates.LOAD_GAME:

                _userInterface.ChangeUIState(UIStates.LOAD_GAME);
                _selectedMap.LoadGameTilesInfo();

                // THIS DEBUG ONLY WORKS WHEN CREATE RESOURCES IS DISABLED IN ALL GAME TILES //
                // int m_count = 0;
                
                // foreach (GameTile f_tile in _selectedMap.GameTiles)
                // {
                //     Debug.Log("// GAME TILE " + m_count + ": " + f_tile.Type.ToString() + " //");

                //     foreach (Resource f_resource in f_tile.Resources)
                //         Debug.Log("\tR: " + f_resource.Name);

                //     m_count++;
                // }

                break;

            // case GameStates.GAMEPLAY:
                // something;
                // break;

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
        ChangeGameState(GameStates.LOAD_GAME);
    }
}
