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
    [SerializeField] private GameObject _animalsObj;
    [SerializeField] private GameObject _fossilFuelObj;
    [SerializeField] private GameObject _luxuryObj;
    [SerializeField] private GameObject _metalsObj;
    [SerializeField] private GameObject _plantsObj;
    [SerializeField] private GameObject _pollutionObj;

    // Variables
    private GameTile _tile;
    private Sprite[] _resourceSprites;

    public void Initialize(GameTile p_tile)
    {
        _resourceSprites = new Sprite[6];
        _tile = p_tile;

        _animalsObj.SetActive(false);
        _fossilFuelObj.SetActive(false);
        _luxuryObj.SetActive(false);
        _metalsObj.SetActive(false);
        _plantsObj.SetActive(false);
        _pollutionObj.SetActive(false);

        OnPointerExit();
        EnableResourceSprites(_tile);
    }

    public void OnPointerEnter()
    {
        _image.sprite = _hoveredSprite;
    }

    public void OnPointerExit()
    {
        _image.sprite = _baseSprite;
    }

    public void OnClick()
    {
        OnInspect?.Invoke();
        OnSendData?.Invoke(_tile, _baseSprite, _resourceSprites);
    }

    private void EnableResourceSprites(GameTile p_tile)
    {
        foreach (Resource resource in p_tile.Resources)
        {
            switch (resource)
            {
                case AnimalsResource:

                    _animalsObj.SetActive(true);
                    _resourceSprites[0] = _animalsObj.GetComponent<Image>().sprite;
                    break;

                case FossilFuelResource:

                    _fossilFuelObj.SetActive(true);
                    _resourceSprites[1] = _fossilFuelObj.GetComponent<Image>().sprite;
                    break;

                case LuxuryResource:

                    _luxuryObj.SetActive(true);
                    _resourceSprites[2] = _luxuryObj.GetComponent<Image>().sprite;
                    break;

                case MetalsResource:

                    _metalsObj.SetActive(true);
                    _resourceSprites[3] = _metalsObj.GetComponent<Image>().sprite;
                    break;

                case PlantsResource:

                    _plantsObj.SetActive(true);
                    _resourceSprites[4] = _plantsObj.GetComponent<Image>().sprite;
                    break;

                case PollutionResource:

                    _pollutionObj.SetActive(true);
                    _resourceSprites[5] = _pollutionObj.GetComponent<Image>().sprite;
                    break;
            }
        }
    }
}


