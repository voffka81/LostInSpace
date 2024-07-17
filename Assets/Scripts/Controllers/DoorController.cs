using UnityEngine;

public class DoorController : BaseInteractableObject
{
    [SerializeField]
    public IndoorSO BuildingInfo;
    [SerializeField]
    private string _scene;
    [SerializeField]
    private string _spawnPointInSceneName;
    [SerializeField]
    private string _exitName;

    protected override void PrepareMenuActions()
    {
        _menuActions[RadialMenuActions.Enter].IsEnabled = true;
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
        GameManager.Instance.BuildingSystem.BuildingInteract(BuildingInfo);
    }
}

