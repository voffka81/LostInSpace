using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContainerItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _price;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    public Button _removeButton;

    public void SetItem(SellableItemSO item)
    {
        _name.text = item.ItemName;
        _price.text = $"{item.Price}$";
        _icon.sprite = item.Icon;
    }
}
