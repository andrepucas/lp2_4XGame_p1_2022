using System;

public class MapData : IComparable<MapData>
{
    public string Name {get; set;}
    public int Dimensions_X {get;}
    public int Dimension_Y {get;}
    public string[] Data {get;}
    public GameTile[] GameTiles {get;}

    public MapData (string p_name, string[] p_data)
    {
        // Dimensions can be found in the first line, separated by a space.
        string[] m_dimensions = p_data[0].Split();

        Name = p_name;
        Data = p_data;

        // X equals the first string of the first line.
        Dimensions_X = Convert.ToInt32(m_dimensions[0]);

        // Y equals the second string of the first line.
        Dimension_Y = Convert.ToInt32(m_dimensions[1]);
    }

    public void LoadGameTilesInfo()
    {
        // Handle info in _data.
    }

    // Orders by Name, alphabetical.
    public int CompareTo(MapData other)
    {
        if (other == null) return 1;
        return Name.CompareTo(other.Name);
    }
}
