using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class MapsBrowser
{
    // Constants
    private const string FOLDER = "/maps4xfiles/";

    // Variables
    private static string _path = System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.Desktop) + FOLDER;

    public static List<MapData> GetMapsList()
    {
        // If folder doesn´t exist, stop here.
        if (!Directory.Exists(_path)) return null;
        
        List<MapData> _mapsList = new List<MapData>();
        DirectoryInfo m_info = new DirectoryInfo(_path);

        foreach(FileInfo file in m_info.GetFiles("*.map4x"))
        {
            _mapsList.Add(new MapData{Name = Path.GetFileNameWithoutExtension(file.FullName)});
        }

        return _mapsList;
    }

    public static bool MapFilesExist()
    {
        if (!Directory.Exists(_path)) return false;

        DirectoryInfo m_info = new DirectoryInfo(_path);

        foreach(FileInfo file in m_info.GetFiles("*.map4x"))
        {
            return true;
        }

        return false;
    }
}
