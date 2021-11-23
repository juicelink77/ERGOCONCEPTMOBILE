using UnityEngine;

public class SafeAreaDispatcher : MonoBehaviour
{
    private static SafeAreaDispatcher instance;

    public static bool HasInstance => instance != null;

    public static SafeAreaDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("SafeAreaDispatcher");
                instance = go.AddComponent<SafeAreaDispatcher>();
            }

            return instance;
        }
    }

    private Rect safeArea;
    private ScreenOrientation lastOrientation;
    private Vector2Int lastResolution;

    public Rect SafeArea => safeArea;

    public delegate void ApplySafeArea();

    public event ApplySafeArea OnApplySafeArea;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        SaveOrientation();
        SaveResolution();
        SaveSafeArea();
    }

    private void SaveOrientation()
    {
        lastOrientation = Screen.orientation;
    }

    private void SaveResolution()
    {
        lastResolution.x = Screen.width;
        lastResolution.y = Screen.height;
    }

    private void SaveSafeArea()
    {
        safeArea = Screen.safeArea;
    }

    private void Update()
    {
        var apply = false;

        if (Screen.safeArea != safeArea)
        {
            SaveSafeArea();
            apply = true;
        }

        if (Application.isMobilePlatform && Screen.orientation != lastOrientation)
        {
            SaveOrientation();
            apply = true;
        }

        if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
        {
            apply = true;
            SaveResolution();
        }

        if (!apply)
            return;

        OnApplySafeArea?.Invoke();
    }
}