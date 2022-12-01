using System;
using System.Collections.Generic;

public class MapData : IComparable<MapData>
{
    public string Name {get; set;}
    public int Dimensions_X {get;}
    public int Dimensions_Y {get;}
    public string[] Data {get;}
    public List<GameTile> GameTiles {get; private set;}

    public MapData (string p_name, string[] p_data)
    {
        // Dimensions can be found in the first line, separated by a space.
        string[] m_dimensions = p_data[0].Split();

        Name = p_name;
        Data = p_data;

        // X equals the first string of the first line.
        Dimensions_X = Convert.ToInt32(m_dimensions[0]);

        // Y equals the second string of the first line.
        Dimensions_Y = Convert.ToInt32(m_dimensions[1]);

        GameTiles = new List<GameTile>(Dimensions_X * Dimensions_Y);
    }

    public void LoadGameTilesInfo()
    {
        int m_commentIndex;
        string m_line;
        string[] m_lineStrings;

        // Handle Data info. Ignores first line.
        for (int i = 1; i < Data.Length; i++)
        {
            // Save this line.
            m_line = Data[i].ToLower();

            // Look for a "#" in the string.
            m_commentIndex = m_line.IndexOf("#");

            // If there is one, ignore everything that comes after it.
            if (m_commentIndex >= 0) m_line = m_line.Substring(0, m_commentIndex);

            // Split line into individual strings, separating by spaces.
            m_lineStrings = Data[i].Split();

            // Handle and instantiate the game tile (first string).
            switch (m_lineStrings[0])
            {
                case "desert":
                    GameTiles.Insert(i - 1, new DesertTile());
                    break;

                case "hills":
                    GameTiles.Insert(i - 1, new HillsTile());
                    break;

                case "mountain":
                    GameTiles.Insert(i - 1, new MountainTile());
                    break;

                case "plains":
                    GameTiles.Insert(i - 1, new PlainsTile());
                    break;

                case "water":
                    GameTiles.Insert(i - 1, new WaterTile());
                    break;
            }

            // If there are more strings.
            if (m_lineStrings.Length > 0)
            {
                // Iterate each one, starting on the second one, the first resource.
                for (int s = 1; s < m_lineStrings.Length; s++)
                {
                    // Handle and instantiate the resource.
                    switch (m_lineStrings[s])
                    {
                        case "plants":
                            GameTiles[i - 1].AddResource(new PlantsResource());
                            break;

                        case "animals":
                            GameTiles[i - 1].AddResource(new AnimalsResource());
                            break;

                        case "metals":
                            GameTiles[i - 1].AddResource(new MetalsResource());
                            break;

                        case "fossilfuel":
                            GameTiles[i - 1].AddResource(new FossilFuelResource());
                            break;

                        case "luxury":
                            GameTiles[i - 1].AddResource(new LuxuryResource());
                            break;

                        case "pollution":
                            GameTiles[i - 1].AddResource(new PollutionResource());
                            break;
                    }
                }
            }
        }
    }

    // Orders by Name, alphabetical.
    public int CompareTo(MapData other) => 
        Name.CompareTo(other?.Name);
}
