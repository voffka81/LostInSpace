using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;

public abstract class BaseInteractableObject : MonoBehaviour
{
    [SerializeField]
    public Transform _interactionPoint;

    protected Dictionary<RadialMenuActions, RadialMenuActionDescription> _menuActions = new();
    private RadialMenuActions _selectedAction;
    protected Player _player;
    private InteractionStatus _currentStatus = InteractionStatus.None;

    private void Start()
    {

        _menuActions = new Dictionary<RadialMenuActions, RadialMenuActionDescription>
        {
            { RadialMenuActions.Buy, new RadialMenuActionDescription() { Description = "Buy", IsEnabled = false } },
            { RadialMenuActions.Sleep, new RadialMenuActionDescription() { Description = "Sleep", IsEnabled = false } },
            { RadialMenuActions.Talk, new RadialMenuActionDescription() { Description = "Talk", IsEnabled = false } },
            { RadialMenuActions.Put, new RadialMenuActionDescription() { Description = "Put", IsEnabled = false } },
            { RadialMenuActions.Take, new RadialMenuActionDescription() { Description = "Take", IsEnabled = false } },
            { RadialMenuActions.Work, new RadialMenuActionDescription() { Description = "Work", IsEnabled = false } },
            { RadialMenuActions.Eat, new RadialMenuActionDescription() { Description = "Eat", IsEnabled = false } },
            { RadialMenuActions.Open, new RadialMenuActionDescription() { Description = "Open", IsEnabled = false } },
            { RadialMenuActions.Enter, new RadialMenuActionDescription() { Description = "Enter", IsEnabled = false } },
            { RadialMenuActions.Cancel, new RadialMenuActionDescription() { Description = "Cancel", IsEnabled = true } },
            { RadialMenuActions.Learn, new RadialMenuActionDescription() { Description = "Learn", IsEnabled = false} },
        };
    }

    public async UniTask<InteractionStatus> ShowPopupMenu(Player player)
    {
        _player = player;
        PrepareMenuActions();
        var filteredActions = _menuActions.Where(x => x.Value.IsEnabled).ToDictionary(i => i.Key, i => i.Value);
        var result=await GameManager.Instance.UI.ShowItemPopupMenu(filteredActions);
        if (result == RadialMenuActions.Cancel)
        {
            _currentStatus = InteractionStatus.Complete;
        }
        else
        {
            _selectedAction = result;
            _currentStatus = InteractionStatus.InProgress;
        }
        return _currentStatus;
    }

    public TaskStatus Interact()
    {
        InteractAction(_selectedAction);
        return TaskStatus.Complete;
    }

    protected abstract void InteractAction(RadialMenuActions interactAction);

    protected abstract void PrepareMenuActions();
}
