using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Plains Tile</c> Class.
/// Contains all info about each individual plains tile.
/// </summary>
public class PlainsTile : GameTile
{
    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each plains tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin { get; }

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each plains tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food { get; }

    /// <summary>
    /// Read only self implemented propriety that stores the tile type of each
    /// plains tile.
    /// </summary>
    /// <value>Tile type of the game tile.</value>
    public override TileType Type => TileType.Plains;

    /// <summary>
    /// Overrides IEnumerable<Resource> and stores it in resourceList.
    /// </summary>
    public override IEnumerable<Resource> Resources => resourceList;

    /// <summary>
    /// Creates a list of the Resource type.
    /// </summary>
    /// <typeparam name="Resource"></typeparam>
    /// <returns>Stores resources in current tile.</returns>
    private List<Resource> resourceList = new List<Resource>();

    /// <summary>
    /// Adds resource to resourceList.
    /// </summary>
    /// <param name="resource"></param>
    public override void AddResource(Resource resource)
    {
        resourceList.Add(resource);
    }

    /// <summary>
    /// Constructor method, instantiates a new Plains Tile.
    /// </summary>
    /// <param name="Coin">Plains Tile's monetary value.</param>
    /// <param name="Food">Plains Tile's food production value.</param>
    /// <param name="totalResourceCoin">
    /// Total monetary value of the tile's resources.</param>
    /// <param name="grid">
    /// Total food production value of the tile's resources.</param>
    public PlainsTile()
    {
        // Integers that stores total monetary and food production values of
        // resources.
        int totalResourceCoin = 0;
        int totalResourceFood = 0;

        // Goes through each of the tile's resources.
        foreach (Resource resource in resourceList)
        {
            // Sums resources total monetary and food production values.
            totalResourceCoin += resource.Coin;
            totalResourceFood += resource.Food;
        }

        // Saves tile's total monetary and food production values.
        this.Coin = 0 + totalResourceCoin;
        this.Food = 2 + totalResourceFood;
    }

    /// <summary>
    /// Shows all of the tile's important information.
    /// </summary>
    /// <remarks>Specially useful for debugging.</remarks>
    /// <returns>A string with all of the tile's info</returns>
    public override string ToString()
    {
        // Shows resource introduction sentence.
        Debug.Log("---RESOURCES---");

        // Goes through each resource.
        foreach (Resource resource in resourceList)
        {
            // Shows relevant resource information.
            Debug.Log($"Type: {resource.GetType()} / Coin: {resource.Coin} / Food: {resource.Food}");
        }

        // Shows game tile introduction sentence.
        Debug.Log("---GAME TILE---");

        // Shows general tile information.
        return $"Type: {this.GetType()} / Coin: {this.Coin} / Food: {this.Food}";
    }
}
