using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    [SerializeField]
    private CinemachineVirtualCamera _camera;
    [SerializeField]
    private float _fieldOfViewMax = 60f;
    [SerializeField]
    private float _fieldOfViewMin = 10f;
    [SerializeField]
    private float _followOffsetMin = 0.1f;
    [SerializeField]
    private float _followOffsetMax = 10f;
    [SerializeField]
    private float _followOffsetMinY = 1f;
    [SerializeField]
    private float _followOffsetMaxY = 10f;
    [SerializeField]
    private bool _useEdgeScrolling;
    [SerializeField]
    private bool _useMouseDrag;
    [SerializeField]
    private bool _useMouseRotate;

    private Vector3 _followOffset;
    [SerializeField]
    private float _rotateSpeed = 100f;
    [SerializeField]
    private float _moveSpeed = 25f;
    [SerializeField]
    private int _edgeScrollSize = 20;
    [SerializeField]
    private float _zoomSpeed = 2f;
    [SerializeField]
    private float _zoomAmount = 3f;
    private float _targetFieldOfView = 60f;
    private CinemachineTransposer _cinemachineTransposer;
    private Bounds _worldBounds;

    private void Awake()
    {
        _cinemachineTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        _followOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Start()
    {
        ResetToPlayerPosition();
    }

    public void ResetToPlayerPosition()
    {
        Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.InstanceID);

        _worldBounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; ++i)
            _worldBounds.Encapsulate(renderers[i].bounds);

        transform.position = Player.Instance.transform.position;
    }

    private void Update()
    {
        var cameraMove = GameManager.Instance.Input.CameraAction.Move.ReadValue<Vector2>();

        if (cameraMove.sqrMagnitude > 0.1f)
        {
            HandleCameraMovement(cameraMove);
        }

        float rotateDir = GameManager.Instance.Input.CameraAction.Rotate.ReadValue<Vector2>().x;
        if (rotateDir != 0)
        {
            HandleCameraRotation(rotateDir, _rotateSpeed);
        }

        float zoomAmount = GameManager.Instance.Input.CameraAction.Zoom.ReadValue<Vector2>().y;
        if (zoomAmount != 0)
        {
            HandleCameraZoom_MoveY(zoomAmount);
        }

        if (_useEdgeScrolling)
        {
            EdgeScrollingMovement();
        }

    }

    private void HandleCameraZoom_MoveForward()
    {
        Vector3 zoomDir = _followOffset.normalized;
        if (Input.mouseScrollDelta.y > 0)
            _followOffset -= zoomDir * _zoomAmount;
        if (Input.mouseScrollDelta.y < 0)
            _followOffset += zoomDir * _zoomAmount;

        if (_followOffset.magnitude < _followOffsetMin)
            _followOffset = zoomDir * _followOffsetMin;

        if (_followOffset.magnitude > _followOffsetMax)
            _followOffset = zoomDir * _followOffsetMax;

        Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _followOffset, Time.deltaTime * _zoomSpeed);

        _cinemachineTransposer.m_FollowOffset = _followOffset;
    }

    private void HandleCameraZoom_MoveY(float _zoomAmount)
    {
        _followOffset.y += _zoomAmount;

        _followOffset.y = Mathf.Clamp(_followOffset.y, _followOffsetMinY, _followOffsetMaxY);

        if (_followOffset.magnitude < _followOffsetMin)
            _followOffset.y = _zoomAmount * _followOffsetMin;

        if (_followOffset.magnitude > _followOffsetMax)
            _followOffset.y = _zoomAmount * _followOffsetMax;

        Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _followOffset, Time.deltaTime * _zoomSpeed);

        _cinemachineTransposer.m_FollowOffset = _followOffset;
    }

    private void HandleCameraZoom_FOV()
    {
        if (Input.mouseScrollDelta.y > 0)
            _targetFieldOfView -= 5;
        if (Input.mouseScrollDelta.y < 0)
            _targetFieldOfView += 5;
        _targetFieldOfView = Mathf.Clamp(_targetFieldOfView, _fieldOfViewMin, _fieldOfViewMax);

        Mathf.Lerp(_camera.m_Lens.FieldOfView, _targetFieldOfView, Time.deltaTime * _zoomSpeed);
        _camera.m_Lens.FieldOfView = _targetFieldOfView;
    }

    private void HandleCameraMovement(Vector2 inputDir)
    {
        
        Vector3 moveDir = transform.forward * inputDir.y + transform.right * inputDir.x;
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    private Vector3 MousePanMovement(Vector3 inputDir)
    {
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        _dragPanMoveActive = true;
        //        _lastMousePosition = Input.mousePosition;
        //    }
        //    if (Input.GetMouseButtonUp(1)) { _dragPanMoveActive = false; }

        //    if (_dragPanMoveActive)
        //    {
        //        Vector2 mouseMovementDelta = ((Vector2)Input.mousePosition - _lastMousePosition) * _dragPanSpeed;
        //        inputDir.x = mouseMovementDelta.x;
        //        inputDir.z = mouseMovementDelta.y;

        //        _lastMousePosition = Input.mousePosition;
        //    }

        return inputDir;
    }

    private void EdgeScrollingMovement()
    {
        Vector3 inputDir = Vector3.zero;

        var mousePosition = Mouse.current.position.ReadValue();
        if (mousePosition.x < _edgeScrollSize) inputDir.x = -1f;
        if (mousePosition.y < _edgeScrollSize) inputDir.y = -1f;
        if (mousePosition.x > Screen.width - _edgeScrollSize) inputDir.x = 1f;
        if (mousePosition.y > Screen.height - _edgeScrollSize) inputDir.y = 1f;

        Vector3 moveDir = transform.forward * inputDir.y + transform.right * inputDir.x;
        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    private void OnCameraRotate(InputAction.CallbackContext context)
    {
        if (Mouse.current.middleButton.isPressed)
        {

        }
        float rotateDir = GameManager.Instance.Input.CameraAction.Rotate.ReadValue<Vector2>().x;
        //HandleCameraRotation(rotationValue, _mouseRotationSpeed);
        transform.eulerAngles += new Vector3(0, rotateDir * _rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleCameraRotation(float rotateDir, float speed)
    {
        transform.eulerAngles += new Vector3(0, rotateDir * speed * Time.deltaTime, 0);
    }
}
