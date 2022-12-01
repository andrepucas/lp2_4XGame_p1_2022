using System.Collections.Generic;

/// <summary>
/// <c>Water Tile</c> Class.
/// Contains all info about each individual water tile.
/// </summary>
public class WaterTile : GameTile
{
    public override string Name => "Water";

    public override int BaseCoin => 0;
    public override int BaseFood => 1;

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each water tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin { get; protected set; }

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each water tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food { get; protected set; }

    /// <summary>
    /// Overrides IEnumerable<Resource> and stores it in resourceList.
    /// </summary>
    public override ICollection<Resource> Resources => resourceList;

    /// <summary>
    /// Creates a list of the Resource type.
    /// </summary>
    /// <typeparam name="Resource"></typeparam>
    /// <returns>Stores resources in current tile.</returns>
    private List<Resource> resourceList = new List<Resource>();

    /// <summary>
    /// Constructor method, instantiates a new Water Tile.
    /// </summary>
    /// <param name="Coin">Water Tile's monetary value.</param>
    /// <param name="Food">Water Tile's food production value.</param>
    /// <param name="totalResourceCoin">
    /// Total monetary value of the tile's resources.</param>
    /// <param name="grid">
    /// Total food production value of the tile's resources.</param>
    public WaterTile()
    {
        // Saves tile's total monetary and food production values.
        Coin = BaseCoin;
        Food = BaseFood;
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
        return $"{GetType().Name} TILE [C: {BaseCoin}, F: {BaseFood}] " + base.ToString();
    }
}
