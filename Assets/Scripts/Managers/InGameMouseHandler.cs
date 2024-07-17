using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InGameMouseHandler : UnityEngine.Object
{
    private LayerMask _selectableLayerMask;

    private LayerMask _walkableLayerMask;

    private WaypointVisual _waypointVisual;

    private Ray _ray;
    private Camera _camera;

    private BaseInteractableObject _selectedObject;

    public InGameMouseHandler(Camera camera)
    {
        _camera = camera;
        _selectableLayerMask = LayerMask.GetMask("Selectable");
        _walkableLayerMask = LayerMask.GetMask("Walking");

        var waypointPrefab = Resources.Load("WayPointSign", typeof(WaypointVisual)) as WaypointVisual;
        _waypointVisual = Instantiate(waypointPrefab, GameManager.Instance.transform);

    }

    private void ClickToMove(bool isClickOnGui)
    {
        if (isClickOnGui)
        {
           
            GameManager.Instance.UI.ClosePopupMenu();

            if (_selectedObject != null)
            {
                _waypointVisual.SetWaypoint(_selectedObject._interactionPoint.position);
                Player.Instance.Interact(_selectedObject);
            }
            else
            {
                if (Physics.Raycast(_ray, out RaycastHit hit, 100f, _walkableLayerMask))
                {
                    _waypointVisual.SetWaypoint(hit.point);
                    Player.Instance.GoToPoint(_waypointVisual);
                }
            }
        }
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ClickToMove(!EventSystem.current.IsPointerOverGameObject());
        }
        _ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(_ray, out var mouseRaycastHit, 100f, _selectableLayerMask))
        {
            mouseRaycastHit.transform.TryGetComponent(out _selectedObject);
            if (_selectedObject != null)
            {
                _selectedObject.GetComponent<Hightlight>()?.ToggleHighlight(true);
                return;
            }
        }
        if (_selectedObject != null)
        {
            _selectedObject.GetComponent<Hightlight>()?.ToggleHighlight(false);
            _selectedObject = null;
        }
    }
}