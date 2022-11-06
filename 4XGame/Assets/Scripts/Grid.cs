using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Game Tile</c> Class.
/// Contains all the generic information about each grid.
/// </summary>
public class Grid
{
    /// <summary>
    /// Stores the X dimentions of each grid.
    /// </summary>
    /// <value>X dimentions of each grid.</value>
    public int X { get; }

    /// <summary>
    /// Stores the Y dimentions of each grid.
    /// </summary>
    /// <value>Y dimentions of each grid.</value>
    public int Y { get; }

    /// <summary>
    /// Constructor method, instantiates a new Grid.
    /// </summary>
    /// <param name="x">X dimentions of each grid.</param>
    /// <param name="y">Y dimentions of each grid.</param>
    public Grid(int x, int y)
    {
        // Saves all propriety values.
        X = x;
        Y = y;
    }

}
