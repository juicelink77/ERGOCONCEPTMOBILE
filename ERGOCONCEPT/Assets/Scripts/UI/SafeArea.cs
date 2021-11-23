using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        var safeAreaTransform = transform as RectTransform;
        if (safeAreaTransform == null)
        {
            enabled = false;
        }
        else
        {
            canvas = safeAreaTransform.GetComponentInParent<Canvas>()?.rootCanvas;
            enabled = canvas != null;
        }
    }

    private void OnEnable()
    {
        ApplySafeArea();
        SafeAreaDispatcher.Instance.OnApplySafeArea += ApplySafeArea;
    }

    private void OnDisable()
    {
        if(SafeAreaDispatcher.HasInstance)
            SafeAreaDispatcher.Instance.OnApplySafeArea -= ApplySafeArea;
    }

    private void ApplySafeArea()
    {
        var safeAreaTransform = transform as RectTransform;
        if (safeAreaTransform == null)
            return;

        var dispatcher = SafeAreaDispatcher.Instance;

        var pixelRect = canvas.pixelRect;
        var safeArea = dispatcher.SafeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= pixelRect.width;
        anchorMin.y /= pixelRect.height;
        anchorMax.x /= pixelRect.width;
        anchorMax.y /= pixelRect.height;

        safeAreaTransform.anchorMin = anchorMin;
        safeAreaTransform.anchorMax = anchorMax;
    }
}