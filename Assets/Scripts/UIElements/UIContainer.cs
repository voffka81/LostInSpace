using UnityEngine;

public class UIContainer : MonoBehaviour
{

    [SerializeField]
    private Transform _container;
    [SerializeField]
    private Transform _itemsList;
    [SerializeField]
    private Transform _itemUITemplate;

    private ContainerItem _playerContainer;

    private void Awake()
    {
        _container.gameObject.SetActive(false);
    }

    private void Start()
    {
        Player.Instance.OnContainerChanged += OnContainerChanged;
    }

    private void OnContainerChanged(object sender, bool e)
    {
        if (e)
        {
            _container.gameObject.SetActive(true);
            _playerContainer = Player.Instance.GetContainerItem();
            _playerContainer.OnItemsChange += OnItemsChange;
        }
        else
        {
            _container.gameObject.SetActive(false);
            _playerContainer.OnItemsChange -= OnItemsChange;
        }
    }

    private void OnItemsChange(object sender, System.EventArgs e)
    {
        ClearObject();

        var playerItemsList = _playerContainer.GetItems();
        for (int count = 0; count < playerItemsList.Count; count++)
        {
            var itemUI = Instantiate(_itemUITemplate, _itemsList);
            itemUI.gameObject.SetActive(true);
            var item = itemUI.GetComponent<UIContainerItem>();
            item.SetItem((SellableItemSO)playerItemsList[count]);
            RemoveButton(playerItemsList[count], item);

        }
    }

    private void RemoveButton(BaseItemSO item, UIContainerItem UIItem)
    {
        UIItem._removeButton.onClick.AddListener(() =>
        {
            _playerContainer.Remove(item);
        });
    }

    private void ClearObject()
    {
        foreach (Transform child in _itemsList)
        {
            if (child == _itemUITemplate) continue;
            Destroy(child.gameObject);
        }
    }
}
