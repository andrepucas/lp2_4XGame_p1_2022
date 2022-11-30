using System.Collections.Generic;

/// <summary>
/// <c>Desert Tile</c> Class.
/// Contains all info about each individual desert tile.
/// </summary>
public class DesertTile : GameTile
{
    public override int BaseCoin => 0;
    public override int BaseFood => 0;

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each desert tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin { get; protected set; }

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each desert tile.
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
    /// Constructor method, instantiates a new Desert Tile.
    /// </summary>
    /// <param name="Coin">Desert Tile's monetary value.</param>
    /// <param name="Food">Desert Tile's food production value.</param>
    public DesertTile()
    {
        // Set base coin and food values.
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
