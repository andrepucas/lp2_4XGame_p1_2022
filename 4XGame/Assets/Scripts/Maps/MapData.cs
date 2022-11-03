using System;

public class MapData : IComparable<MapData>
{
    public string Name {get; set;}
    public int Dimensions_X {get;}
    public int Dimension_Y {get;}
    public GameTile[] GameTiles {get;}

    // Orders by Name, alphabetical.
    public int CompareTo(MapData other)
    {
        if (other == null) return 1;
        return Name.CompareTo(other.Name);
    }
}
