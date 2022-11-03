using UnityEngine;

public class LoadMapPanel : MonoBehaviour
{
    // Serialized
    [Header("Scroll Rect Widgets")]
    [SerializeField] private Transform _widgetsFolder;
    [SerializeField] private GameObject _mapFileWidget;
    [SerializeField] private GameObject _mapFileGeneratorWidget;

    private void OnEnable() => MapFileGeneratorWidget.NewMapFile += CreateNewWidget;
    private void OnDisable() => MapFileGeneratorWidget.NewMapFile -= CreateNewWidget;
    
    private void Start()
    {
        InstantiateMapFileWidgets();
    }

    private void InstantiateMapFileWidgets()
    {
        GameObject m_widget;

        // Destroy any existing widgets.
        foreach (Transform f_widget in _widgetsFolder)
            GameObject.Destroy(f_widget.gameObject);

        // If there are map files.
        if (MapsBrowser.GetMapsList() != null)
        {
            // Instantiate all of them as map file widgets.
            foreach (MapData f_map in MapsBrowser.GetMapsList())
            {
                m_widget = Instantiate(_mapFileWidget, Vector3.zero, Quaternion.identity);
                m_widget.transform.SetParent(_widgetsFolder, false);

                m_widget.GetComponent<MapFileWidget>().Initialize(f_map);
            }
        }

        else Debug.Log("No map files.");

        // Instantiate map file generator widget at the end of the list.
        Instantiate(_mapFileGeneratorWidget, Vector3.zero, 
            Quaternion.identity).transform.SetParent(_widgetsFolder, false);
    }

    private void CreateNewWidget(string p_name, int p_sizeX, int p_sizeY, 
        MapFileGeneratorDataSO p_data)
    {
        MapsBrowser.GenerateNewMapFile(p_name, p_sizeX, p_sizeY, p_data);
        InstantiateMapFileWidgets();
    }
}
