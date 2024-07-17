using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraSystem _cameraSystem;
    [SerializeField]
    private UISystem _uiSystem;


    private bool _isPause;
    private InputSystem _inputSystem;
    private SceneManager _sceneManager;
    private TimeSystem _timeSystem;
    private InGameMouseHandler _gameMouseHandler;
    private BuildingManager _buildingManager;

    public BuildingManager BuildingSystem => _buildingManager;
    public InputSystem Input => _inputSystem;
    public SceneManager Scene => _sceneManager;
    public TimeSystem Time => _timeSystem;
    public UISystem UI => _uiSystem;

    public CameraSystem Camera => _cameraSystem;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _buildingManager = new BuildingManager();
            _inputSystem = new InputSystem();
            _sceneManager = new SceneManager();
            _timeSystem = new TimeSystem();
            _gameMouseHandler = new InGameMouseHandler(_cameraSystem.MainCamera);
        }
    }
    private void OnEnable()
    {
        Instance._inputSystem.Enable();
    }

    private void OnDisable()
    {
        Instance._inputSystem.Disable();
    }

    public void Pause()
    {
        Instance._isPause = true;
    }

    internal void Resume()
    {
        Instance._isPause = false;
    }

    private void Update()
    {
        if (!Instance._isPause)
        {
            Instance._gameMouseHandler.Update();
            Instance._timeSystem.UpdateTime();
        }
    }
}
