using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    private IUserInterface _userInterface;

    private void Awake()
    {
        // Can't create panels user interface with the new keyword, since it
        // extends mono behaviour.
        _userInterface = FindObjectOfType<PanelsUserInterface>();
        _userInterface.Initialize();
    }

    private void OnEnable()
    {
        UIPanelPreStart.PromptRevealed += () => StartCoroutine(WaitForPreStartKey());
    }

    private void OnDisable()
    {
        UIPanelPreStart.PromptRevealed -= () => StopCoroutine(WaitForPreStartKey());
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

            case GameStates.GAME:
                // Game things.
                break;
        }
    }

    private IEnumerator WaitForPreStartKey()
    {
        while (!Input.anyKey) yield return null;

        ChangeGameState(GameStates.MAP_BROWSER);
    }
}
