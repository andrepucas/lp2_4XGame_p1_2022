using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameTileComparer : IEqualityComparer<GameTile>
{
    StringBuilder _stringB;
    IEnumerable<string> _firstRStrings; 
    IEnumerable<string> _secondRStrings; 

    public bool Equals(GameTile p_first, GameTile p_second)
    {
        _firstRStrings = p_first.Resources.Select(t => t.Name);
        _secondRStrings = p_second.Resources.Select(t => t.Name);

        return p_first.Name == p_second.Name && _firstRStrings.All(_secondRStrings.Contains);
    }

    public int GetHashCode(GameTile p_tile)
    {
        _stringB = new StringBuilder();

        foreach (string r in p_tile.Resources.Select(t => t.Name))
            _stringB.Append(r);

        return p_tile.Name.GetHashCode() ^ _stringB.ToString().GetHashCode();
    }
}
