using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager instance;

    CanvasGroup canvasGroup;
    public float changSceneScaler;
    public float regenarationScaler;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(Fade(0, changSceneScaler));
    }


    public IEnumerator Fade(int amount, float scaler)
    {
        canvasGroup.blocksRaycasts = true;
        while (canvasGroup.alpha != amount)
        {
            switch (amount)
            {
                case 1:
                    canvasGroup.alpha += Time.deltaTime * scaler;
                    break;
                case 0:
                    canvasGroup.alpha -= Time.deltaTime * scaler;
                    break;
            }
            yield return null;
        }
        canvasGroup.blocksRaycasts = false;
    }

    public void ChangeScene(int index)
    {
        Time.timeScale = 1;
        StartCoroutine(ChangeSceneCoroutine(index));
    }

    IEnumerator ChangeSceneCoroutine(int index)
    {
        yield return Fade(1, changSceneScaler);
        yield return SceneManager.LoadSceneAsync(index);
        yield return Fade(0, changSceneScaler);
    }

    public void ReSetPlayerPosFadeIn()
    {
        StartCoroutine(Fade(1, regenarationScaler));
    }
    public void ReSetPlayerPosFadeOut()
    {
        StartCoroutine(Fade(0, regenarationScaler));
    }

    IEnumerator FadeInFadeOut()
    {
        yield return Fade(1, regenarationScaler);
        yield return Fade(0, regenarationScaler);
    }

}
