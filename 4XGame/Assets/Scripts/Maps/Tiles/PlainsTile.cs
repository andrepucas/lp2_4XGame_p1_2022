using System.Collections.Generic;

/// <summary>
/// <c>Plains Tile</c> Class.
/// Contains all info about each individual plains tile.
/// </summary>
public class PlainsTile : GameTile
{
    private const int BASE_COIN = 0;
    private const int BASE_FOOD = 2;

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each plains tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin { get; protected set; }

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each plains tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food { get; protected set; }

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
        // Saves tile's total monetary and food production values.
        Coin = BASE_COIN;
        Food = BASE_FOOD;
    }

    /// <summary>
    /// Adds resource to resourceList.
    /// </summary>
    /// <param name="resource"></param>
    public override void AddResource(Resource resource)
    {
        resourceList.Add(resource);
        Coin += resource.Coin;
        Food += resource.Food;
    }

    /// <summary>
    /// Shows all of the tile's important information.
    /// </summary>
    /// <remarks>Specially useful for debugging.</remarks>
    /// <returns>A string with all of the tile's info</returns>
    public override string ToString()
    {
        return $"{GetType().Name} TILE [C: {BASE_COIN}, F: {BASE_FOOD}] " + base.ToString();
    }
}
