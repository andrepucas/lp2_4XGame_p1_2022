using System;

public class MapData : IComparable<MapData>
{
    public string Name {get; set;}
    public int Dimensions_X {get;}
    public int Dimension_Y {get;}
    public GameTile[] GameTiles {get;}

    public MapData (string p_name, int p_dimensionX, int p_dimensionY)
    {
        Name = p_name;
        Dimensions_X = p_dimensionX;
        Dimension_Y = p_dimensionY;
    }

    // Orders by Name, alphabetical.
    public int CompareTo(MapData other)
    {
        if (other == null) return 1;
        return Name.CompareTo(other.Name);
    }
}
