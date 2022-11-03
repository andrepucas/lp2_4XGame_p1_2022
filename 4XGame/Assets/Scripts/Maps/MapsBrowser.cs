using System.Collections.Generic;
using System.IO;
using ImportedGenerator;
using UnityEngine;

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
            _mapsList.Add(new MapData(
                Path.GetFileNameWithoutExtension(file.FullName), 
                0,
                0));
        }

        _mapsList.Sort();
        return _mapsList;
    }

    public static void RenameMapFile(string p_oldName, string p_newName)
    {
        p_oldName += ".map4x";
        p_newName += ".map4x";

        System.IO.File.Move((_path + p_oldName), (_path + p_newName));
    }

    public static void DeleteMapFile(string p_fileName)
    {
        p_fileName += ".map4x";

        System.IO.File.Delete(_path + p_fileName);
    }

    public static void GenerateNewMapFile(string p_name, int p_sizeX, 
        int p_sizeY, MapFileGeneratorDataSO p_data)
    {
        Generator m_generator;
        Map m_map;
        int m_sameNameFileCount = 0;

        // If folder doesn't exist, create it.
        if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

        // Create new generator and map.
        m_generator = new Generator(p_data.Terrains, p_data.Resources);
        m_map = m_generator.CreateMap(p_sizeX, p_sizeY);

        p_name += ".map4x";

        // If file with this name already exists, append a "_N".
        while (File.Exists(_path + p_name))
        {
            m_sameNameFileCount++;
            p_name = Path.GetFileNameWithoutExtension(_path + p_name);

            // If name already has a _N appended to it, remove it.
            if (m_sameNameFileCount > 1)
            {
                Debug.Log(1);
                p_name = p_name.Substring(0, p_name.Length-2);
            }

            // Add _N to the end of the name.
            p_name += ("_" + m_sameNameFileCount + ".map4x");
        }

        // Create map file.
        m_generator.SaveMap(m_map, (_path + p_name));
    }
}
