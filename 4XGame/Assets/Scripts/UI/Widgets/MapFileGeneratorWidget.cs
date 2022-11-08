using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class MapFileGeneratorWidget : MonoBehaviour
{
    // Events
    public static Action<string, int, int, MapFileGeneratorDataSO> NewMapFile;

    // Serialized
    [Header("Name")]
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_Text _placeholderName;
    [Header("X")]
    [SerializeField] private TMP_InputField _sizeXInput;
    [SerializeField] private TMP_Text _placeholderSizeX;
    [Header("Y")]
    [SerializeField] private TMP_InputField _sizeYInput;
    [SerializeField] private TMP_Text _placeholderSizeY;

    [Header("Map Generation Data")]
    [SerializeField] private MapFileGeneratorDataSO _generateData;

    // Variables
    private string _name;
    private int _sizeX;
    private int _sizeY;

    private void Start()
    {
        // Assign default values.
        _name = _placeholderName.text;
        _sizeX = Int32.Parse(_placeholderSizeX.text);
        _sizeY = Int32.Parse(_placeholderSizeY.text);
    }

    public void NameEdited() =>
        _nameInput.text = MapFileNameValidator.Validate(_nameInput.text);

    public void AddMapFile()
    {
        if (_nameInput.text != "")
            _name = _nameInput.text;

        // If default name wasn't changed. Validate it.
        else _name = MapFileNameValidator.Validate(_name);

        if (_sizeXInput.text != "")
            _sizeX = Int32.Parse(_sizeXInput.text);

        if (_sizeYInput.text != "")
            _sizeY = Int32.Parse(_sizeYInput.text);

        NewMapFile(_name, _sizeX, _sizeY, _generateData);

        EventSystem.current.SetSelectedGameObject(null);
    }
}
