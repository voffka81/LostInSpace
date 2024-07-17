using UnityEngine;

public class SellableItem : MonoBehaviour
{
    [SerializeField]
    private SellableItemSO _sellableItemSO;

    public SellableItemSO GetSellableItemSO()
    {
        return _sellableItemSO;
    }
}
