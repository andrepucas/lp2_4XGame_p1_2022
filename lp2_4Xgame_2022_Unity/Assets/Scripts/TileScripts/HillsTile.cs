using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillsTile : GameTile
{
    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each hills tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin => 1;

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each hills tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food => 1;
}
