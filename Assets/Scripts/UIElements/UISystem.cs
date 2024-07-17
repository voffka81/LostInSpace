using Assets.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    private TimeSliderUI _timeSliderPrefab;
    [SerializeField]
    private DialogOptionsUI _DialogUIPrefab;
    [SerializeField]
    private GameObject _blurOverlay;
    [SerializeField]
    private RadialMenuItem _radialMenuItemPrefab;

    RadialMenuItem _popupMenu;

    private void Start()
    {
        _blurOverlay.SetActive(false);
    }

    public void ShowTimeSliderDialog(string title, string description, Action onCancel, Action<TimeSpan> onConfirm)
    {
        var timeSlider = Instantiate(_timeSliderPrefab, transform);
        timeSlider.ShowTimeSliderDialog(title, description, onCancel, onConfirm);
    }

    public void ShowTabOptionsDialog(DialogSO dialogSO, Action onCancel, Action<IDialogOption> onConfirm)
    {
        var dialog = Instantiate(_DialogUIPrefab, transform);
        dialog.ShowCategoriesDialog(dialogSO,  onCancel, onConfirm);
    }

    public async UniTask<RadialMenuActions> ShowItemPopupMenu(Dictionary<RadialMenuActions, RadialMenuActionDescription> actions)
    {
        _popupMenu = Instantiate(_radialMenuItemPrefab);
        _popupMenu.transform.transform.SetParent(transform, false);
        _popupMenu.transform.position = Input.mousePosition;
        return await _popupMenu.ShowButtons(actions);
    }

    public void ClosePopupMenu()
    {
        if (_popupMenu != null)
        {
            _popupMenu.CancelAndClose();
        }
    }
    public void Freeze()
    {
        _blurOverlay.SetActive(true);
    }

    public void Unfreeze()
    {
        _blurOverlay.SetActive(false);
    }
}
