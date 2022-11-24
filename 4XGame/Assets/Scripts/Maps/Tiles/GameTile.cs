using System.Collections.Generic;

/// <summary>
/// <c>Game Tile</c> Class.
/// Contains all the generic info about each individual game tile.
/// </summary>
public abstract class GameTile
{
    /// <summary>
    /// Read only self implemented property that stores the monetary value of
    /// each game tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public abstract int Coin { get; protected set;}

    /// <summary>
    /// Read only self implemented property that stores the food production 
    /// value of each game tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public abstract int Food { get; protected set;}

    /// <summary>
    /// Read only self implemented propriety that stores the tile type of each
    /// game tile.
    /// </summary>
    /// <value>Tile type of each game tile.</value>
    public abstract TileType Type { get; }

    public abstract IEnumerable<Resource> Resources { get; }

    public abstract void AddResource(Resource resource);

    /// <summary>
    /// Shows all of the tile's important information.
    /// </summary>
    /// <remarks>Specially useful for debugging.</remarks>
    /// <returns>A string with all of the tile's info</returns>
    public override string ToString()
    {
        string m_info = "";

        // Goes through each resource.
        foreach (Resource resource in Resources)
        {
            // Shows relevant resource information.
            m_info += $" + {resource.Type.ToString().ToUpper()} RESOURCE [C: {resource.Coin}, F: {resource.Food}]";
        }

        m_info += $" => [C: {Coin}, F: {Food}]";

        return m_info;
    }
}
