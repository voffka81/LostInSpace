using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JobItemUITemplate : MonoBehaviour,  IDialogItemUI ,IPointerEnterHandler
{
    [SerializeField]
    private TextMeshProUGUI _descreiption;
    [SerializeField]
    private TextMeshProUGUI _sallary;
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Button _button;

    private DialogOptionsUI _parent;
    private JobInfoSO _item;
    public IDialogOption Item=> _item;

    public void SetItem(DialogOptionsUI parent,IDialogOption item)
    {
        _item=  item as JobInfoSO;
        _parent = parent;
        _descreiption.text = _item.Description;
        _sallary.text = $"{_item.Salary}$";
        _icon.sprite = item.Icon;
        _button.enabled = Player.Instance.Education>= _item.MinimumEducationSkill;
        _button.onClick.AddListener(() => {
            if (_button.enabled)
            {
                _parent.OnItemSelected(this);
            }
        });
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_button.enabled) { print("Not enough education"); }
    }
}
