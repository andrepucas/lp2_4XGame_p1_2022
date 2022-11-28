using UnityEngine;

public class MapCell : MonoBehaviour
{
    // Serialized
    [SerializeField] private GameObject _animalsSprite;
    [SerializeField] private GameObject _fossilFuelSprite;
    [SerializeField] private GameObject _luxurySprite;
    [SerializeField] private GameObject _metalsSprite;
    [SerializeField] private GameObject _plantsSprite;
    [SerializeField] private GameObject _pollutionSprite;

    // Variables
    private GameTile _tile;

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

    private void EnableResourceSprites(GameTile p_tile)
    {
        foreach (Resource resource in p_tile.Resources)
        {
            switch (resource)
            {
                case AnimalsResource:

                    _animalsSprite.SetActive(true);
                    break;

                case FossilFuelResource:

                    _fossilFuelSprite.SetActive(true);
                    break;

                case LuxuryResource:

                    _luxurySprite.SetActive(true);
                    break;

                case MetalsResource:

                    _metalsSprite.SetActive(true);
                    break;

                case PlantsResource:

                    _plantsSprite.SetActive(true);
                    break;

                case PollutionResource:

                    _pollutionSprite.SetActive(true);
                    break;
            }
        }
    }
}


