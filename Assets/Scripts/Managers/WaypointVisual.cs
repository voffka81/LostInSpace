using UnityEngine;

public class WaypointVisual : BaseInteractableObject
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    public void SetWaypoint(Vector3 position)
    {
        _interactionPoint.position = position;
        _particleSystem.Play();
    }

    protected override void InteractAction(RadialMenuActions interactAction)
    {
    }

    protected override void PrepareMenuActions()
    {
    }
}
