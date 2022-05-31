using UnityEngine;

public class Core : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor] Editor values
    [SerializeField] private InputManager InputManagerReference;
    [SerializeField] private ScoreManager ScoreManagerReference;
    [SerializeField] private SceneManager SceneManagerReference;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Properties]
    public static InputManager InputManager => Instance.InputManagerReference;
    public static ScoreManager ScoreManager => Instance.ScoreManagerReference;
    public static SceneManager SceneManager => Instance.SceneManagerReference;
    public static FlappyFish ActiveFlappyFish => Instance._flappyFishReference;
    public static LevelController ActiveLevelController => Instance._levelController;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - Private]
    private static Core _instance;
    private FlappyFish _flappyFishReference;
    private LevelController _levelController;
    private Camera _mainCamera;
    // ------------------------------------------------------------------------------------------------------------------------------
    public static Core Instance
    {
        get
        {
            return _instance;
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
            InitGameSystems();
            _mainCamera = Camera.main;
        }
        else
        {
            Destroy(this);
        }

    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public static void SetActiveFish(FlappyFish flappyFish)
	{
        Instance._flappyFishReference = flappyFish;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public static void SetActiveLevelController(LevelController levelController)
    {
        if (Instance._levelController != null)
		{
            throw new System.Exception("Level Controller already set!");
		}

        Instance._levelController = levelController;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void InitGameSystems()
	{
        InitGameSettings();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ShowCursor(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ToggleCursor()
    {
        bool isCurrentlyVisible = Cursor.visible;
        Cursor.lockState = isCurrentlyVisible ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCurrentlyVisible;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    // Should be a separate class?
    public void InitGameSettings()
	{
        ShowCursor(true); // ? (GameSettings.ShowCursor);
        //Application.targetFrameRate = 144;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
