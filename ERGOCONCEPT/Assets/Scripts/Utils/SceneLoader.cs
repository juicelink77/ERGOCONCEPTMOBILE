using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    public Canvas Canvas;
    public Image image;
    public CanvasGroup CanvasGroup;
    public bool BypassTransition = false;
    private string sceneNameToGo;
    private float fadeTimer = 1f;

    public UnityEvent OnSceneInit;

    private IEnumerator FadeOut()
    {
        while (CanvasGroup.alpha > 0)
        {
            CanvasGroup.alpha -= Time.deltaTime / fadeTimer;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        while (CanvasGroup.alpha < 1)
        {
            CanvasGroup.alpha += Time.deltaTime / fadeTimer;
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
        OnSceneInit.Invoke();
    }

    public void GotoScene(string sceneName)
    {
        sceneNameToGo = sceneName;
        if (BypassTransition)
        {
            OnFadeInFinished();
        }
        else
        {
            StartCoroutine(LaunchTransition());
        }
    }

    private IEnumerator LaunchTransition()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTimer);
        OnFadeInFinished();
        //StartCoroutine(FadeOut());
    }

    private void OnFadeInFinished()
    {
        SceneManager.LoadScene(sceneNameToGo);
    }
}