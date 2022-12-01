public class PlantsResource : Resource
{
    public override string Name => "Plants";

    /// <summary>
    /// Read only self implemented property that stores the monetary value of
    /// this resource.
    /// </summary>
    /// <value>Monetary value of the resource.</value>
    public override int Coin => 1;

    /// <summary>
    /// Read only self implemented property that stores the food production 
    /// value of this resource.
    /// </summary>
    /// <value>Food Production of the resource.</value>
    public override int Food => 2;
}
