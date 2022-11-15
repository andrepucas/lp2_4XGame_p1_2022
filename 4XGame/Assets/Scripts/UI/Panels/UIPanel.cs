using UnityEngine;
using System.Collections;

public abstract class UIPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    protected void Open(float p_transition)
    {
        gameObject.SetActive(true);

        if (p_transition == 0)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        else StartCoroutine(RevealOverTime(p_transition));
    }

    protected void Close(float p_transition)
    {
        _canvasGroup.blocksRaycasts = false;

        if (p_transition == 0)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }

        else StartCoroutine(HideOverTime(p_transition));
    }

    private IEnumerator RevealOverTime(float p_transition)
    {
        float m_elapsedTime = 0;

        // Set as interactable right away, to display correct child UI colors.
        _canvasGroup.interactable = true;

        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha = Mathf.Lerp(0, 1, (m_elapsedTime/p_transition));

            m_elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator HideOverTime(float p_transition)
    {
        float m_startTime = Time.unscaledTime;
        float m_elapsedTime = 0;

        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, (m_elapsedTime/p_transition));

            m_elapsedTime = Time.unscaledTime - m_startTime;

            yield return null;
        }

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        gameObject.SetActive(false);
    }
}
