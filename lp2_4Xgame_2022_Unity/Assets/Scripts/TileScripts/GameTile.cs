using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTile : MonoBehaviour
{
    /// <summary>
    /// Read only self implemented property that stores the monetary value of
    /// each game tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public abstract int Coin {get;}

    /// <summary>
    /// Read only self implemented property that stores the food production 
    /// value of each game tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public abstract int Food {get;}

    //public abstract IEnumerable<Resources> Resources {get;}

    //public abstract void AddResource (Resources resource);
}
