using UnityEngine;
using UnityEngine.Events;

public class MobileWebVersionSelector : MonoBehaviour
{
    public UnityEvent OnWebOnMobile;
    public UnityEvent OnWebOnComputer;
    public UnityEvent OnMobile;

    public bool SimulateWebGL = false;
    public bool SimulateMobile = false;

    private void OnEnable()
    {
        if (IsMobile() && IsWebGL())
        {
            OnWebOnMobile.Invoke();
        }
        else if (IsMobile() && !IsWebGL())
        {
            OnMobile.Invoke();
        }
        else
        {
            OnWebOnComputer.Invoke();
        }
    }

    private bool IsMobile()
    {
        if (SimulateMobile)
            return true;

        return Application.isMobilePlatform;
    }

    private bool IsWebGL()
    {
        if (SimulateWebGL)
            return true;
#if !UNITY_EDITOR && UNITY_WEBGL
         return true;
#endif
        return false;
    }
}