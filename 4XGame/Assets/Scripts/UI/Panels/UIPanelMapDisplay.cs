using UnityEngine;

public class UIPanelMapDisplay : UIPanel
{
    public void SetupPanel()
    {
        ClosePanel();

        // Destroy any existing map cells.
        foreach (Transform f_child in transform)
            Destroy(f_child.gameObject);
    }

    public void OpenPanel(float p_transitionTime = 0)
    {
        base.Open(p_transitionTime);
    }

    public void ClosePanel(float p_transitionTime = 0)
    {
        base.Close(p_transitionTime);
    }
}
