using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIPanelPreStart : UIPanel
{
    public static event Action OnPromptRevealed;
    
    [SerializeField] private TMP_Text _prompt;
    [SerializeField][Range(0, 1)] private float _fadeTime;

    public void SetupPanel()
    {
        ClosePanel();

        _prompt.alpha = 0;
    }

    public void OpenPanel(float p_transitionTime = 0)
    {
        base.Open(p_transitionTime);
        StartCoroutine(PromptFadeIn(p_transitionTime));
    }

    public void ClosePanel(float p_transitionTime = 0)
    {
        base.Close(p_transitionTime);
    }

    private IEnumerator PromptFadeIn(float p_revealTime)
    {
        float m_elapsedTime = 0;

        yield return new WaitForSecondsRealtime(p_revealTime);

        OnPromptRevealed?.Invoke();

        while (_prompt.alpha < 1)
        {
            _prompt.alpha = Mathf.Lerp(0, 1, (m_elapsedTime/_fadeTime));

            m_elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        _prompt.alpha = 1;
    }
}
