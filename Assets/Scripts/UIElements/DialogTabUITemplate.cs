using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogTabUITemplate : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Image _icon;

    public DialogCategorySO DialogOption=> _dialogOption;
    private DialogCategorySO _dialogOption;

    private DialogOptionsUI _parent;
    public void SetItem(DialogOptionsUI parent, DialogCategorySO dialogOption)
    {
        _dialogOption = dialogOption;
        _parent = parent;
        _icon.sprite = _dialogOption.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            _parent.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _parent.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            _parent.OnTabExit(this);
    }

}
