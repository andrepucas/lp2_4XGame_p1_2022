using System;

public class UIPanelGameplay : UIPanel
{
    // Events.
    public static event Action OnRestart;
    
    public void SetupPanel()
    {
        ClosePanel();
    }

    public void OpenPanel(float p_transitionTime = 0)
    {
        base.Open(p_transitionTime);
    }

    public void ClosePanel(float p_transitionTime = 0)
    {
        base.Close(p_transitionTime);
    }

    public void BackToMenu()
    {
        OnRestart?.Invoke();
    }
}
