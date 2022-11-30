using UnityEngine;
using TMPro;
using System.Linq;

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

        switch (p_index)
        {
            case 1:

                _titleTxt.text = "1. No. of tiles without resources";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                _answerTxt.text = "?";

                break;

            case 2:

                _titleTxt.text = "2. Average Coin in Mountain tiles";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                _answerTxt.text = "?";

                break;

            case 3:

                _titleTxt.text = "3. All terrains, alphabetically";
                _answerTxt.fontSize = FONT_SIZE_STRING;

                // sorted list terrains
                _answerTxt.text = "?";

                break;

            case 4:

                _titleTxt.text = "4. Tile with least Coin";
                _answerTxt.fontSize = FONT_SIZE_STRING;

                // type, resources, coords of tile.
                _answerTxt.text = "?";

                break;

            case 5:

                _titleTxt.text = "5. No. of unique tiles";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                // count of [tile + resources] diferentes
                _answerTxt.text = "?";

                break;
        }
    }
}
