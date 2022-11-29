using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    // Events
    public static event Action OnInspect;
    public static event Action<GameTile, Sprite, Sprite[]> OnSendData;

    // Serialized
    [Header("Base Sprite")]
    [SerializeField] private Sprite _baseSprite;
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

        EnableResourceSprites(_tile);
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


