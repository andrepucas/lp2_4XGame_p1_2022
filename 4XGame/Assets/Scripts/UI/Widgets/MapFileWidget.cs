using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Responsible for each file widget that can be found in the map files
/// scrollable list.
/// </summary>
public class MapFileWidget : MonoBehaviour
{
    // Events
    public static event Action<MapFileWidget> OnSelected;
    public static event Action OnDeleted;

    // Readonly
    private readonly Color32 NORMAL_COLOR = new Color32(255, 50, 100, 255);
    private readonly Color32 SELECTED_COLOR = new Color32(255, 200, 100, 255);

    // Serialized
    [Header("Components")]
    [SerializeField] private Button _widgetButton;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _sizeXDisplay;
    [SerializeField] private TMP_InputField _sizeYDisplay;
    [SerializeField] private Button _editNameButton;

    // Properties
    public MapData MapData {get; private set;}

    // Variables
    private ColorBlock _buttonColors;
    private int _editNameToggleIndex;
    private string _nameBeforeEdit;

    public void Initialize(MapData p_mapData)
    {
        MapData = p_mapData;
        _nameInput.text = MapData.Name;
        _sizeXDisplay.text = p_mapData.Dimensions_X.ToString();
        _sizeYDisplay.text = p_mapData.Dimensions_Y.ToString();
    }

    private void Start()
    {
        _nameInput.interactable = false;
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
        if (_nameInput.interactable)
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

    public void Select()
    {
        _widgetButton.interactable = false;

        OnSelected?.Invoke(this);
    }

    public void DeSelect()
    {
        _widgetButton.interactable = true;
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
            // If the input field is still enabled.
            if (_nameInput.interactable)
            {
                // Increment again, since it was just incremented on NameEdited()
                // but the user is disabling edit mode by pressing this button again.
                _editNameToggleIndex++;
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            // Enable.
            _nameBeforeEdit = _nameInput.text;
            _nameInput.interactable = true;
            _nameInput.Select();
        }

        UpdateEditNameButtonColors();
    }

    /// <summary>
    /// Called when the input field leaves edit-mode.
    /// This happens when the player clicks away, escape or enter.
    /// Disables input-field directly.
    /// </summary>
    public void NameEdited()
    {
        // Fail-proof new name (if different) by removing all illegal file characters.
        if (_nameBeforeEdit != _nameInput.text)
            _nameInput.text = MapFileNameValidator.Validate(_nameInput.text);

        // Update File name and Map Data name.
        MapFilesBrowser.RenameMapFile(MapData.Name, _nameInput.text);
        MapData.Name = _nameInput.text;

        // Disable name input field after a short delay.
        _editNameToggleIndex++;
        StartCoroutine(DisableAfterDelay());
    }

    /// <summary>
    /// Waits some time before disabling the input field.
    /// This prevents:
    /// 1. An event system conflict when disabling the input and clicking 
    /// somewhere else at the same time.
    /// 2. Allows the edit button method to detect it as enabled still, thus
    /// allowing it to be properly handled when the user tries to leave edit mode
    /// by pressing the button.
    /// </summary>
    /// <returns>WaitForSeconds(.1f)</returns>
    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(.1f);

        _nameInput.interactable = false;
        UpdateEditNameButtonColors();
    }

    /// <summary>
    /// Called by delete button.
    /// Deletes this file widget and it's corresponding map file from the directory.
    /// </summary>
    public void Delete()
    {
        MapFilesBrowser.DeleteMapFile(MapData.Name);
        OnDeleted?.Invoke();
        Destroy(this.gameObject, .1f);
    }

    public override int GetHashCode() => MapData.Name.GetHashCode();

    public override bool Equals(object p_obj) => 
        (p_obj as MapFileWidget)?.MapData.Name == MapData.Name;
}
