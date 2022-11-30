using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanelInspector : UIPanel
{
    private const string OPEN_TRIGGER = "Open";
    private const string CLOSE_TRIGGER = "Close";

    [Header("Animator")]
    [SerializeField] private Animator _subPanelAnim;
    [Header("Sprites Data")]
    [SerializeField] private Image _tileImage;
    [SerializeField] private Image[] _resourcesImages;
    [Header("General Data")]
    [SerializeField] private TMP_Text _tileNameTxt;
    [SerializeField] private TMP_Text _tileResourcesCountTxt;
    [SerializeField] private GameObject[] _resourcesNamesObjs;
    [Header("Coin Data")]
    [SerializeField] private TMP_Text _tileBaseCoinTxt;
    [SerializeField] private TMP_Text _tileTotalCoinTxt;
    [SerializeField] private GameObject[] _resourcesCoinObjs;
    [Header("Food Data")]
    [SerializeField] private TMP_Text _tileBaseFoodTxt;
    [SerializeField] private TMP_Text _tileTotalFoodTxt;
    [SerializeField] private GameObject[] _resourcesFoodObjs;
    [Header("Empty Grid Spaces")]
    [SerializeField] private GameObject[] _emptySpaces;


    private void OnEnable() => MapCell.OnSendData += UpdateDataAndOpen;
    private void OnDisable() => MapCell.OnSendData -= UpdateDataAndOpen;

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

        // Disable empty grid spaces.
        for (int i = 0; i < _emptySpaces.Length; i++)
            _emptySpaces[i].SetActive(true);

        // Loop length of possible existing resources (6, for now).
        for (int i = 0; i < _resourcesImages.Length; i++)
        {
            // Disable sprite, name, coin and food data.
            _resourcesImages[i].gameObject.SetActive(false);
            _resourcesNamesObjs[i].SetActive(false);
            _resourcesCoinObjs[i].SetActive(false);
            _resourcesFoodObjs[i].SetActive(false);
        }
    }

    private void UpdateDataAndOpen(GameTile p_tile, Sprite p_baseSprite, Sprite[] p_resourceSprites)
    {
        string m_name;

        // Display tile name (Removes "Tile" from name). 
        m_name = p_tile.GetType().Name;
        _tileNameTxt.text = m_name.Substring(0, m_name.Length - 4);

        // Display tile view sprite to match the base clicked cell.
        _tileImage.sprite = p_baseSprite;

        // Display base coin and food values of tile.
        _tileBaseCoinTxt.text = p_tile.BaseCoin.ToString();
        _tileBaseFoodTxt.text = p_tile.BaseFood.ToString();

        // Display total coin and food values of tile.
        _tileTotalCoinTxt.text = p_tile.Coin.ToString();
        _tileTotalFoodTxt.text = p_tile.Food.ToString();

        // Update no. of resources.
        _tileResourcesCountTxt.text = 
            ($"No. of resources: " + p_tile.Resources.Count.ToString());

        // If there are resources.
        if (p_tile.Resources.Count > 0)
        {
            // Enable empty grid spaces.
            for (int i = 0; i < _emptySpaces.Length; i++)
                _emptySpaces[i].SetActive(true);

            // Loop length of possible existing resources (6, for now).
            for (int i = 0; i < _resourcesImages.Length; i++)
            {
                // If this resource exists in the clicked cell.
                if (p_resourceSprites[i] != null)
                {
                    // Sync sprite.
                    _resourcesImages[i].sprite = p_resourceSprites[i];

                    // Enable sprite, name, coin and food data.
                    _resourcesImages[i].gameObject.SetActive(true);
                    _resourcesNamesObjs[i].SetActive(true);
                    _resourcesCoinObjs[i].SetActive(true);
                    _resourcesFoodObjs[i].SetActive(true);
                }
            }
        }
    }
}
