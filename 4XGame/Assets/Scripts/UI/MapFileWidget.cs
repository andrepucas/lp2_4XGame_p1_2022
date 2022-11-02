using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// Responsible for each file widget that can be found in the map files
/// scrollable list.
/// </summary>
public class MapFileWidget : MonoBehaviour
{
    // Readonly
    private readonly Color32 NORMAL_COLOR = new Color32(255, 50, 100, 255);
    private readonly Color32 SELECTED_COLOR = new Color32(255, 200, 100, 255);
    private readonly Regex ILLEGAL_CHARS = new Regex("[#%&{}\\<>*?/$!'\":@+`|= ]");

    // Serialized
    [Header("Components")]
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private Button _editNameButton;

    // Variables
    private MapData _mapData;
    private ColorBlock _buttonColors;
    private int _editNameToggleIndex;

    public void Initialize(MapData p_mapData)
    {
        _mapData = p_mapData;
        _name.text = _mapData.Name;
    }

    private void Start()
    {
        _name.interactable = false;
        _buttonColors = _editNameButton.colors;

        UpdateEditNameButtonColors();
        _editNameToggleIndex = 0;
    }

    /// <summary>
    /// Changes colors of the edit name button manually, thus allowing it to be
    /// 'pressed' or 'selected' while the user edits the file name.
    /// </summary>
    private void UpdateEditNameButtonColors()
    {
        // If the name input field is active.
        if (_name.interactable)
        {
            _buttonColors.normalColor = SELECTED_COLOR;
            _buttonColors.pressedColor = SELECTED_COLOR;
            _buttonColors.selectedColor = SELECTED_COLOR;
        }

        // If its disabled.
        else 
        {
            _buttonColors.normalColor = NORMAL_COLOR;
            _buttonColors.pressedColor = NORMAL_COLOR;
            _buttonColors.selectedColor = NORMAL_COLOR;
        }

        _editNameButton.colors = _buttonColors;
    }

    /// <summary>
    /// Called on click of the edit name button. 
    /// Disables and enables the name input field.
    /// </summary>
    public void ToggleEditName()
    {
        _editNameToggleIndex++;

        // If its an odd number.
        if (_editNameToggleIndex % 2 != 0)
        {
            // Enable.
            _name.interactable = true;
            _name.Select();
        }

        else
        {
            // Disable input field.
            _name.interactable = false;
        }

        UpdateEditNameButtonColors();
    }

    /// <summary>
    /// Called when the input field is 
    /// </summary>
    public void NameEdited()
    {
        // Fail-proof new name by removing all illegal file characters.
        _name.text = ILLEGAL_CHARS.Replace(_name.text, "_");
    }
}
