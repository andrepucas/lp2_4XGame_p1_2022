using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuxuryResource : Resource
{
    /// <summary>
    /// Read only self implemented property that stores the name of this 
    /// resource.
    /// </summary>
    /// <value>Name of the resource.</value>
    public override ResourceTypes Name {get;}
    
    /// <summary>
    /// Read only self implemented property that stores the monetary value of
    /// this resource.
    /// </summary>
    /// <value>Monetary value of the resource.</value>
    public override int Coin {get;}

    /// <summary>
    /// Read only self implemented property that stores the food production 
    /// value of this resource.
    /// </summary>
    /// <value>Food Production of the resource.</value>
    public override int Food {get;}

    public LuxuryResource()
    {
        this.Name = ResourceTypes.Luxury;
        this.Coin = 4;
        this.Food = -1;
    }
}
