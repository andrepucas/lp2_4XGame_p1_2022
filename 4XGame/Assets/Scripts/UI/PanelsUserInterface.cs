using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelsUserInterface : MonoBehaviour, IUserInterface
{
    [Header("On Start")]
    [SerializeField][Range(0, 3)] private float _bgRevealTime;
    [SerializeField] private Image _background;
    [Header("Panels")]
    [SerializeField][Range(0, 1)] private float _panelTransitionTime;
    [SerializeField] private UIPanelPreStart _preStart;
    [SerializeField] private UIPanelMapBrowser _mapBrowser;

    private Color _bgColor;

    public void Initialize()
    {
        // Hide background.
        _bgColor = _background.color;
        _bgColor.a = 0;
        _background.color = _bgColor;

        // Setup all panels.
        _preStart.SetupPanel();
        _mapBrowser.SetupPanel();
    }

    public void ChangeUIState(UIStates p_uiState)
    {
        switch(p_uiState)
        {
            case UIStates.PRE_START:
                StartCoroutine(StartDelayAndReveal());
                break;

            case UIStates.MAP_BROWSER:
                _preStart.ClosePanel();
                _mapBrowser.OpenPanel(_panelTransitionTime);
                break;

            case UIStates.GAME:
                _mapBrowser.ClosePanel();
                break;
        }
    }

    private IEnumerator StartDelayAndReveal()
    {
        float m_startTime = Time.time;
        float m_elapsedTime = 0;

        // Reveal background.
        while (_background.color.a < 1)
        {
            _bgColor.a = Mathf.Lerp(0, 1, (m_elapsedTime/_bgRevealTime));
            _background.color = _bgColor;

            m_elapsedTime = Time.time - m_startTime;
            yield return null;
        }

        // Reveal Pre Start Panel.
        _preStart.OpenPanel(_panelTransitionTime);
    }
}
