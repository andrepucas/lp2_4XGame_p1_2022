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
    [SerializeField] private UIPanelGameplay _gameplay;
    [SerializeField] private UIPanelMapDisplay _mapDisplay;
    [SerializeField] private UIPanelInspector _inspector;
    [SerializeField] private UIPanelAnalytics _analytics;

    private Color _bgColor;

    public void Initialize()
    {
        // Hide background.
        _bgColor = _background.color;
        _bgColor.a = 0;
        _background.color = _bgColor;
    }

    private void SetupAllPanels()
    {
        _preStart.SetupPanel();
        _mapBrowser.SetupPanel();
        _gameplay.SetupPanel();
        _mapDisplay.SetupPanel();
        _inspector.SetupPanel();
        _analytics.SetupPanel();
    }

    public void ChangeUIState(UIStates p_uiState)
    {
        switch(p_uiState)
        {
            case UIStates.PRE_START:

                SetupAllPanels();
                StartCoroutine(StartDelayAndReveal());

                break;

            case UIStates.MAP_BROWSER:

                _preStart.ClosePanel();
                _mapBrowser.OpenPanel(_panelTransitionTime);

                break;

            case UIStates.LOAD_MAP:

                _mapBrowser.ClosePanel();

                break;

            case UIStates.DISPLAY_MAP:

                _gameplay.OpenPanel(_panelTransitionTime);
                _mapDisplay.OpenPanel(_panelTransitionTime);

                break;

            case UIStates.INSPECTOR:

                _gameplay.ClosePanel();
                _inspector.OpenPanel();

                break;

            case UIStates.ANALYTICS:

                _gameplay.ClosePanel();
                _analytics.OpenPanel();

                break;

            case UIStates.RESUME_FROM_INSPECTOR:

                _inspector.ClosePanel(_panelTransitionTime);
                _gameplay.OpenPanel(_panelTransitionTime);

                break;

            case UIStates.RESUME_FROM_ANALYTICS:

                _analytics.ClosePanel(_panelTransitionTime);
                _gameplay.OpenPanel(_panelTransitionTime);

                break;
        }
    }

    public void UpdateAnalyticsData(int p_index, MapData p_mapData)
        => _analytics.UpdateData(p_index, p_mapData);

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
