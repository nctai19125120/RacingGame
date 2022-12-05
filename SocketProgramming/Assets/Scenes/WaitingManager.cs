using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingManager : MonoBehaviour
{
    public static WaitingManager Instance;
    public float MinLoadTime;
    public GameObject WaitingPanel;

    public GameObject LoadingWheel;
    public float WheelSpeed;

    private bool isLoading;
    private string targetScene;

    public Image fadeImage;
    public float fadeTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        WaitingPanel.SetActive(false);
        fadeImage.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;

        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0);

        while(!Fade(1))
        {
            yield return null;
        }

        WaitingPanel.SetActive(true);
        StartCoroutine(SpinWheelRoutine());

        while(!Fade(0))
        {
            yield return null;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);

        float elapsedLoadTime = 0f; 

        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while (elapsedLoadTime < MinLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while(!Fade(1))
        {
            yield return null;
        }

        WaitingPanel.SetActive(false);

        while (!Fade(0))
            yield return null;

        isLoading = false;
    }

    private bool Fade(float target)
    {
        fadeImage.CrossFadeAlpha(target, fadeTime, true);

        if (Math.Abs(fadeImage.canvasRenderer.GetAlpha() - target) <= 0.05f)
        {
            fadeImage.canvasRenderer.SetAlpha(target);
            return true;
        }
        return false;
    }

    private IEnumerator SpinWheelRoutine()
    {
        while(isLoading)
        {
            LoadingWheel.transform.Rotate(0, 0, -WheelSpeed);
            yield return null;
        }
    }
}
