using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
