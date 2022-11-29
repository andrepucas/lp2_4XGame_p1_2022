using UnityEngine;
using UnityEngine.UI;

public class UIPanelInspector : UIPanel
{
    private const string OPEN_TRIGGER = "Open";
    private const string CLOSE_TRIGGER = "Close";

    [Header("Sub Panel")]
    [SerializeField] private Animator _subPanelAnim;
    [SerializeField] private Image _tileImage;
    [SerializeField] private Image[] _resourcesImages;

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

        // Disable all different resource images.
        foreach(Image f_img in _resourcesImages)
            f_img.gameObject.SetActive(false);
    }

    private void UpdateDataAndOpen(GameTile p_tile, Sprite p_baseSprite, Sprite[] p_resourceSprites)
    {
        // Update tile view sprites to match clicked cell.
        _tileImage.sprite = p_baseSprite;

        for (int i = 0; i < _resourcesImages.Length; i++)
        {
            if (p_resourceSprites[i] != null)
            {
                _resourcesImages[i].sprite = p_resourceSprites[i];
                _resourcesImages[i].gameObject.SetActive(true);
            }
        }

        Debug.Log($"Coin: {p_tile.Coin}, Food: {p_tile.Food}");
    }
}
