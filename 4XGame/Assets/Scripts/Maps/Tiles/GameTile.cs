using System.Collections.Generic;

/// <summary>
/// <c>Game Tile</c> Class.
/// Contains all the generic info about each individual game tile.
/// </summary>
public abstract class GameTile
{
    public abstract int BaseCoin {get;}
    public abstract int BaseFood {get;}

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

    public abstract ICollection<Resource> Resources { get; }

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
            m_info += $" + {resource.GetType().Name} [C: {resource.Coin}, F: {resource.Food}]";
        }

        m_info += $" => [C: {Coin}, F: {Food}]";

        return m_info;
    }
}
