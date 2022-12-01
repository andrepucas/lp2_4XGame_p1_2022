using UnityEngine;
using TMPro;
using System.Linq;
using System.Text;
using System.Collections.Generic;

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
        StringBuilder m_stringBuilder = new StringBuilder();

        switch (p_index)
        {
            case 1:

                _titleTxt.text = "1. No. of tiles without resources";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                _answerTxt.text =
                    p_mapData.GameTiles.Count(t => t.Resources.Count == 0).ToString();

                break;

            case 2:

                _titleTxt.text = "2. Average Coin in Mountain tiles";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                _answerTxt.text = (p_mapData.GameTiles.OfType<MountainTile>().Any())
                    ? p_mapData.GameTiles.OfType<MountainTile>().Average(t => t.Coin).ToString("0.00")
                    : "0";

                break;

            case 3:

                _titleTxt.text = "3. Existing terrains, alphabetically";
                _answerTxt.fontSize = FONT_SIZE_STRING;

                IEnumerable<string> _existingTerrains =
                    p_mapData.GameTiles.OrderBy(t => t.Name).Select(t => t.Name).Distinct();

                foreach (string f_terrain in _existingTerrains)
                    m_stringBuilder.Append(f_terrain + "\n");

                // sorted list terrains
                _answerTxt.text = m_stringBuilder.ToString();

                break;

            case 4:

                _titleTxt.text = "4. Tile with least Coin";
                _answerTxt.fontSize = FONT_SIZE_STRING;

                GameTile m_tile = p_mapData.GameTiles.OrderBy(t => t.Coin).FirstOrDefault();

                m_stringBuilder.Append(m_tile.Name + "\n\n");

                foreach (Resource r in m_tile.Resources)
                    m_stringBuilder.Append("-" + r.Name + "\n");

                // Calculate coords of game tile based on index.
                float m_aux = p_mapData.GameTiles.IndexOf(m_tile) *
                    p_mapData.Dimensions_Y / (float)(p_mapData.Dimensions_Y * p_mapData.Dimensions_X);

                long m_yCoords = (long)m_aux;
                int m_xCoords = (int)((m_aux - m_yCoords) * p_mapData.Dimensions_X);

                m_stringBuilder.Append("\n" + $"({m_xCoords.ToString()}, {m_yCoords.ToString()})");

                // type, resources, coords of tile.
                _answerTxt.text = m_stringBuilder.ToString();

                break;

            case 5:

                _titleTxt.text = "5. No. of unique tiles";
                _answerTxt.fontSize = FONT_SIZE_NUM;

                // count of [tile + resources] diferentes
                _answerTxt.text = p_mapData.GameTiles.Distinct(new GameTileComparer()).Count().ToString();

                break;
        }
    }
}

