using static InputActions;

public class InputSystem
{
    private InputActions _inputActions;
    public PlayerActions PlayerAction { get; private set; }
    public CameraActions CameraAction { get; private set; }


    public InputSystem()
    {
        _inputActions = new InputActions();

        PlayerAction = _inputActions.Player;
        CameraAction = _inputActions.Camera;
    }

    public void Enable()
    {
        _inputActions.Enable();
    }
    public void Disable()
    {
        _inputActions.Disable();
    }
}