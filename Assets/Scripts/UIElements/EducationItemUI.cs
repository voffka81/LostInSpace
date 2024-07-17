using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EducationItemUI : MonoBehaviour,  IDialogItemUI ,IPointerEnterHandler
{
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _duration;
    [SerializeField]
    private TextMeshProUGUI _price;
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Button _button;

    private DialogOptionsUI _parent;
    private EducationInfoSO _item;
    public IDialogOption Item => _item;

    public void SetItem(DialogOptionsUI parent,IDialogOption item)
    {
        _item=  item as EducationInfoSO;
        _parent = parent;
        _duration.text = _item.Duration.ToString();
        _description.text = _item.Description;
        _price.text = $"{_item.EnrollPrice}$";
        _icon.sprite = item.Icon;
        _button.enabled = true;
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
