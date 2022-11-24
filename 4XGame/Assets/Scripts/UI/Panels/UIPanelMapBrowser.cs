using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIPanelMapBrowser : UIPanel
{
    // Events
    public static event Action<MapData> OnSendMapData;

    // Serialized
    [Header("Scroll Rect Widgets")]
    [SerializeField] private Transform _widgetsFolder;
    [SerializeField] private GameObject _mapFileWidget;
    [SerializeField] private GameObject _mapFileGeneratorWidget;
    [Header("Buttons Data")]
    [SerializeField] private TMP_Text _refreshTimeText;
    [SerializeField] private RectTransform _loadButtonSize;
    [SerializeField] private TMP_Text _loadButtonText;

    // Variables
    private List<MapFileWidget> _widgetsList;
    private MapFileWidget _lastWidgetSelected;

    private void OnEnable()
    {
        MapFileWidget.OnSelected += UpdateLastSelected;
        MapFileWidget.OnDeleted += () => InstantiateMapFileWidgets();
        MapFileGeneratorWidget.OnNewMapFile += CreateNewWidget;
    }
    private void OnDisable()
    {
        MapFileWidget.OnSelected -= UpdateLastSelected;
        MapFileWidget.OnDeleted -= () => InstantiateMapFileWidgets();
        MapFileGeneratorWidget.OnNewMapFile -= CreateNewWidget;
    }

    public void SetupPanel()
    {
        ClosePanel();

        _widgetsList = new List<MapFileWidget>();
        _lastWidgetSelected = null;
    }

    public void OpenPanel(float p_transitionTime = 0)
    {
        base.Open(p_transitionTime);
        InstantiateMapFileWidgets();
    }

    public void ClosePanel(float p_transitionTime = 0) => base.Close(p_transitionTime);

    private void InstantiateMapFileWidgets(string p_newWidgetName = "")
    {
        GameObject m_widgetObj;

        // Destroy any existing widgets.
        foreach (Transform f_widget in _widgetsFolder)
            GameObject.Destroy(f_widget.gameObject);

        _widgetsList.Clear();

        // If there are map files.
        if (MapFilesBrowser.GetMapsList() != null)
        {
            MapFileWidget m_fileWidget;

            // Instantiate all of them as map file widgets.
            foreach (MapData f_map in MapFilesBrowser.GetMapsList())
            {
                m_widgetObj = Instantiate(_mapFileWidget, Vector3.zero, Quaternion.identity);
                m_widgetObj.transform.SetParent(_widgetsFolder, false);

                m_fileWidget = m_widgetObj.GetComponent<MapFileWidget>();

                m_fileWidget.Initialize(f_map);
                _widgetsList.Add(m_fileWidget);
            }
        }

        else Debug.Log("No map files.");

        if (p_newWidgetName == "") PreSelectWidget();
        else PreSelectWidget(p_newWidgetName);

        // Instantiate map file generator widget at the end of the list.
        Instantiate(_mapFileGeneratorWidget, Vector3.zero, 
            Quaternion.identity).transform.SetParent(_widgetsFolder, false);

        // Update refreshed time.
        DisplayCurrentTime();
    }

    private void CreateNewWidget(string p_name, int p_sizeX, int p_sizeY, 
        MapFileGeneratorDataSO p_data)
    {
        string m_newWidgetName;

        m_newWidgetName = MapFilesBrowser.GenerateNewMapFile(p_name, p_sizeX, p_sizeY, p_data);
        InstantiateMapFileWidgets(m_newWidgetName);
    }

    private void UpdateLastSelected(MapFileWidget p_selectedWidget)
    {
        if (_lastWidgetSelected != null && _lastWidgetSelected != p_selectedWidget) 
            _lastWidgetSelected.DeSelect();

        _lastWidgetSelected = p_selectedWidget;

        StartCoroutine(UpdateLoadButton(_lastWidgetSelected.MapData.Name));
    }

    private void PreSelectWidget()
    {
        MapFileWidget m_widget = _widgetsList[0];

        if (_lastWidgetSelected != null)
        {
            foreach(MapFileWidget f_widget in _widgetsList)
            {
                if (f_widget.Equals(_lastWidgetSelected))
                {
                    m_widget = f_widget;
                    break;
                }
            }
        }

        m_widget.Select();
    }

    private void PreSelectWidget(string p_newWidgetName)
    {
        foreach(MapFileWidget f_widget in _widgetsList)
        {
            if (f_widget.MapData.Name == p_newWidgetName)
            {
                f_widget.Select();
                break;
            }
        }
    }

    private void DisplayCurrentTime()
    {
        string m_hour = System.DateTime.Now.Hour.ToString("D2");
        string m_minute = System.DateTime.Now.Minute.ToString("D2");
        string m_second = System.DateTime.Now.Second.ToString("D2");

        _refreshTimeText.text = 
            ("Last updated at " + m_hour + ":" + m_minute + ":" + m_second);
    }

    private IEnumerator UpdateLoadButton(string p_mapName)
    {
        Vector2 m_loadButtonSize = _loadButtonSize.sizeDelta;

        _loadButtonText.text = "LOAD " + p_mapName.ToUpper();

        // Wait for the text bounds to adjust, with the content size fitter component.
        yield return new WaitForEndOfFrame();

        m_loadButtonSize.x = _loadButtonText.textBounds.size.x + 15;
        _loadButtonSize.sizeDelta = m_loadButtonSize;
    }

    public void RefreshWidgets()
    {
        InstantiateMapFileWidgets();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SendMapData()
    {
        MapData m_mapDataToLoad = _lastWidgetSelected.MapData;
        OnSendMapData?.Invoke(m_mapDataToLoad);
    }
}
