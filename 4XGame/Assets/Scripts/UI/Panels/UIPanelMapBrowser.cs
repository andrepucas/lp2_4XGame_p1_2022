using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIPanelMapBrowser : UIPanel
{
    // Serialized
    [Header("Scroll Rect Widgets")]
    [SerializeField] private Transform _widgetsFolder;
    [SerializeField] private GameObject _mapFileWidget;
    [SerializeField] private GameObject _mapFileGeneratorWidget;
    [Header("Buttons Data")]
    [SerializeField] private TMP_Text _refreshTimeText;

    private void OnEnable() => MapFileGeneratorWidget.NewMapFile += CreateNewWidget;
    private void OnDisable() => MapFileGeneratorWidget.NewMapFile -= CreateNewWidget;

    public void SetupPanel()
    {
        InstantiateMapFileWidgets();
        ClosePanel();
    }

    public void OpenPanel(float p_transitionTime = 0) => base.Open(p_transitionTime);

    public void ClosePanel(float p_transitionTime = 0) => base.Close(p_transitionTime);

    private void InstantiateMapFileWidgets()
    {
        GameObject m_widget;

        // Destroy any existing widgets.
        foreach (Transform f_widget in _widgetsFolder)
            GameObject.Destroy(f_widget.gameObject);

        // If there are map files.
        if (MapFilesBrowser.GetMapsList() != null)
        {
            // Instantiate all of them as map file widgets.
            foreach (MapData f_map in MapFilesBrowser.GetMapsList())
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

        // Update refreshed time.
        DisplayCurrentTime();
    }

    private void CreateNewWidget(string p_name, int p_sizeX, int p_sizeY, 
        MapFileGeneratorDataSO p_data)
    {
        MapFilesBrowser.GenerateNewMapFile(p_name, p_sizeX, p_sizeY, p_data);
        InstantiateMapFileWidgets();
    }

    private void DisplayCurrentTime()
    {
        string m_hour = System.DateTime.Now.Hour.ToString("D2");
        string m_minute = System.DateTime.Now.Minute.ToString("D2");
        string m_second = System.DateTime.Now.Second.ToString("D2");

        _refreshTimeText.text = 
            ("Last updated at " + m_hour + ":" + m_minute + ":" + m_second);
    }

    public void RefreshWidgets()
    {
        InstantiateMapFileWidgets();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
