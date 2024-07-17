using Assets.Scripts.Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogOptionsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _subTitle;
    [SerializeField]
    private Button _btnCancel;
    [SerializeField]
    private Button _btnOk;

    [SerializeField]
    private Transform _itemsContainer;
    [SerializeField]
    private Transform _tabsContainer;
    [SerializeField]
    private JobItemUITemplate _jobItemUItemplate;
    [SerializeField]
    private DialogTabUITemplate _dialogTabUI;
   
    private DialogTabUITemplate _selectedTab;
    private IDialogItemUI _selectedItem;
    private DialogSO _dialogSO;

    public void ShowCategoriesDialog(DialogSO dialogSO,  Action onCancel, Action<IDialogOption> onConfirm)
    {
        GameManager.Instance.UI.Freeze();

        _dialogSO = dialogSO;

        gameObject.SetActive(true);
        _title.text = _dialogSO.Title;

        //Create Tabs
        for (int count = 0; count < _dialogSO.CategoriesSO.Count; count++)
        {
            var  dialogOption = _dialogSO.CategoriesSO[count];
            var itemUI = Instantiate(_dialogTabUI, _tabsContainer);
            itemUI.gameObject.SetActive(true);
            var template = itemUI.GetComponent<DialogTabUITemplate>();            

            template.SetItem(this, dialogOption);
            if (count== 0) {
                OnTabSelected(template);
            }
        }
       
        _btnCancel.onClick.AddListener(() =>
        {
            onCancel?.Invoke();
            Hide();
        });
        _btnOk.onClick.AddListener(() =>
        {
            onConfirm?.Invoke(_selectedItem.Item);
            Hide();
        });
    }

    public void OnTabEnter(DialogTabUITemplate button)
    {
        print($"enter to {button.DialogOption.name}");
    }
    public void OnTabSelected(DialogTabUITemplate button)
    {
        _selectedTab = button;
        _subTitle.text = _selectedTab.DialogOption.Title;
        while (_itemsContainer.childCount > 0)
        {
            DestroyImmediate(_itemsContainer.GetChild(0).gameObject);
        }
        foreach (var job in _selectedTab.DialogOption.OptionsList)
        {
            var itemUI = Instantiate(_dialogSO.UITemplate, _itemsContainer);
            itemUI.gameObject.SetActive(true);
            itemUI.GetComponent<IDialogItemUI>().SetItem(this, job);
        }
    }

    public void OnTabExit(DialogTabUITemplate button)
    {
    }

    public void OnItemSelected(IDialogItemUI button)
    {
        _selectedItem = button;
    }

    private void CloseDialog()
    {
        GameManager.Instance.UI.Unfreeze();
        Destroy(this);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        CloseDialog();
    }
}