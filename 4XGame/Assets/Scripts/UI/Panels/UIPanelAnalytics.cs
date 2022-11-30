using UnityEngine;
using TMPro;

public class UIPanelAnalytics : UIPanel
{
    private const string OPEN_TRIGGER = "Open";
    private const string CLOSE_TRIGGER = "Close";
    private const float FONT_SIZE_NUM = 100f;
    private const float FONT_SIZE_STRING = 50f;

    [Header("Animator")]
    [SerializeField] private Animator _subPanelAnim;
    [Header("Text")]
    [SerializeField] private TMP_Text _titleTxt;
    [SerializeField] private TMP_Text _answerTxt;

    private MapData _data;
    private int _currentAnalytic;

    public void SetupPanel()
    {
        ClosePanel();
    }

    public void OpenPanel(float p_transitionTime = 0)
    {
        base.Open(p_transitionTime);
        _subPanelAnim.SetTrigger(OPEN_TRIGGER);
    }

    public void ClosePanel(float p_transitionTime = 0)
    {
        _subPanelAnim.SetTrigger(CLOSE_TRIGGER);
        base.Close(p_transitionTime);
    }

    public void UpdateData(int p_index, MapData p_mapData)
    {
        _currentAnalytic = p_index;
        _data = p_mapData;

        // Switch for current analytic.
        // Do magics.
        // _titleTxt.text = "TITLE"
        // _answerTxt.text "something"
        // depending on answer type, _answerTxt.fontSize = FONT_SIZE_NUM || FONT_SIZE_LIST
    }
}
