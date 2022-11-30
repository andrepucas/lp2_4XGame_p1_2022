using System;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    // Events
    public static event Action OnInspect;
    public static event Action<GameTile, Sprite, Sprite[]> OnSendData;

    // Serialized
    [Header("Sprites")]
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _baseSprite;
    [SerializeField] private Sprite _hoveredSprite;
    [Header("Resources")]
    [SerializeField] private GameObject[] _resourceObjs;
    [SerializeField] private Image[] _resourceImages;

    // Variables
    private GameTile _tile;
    private Sprite[] _activeResourceSprites;

    public void Initialize(GameTile p_tile)
    {
        _activeResourceSprites = new Sprite[6];
        _tile = p_tile;

        for (int i = 0; i < _resourceObjs.Length; i++)
            _resourceObjs[i].SetActive(false);

        OnPointerExit();
        EnableResourceSprites(_tile);
    }

    public void OnPointerEnter() => _image.sprite = _hoveredSprite;

    public void OnPointerExit() => _image.sprite = _baseSprite;

    public void OnClick()
    {
        OnInspect?.Invoke();
        OnSendData?.Invoke(_tile, _baseSprite, _activeResourceSprites);
    }

    private void EnableResourceSprites(GameTile p_tile)
    {
        foreach (Resource resource in p_tile.Resources)
        {
            switch (resource)
            {
                case AnimalsResource:

                    _resourceObjs[0].SetActive(true);
                    _activeResourceSprites[0] = _resourceImages[0].sprite;
                    break;

                case FossilFuelResource:

                    _resourceObjs[1].SetActive(true);
                    _activeResourceSprites[1] = _resourceImages[1].sprite;
                    break;

                case LuxuryResource:

                    _resourceObjs[2].SetActive(true);
                    _activeResourceSprites[2] = _resourceImages[2].sprite;
                    break;

                case MetalsResource:

                    _resourceObjs[3].SetActive(true);
                    _activeResourceSprites[3] = _resourceImages[3].sprite;
                    break;

                case PlantsResource:

                    _resourceObjs[4].SetActive(true);
                    _activeResourceSprites[4] = _resourceImages[4].sprite;
                    break;

                case PollutionResource:

                    _resourceObjs[5].SetActive(true);
                    _activeResourceSprites[5] = _resourceImages[5].sprite;
                    break;
            }
        }
    }
}


