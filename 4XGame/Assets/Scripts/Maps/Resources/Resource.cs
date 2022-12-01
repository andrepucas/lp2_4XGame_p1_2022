public abstract class Resource
{
    public abstract string Name {get;}

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
