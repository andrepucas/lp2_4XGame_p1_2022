using UnityEngine;

public class LoadMapPanel : MonoBehaviour
{
    // Serialized
    [Header("Scroll Rect Widgets")]
    [SerializeField] private Transform _widgetsFolder;
    [SerializeField] private GameObject _mapFileWidget;
    [SerializeField] private GameObject _mapFileGeneratorWidget;

    private void Start()
    {
        GameObject m_widget;

        // Destroy any existing widgets.
        foreach (Transform f_widget in _widgetsFolder)
            GameObject.Destroy(f_widget.gameObject);

        // If there are map files.
        if (MapsBrowser.GetMapsList() != null)
        {
            foreach (MapData f_map in MapsBrowser.GetMapsList())
            {
                m_widget = Instantiate(_mapFileWidget, Vector3.zero, Quaternion.identity);
                m_widget.transform.SetParent(_widgetsFolder, false);

                m_widget.GetComponent<MapFileWidget>().Initialize(f_map);
            }
        }

        else Debug.Log("No map files.");

        // Instantiate map file generator.
    }
}
