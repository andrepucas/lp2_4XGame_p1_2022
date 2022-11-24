using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource
{
    /// <summary>
    /// Read only self implemented property that stores the name of each 
    /// resource.
    /// </summary>
    /// <value>Name of the resource.</value>
    public abstract ResourceTypes Type {get;}
    
    /// <summary>
    /// Read only self implemented property that stores the monetary value of
    /// each resource.
    /// </summary>
    /// <value>Monetary value of the resource.</value>
    public abstract int Coin {get;}

    /// <summary>
    /// Read only self implemented property that stores the food production 
    /// value of each resource.
    /// </summary>
    /// <value>Food Production of the resource.</value>
    public abstract int Food {get;}
}
