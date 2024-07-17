using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerItem : MonoBehaviour
{
    [SerializeField]
    private ContainerSO _containerSO;
    public event EventHandler OnItemsChange;

    private List<BaseItemSO> _items = new List<BaseItemSO>();

    public ContainerSO GetContainerObjectSO()
    {
        return _containerSO;
    }

    public void AddItem(BaseItemSO item)
    {
        if (_items.Count < _containerSO.MaxCapacity)
        {
            Debug.Log($"Player put to container a {item.ItemName}");
            _items.Add(item);
            OnItemsChange?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Debug.Log("Container is full");
        }
    }
    
    public void Remove(BaseItemSO item)
    {
        if (_items.Count>0)
        {
            Debug.Log($"Player remove {item.ItemName} from container");
            _items.Remove(item);
            OnItemsChange?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Container is emplty");
        }
    }

    public List<BaseItemSO> GetItems()
    {
        return _items;
    }

    public bool IsSalebleItems()
    {
        return _items.Any(x => x is SellableItemSO);
    }

    public void AddItems(List<BaseItemSO> playerItemsList)
    {
        foreach (var item in playerItemsList)
        {
            AddItem(item);
        }

    }
}
