using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager instance;

    CanvasGroup canvasGroup;
    public float scaler;

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
        StartCoroutine(Fade(0));
    }


    IEnumerator Fade(int amount)
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
        yield return Fade(1);
        yield return SceneManager.LoadSceneAsync(index);
        yield return Fade(0);
    }

    public void ReSetPlayerPosition()
    {
        StartCoroutine(FadeInFadeOut());
    }

    IEnumerator FadeInFadeOut()
    {
        yield return Fade(1);
        yield return Fade(0);
    }

}
