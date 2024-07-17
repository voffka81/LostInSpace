
using UnityEngine;

public class StoreContainer : BaseInteractableObject
{
    [SerializeField]
    private SellableItemSO _sellableItemSO;

    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Take].IsEnabled = _player.IsHoldContainerItem();
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        var clone = Instantiate(_sellableItemSO);

        var container = _player.GetContainerItem();
        container.AddItem(clone);
    }
}
