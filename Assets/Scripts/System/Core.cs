using UnityEngine;

public class Core : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor] Editor values
    [SerializeField] private InputManager InputManager;
    //[SerializeField] private HUDManager HUDManager;
    [SerializeField] private SceneHandler SceneHandler;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code] Private Variables
    private static Core _instance;
    private FlappyFish _flappyFishReference;
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
    // delete!
    public static bool Exists()
	{
        return _instance != null;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public static FlappyFish GetActivePlayer()
	{
        return Instance._flappyFishReference;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public static Camera GetMainCamera()
	{
        return Instance._mainCamera;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public static void SetActiveFish(FlappyFish flappyFish)
	{
        Instance._flappyFishReference = flappyFish;
        //Instance.InputManager.RegisterPlayer(player);
	}
    // ------------------------------------------------------------------------------------------------------------------------------
 //   public static HUDManager GetHudManager()
	//{
 //       return Instance.HUDManager;
	//}
 //   // ------------------------------------------------------------------------------------------------------------------------------
    public static InputManager GetInputManager()
	{
        return Instance.InputManager;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public static SceneHandler GetSceneHandler()
	{
        return Instance.SceneHandler;
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
