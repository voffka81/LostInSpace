using UnityEngine;

public class ShopingContainer : BaseInteractableObject
{
    [SerializeField]
    private ContainerSO _containerSO;

    protected override void PrepareMenuActions()
    {
        if (!_player.IsHoldContainerItem())
            _menuActions[RadialMenuActions.Take].IsEnabled = true;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        var transform = Instantiate(_containerSO.prefab, _interactionPoint);
        var containerItem = transform.GetComponent<ContainerItem>();
        if (containerItem == null)
        {
            Debug.LogError("Container Item is null");
            return;
        }
        _player.SetContainerItem(containerItem);
    }
}
