using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private GameTile _tile;
    [SerializeField] private GameObject _animalsSprite;
    [SerializeField] private GameObject _fossilFuelSprite;
    [SerializeField] private GameObject _luxurySprite;
    [SerializeField] private GameObject _metalsSprite;
    [SerializeField] private GameObject _plantsSprite;
    [SerializeField] private GameObject _pollutionSprite;

    public void Initialize(GameTile p_tile)
    {
        _tile = p_tile;
        _animalsSprite.SetActive(false);
        _fossilFuelSprite.SetActive(false);
        _luxurySprite.SetActive(false);
        _metalsSprite.SetActive(false);
        _plantsSprite.SetActive(false);
        _pollutionSprite.SetActive(false);

        EnableResourceSprites(_tile);
    }

    public void OnClick()
    {
        Debug.Log($"Coin: {_tile.Coin}, Food: {_tile.Food}");
    }

    public void EnableResourceSprites(GameTile p_tile)
    {
        foreach (Resource resource in p_tile.Resources)
        {
            switch (resource.Type)
            {

                case ResourceTypes.Animals:

                    _animalsSprite.SetActive(true);
                    break;

                case ResourceTypes.FossilFuel:

                    _fossilFuelSprite.SetActive(true);
                    break;

                case ResourceTypes.Luxury:

                    _luxurySprite.SetActive(true);
                    break;

                case ResourceTypes.Metals:

                    _metalsSprite.SetActive(true);
                    break;

                case ResourceTypes.Plants:

                    _plantsSprite.SetActive(true);
                    break;

                case ResourceTypes.Pollution:

                    _pollutionSprite.SetActive(true);
                    break;
            }
        }
    }
}


