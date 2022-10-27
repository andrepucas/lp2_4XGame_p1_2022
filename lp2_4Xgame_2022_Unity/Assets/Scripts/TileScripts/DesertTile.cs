using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertTile : GameTile
{
    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each desert tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin => 0;

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each desert tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food => 0;

    //public override IEnumerable<Resources> Resources => resourceList;

    private List<Resources> resourceList = new List<Resources>();

    // public override void AddResource(Resources resource)
    // {
    //     resourceList.Add(resource);
    // }

    public DesertTile()
    {
        // foreach(Resources resource in resourceList)
        // {
        //     if (resource == DesertTile)
        //     {
                
        //     }
        // }
    } 
}
