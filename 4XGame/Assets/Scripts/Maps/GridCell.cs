using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private GameTile _tile;
    // [SerializeField] private GameObject _animalsSprite;
    // [SerializeField] private GameObject _fossilFuelSprite;
    // [SerializeField] private GameObject _luxurySprite;
    // [SerializeField] private GameObject _metalsSprite;
    // [SerializeField] private GameObject _plantsSprite;
    // [SerializeField] private GameObject _pollutionSprite;

    public void Initialize(GameTile p_tile)
    {
        _tile = p_tile;
    }

    public void OnClick()
    {
        Debug.Log($"Coin: {_tile.Coin}, Food: {_tile.Food}");
    }


}
