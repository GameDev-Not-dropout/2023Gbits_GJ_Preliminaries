using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager instance;

    public CanvasGroup canvasGroup;
    public float changSceneDuration;
    public float regenarationDuration;
    public AudioSource audioSource;
    public AudioClip[] chapterAudios;
    public GameObject voiceText;

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
        StartCoroutine(Fade(0, changSceneDuration));
    }

    public void RegenarationFadeWithTween(float beginValue, float targetValue, Action onComplete)
    {
        DOTween.To((value) => canvasGroup.alpha = value, beginValue, targetValue, regenarationDuration).SetEase(Ease.Linear).OnComplete(() => onComplete.Invoke());
    }

    public void ChangSceneFadeWithTween(float beginValue, float targetValue, Action onComplete)
    {
        DOTween.To((value) => canvasGroup.alpha = value, beginValue, targetValue, changSceneDuration).SetEase(Ease.Linear).OnComplete(() => onComplete.Invoke());
    }

    public IEnumerator Fade(int amount, float scaler)
    {
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
    }

    public void ChangeScene(int index, bool isReload = false)
    {
        Time.timeScale = 1;
        StartCoroutine(ChangeSceneCoroutine(index, isReload));
    }

    private IEnumerator ChangeSceneCoroutine(int index, bool isReload = false)
    {
        canvasGroup.blocksRaycasts = true;

        yield return Fade(1, changSceneDuration);

        if (!isReload)
            SoundManager.Instance.PlayMusic((BGM)index);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1.5f);

                if (index == 1)
                {
                    voiceText.SetActive(true);
                    voiceText.GetComponent<Text>().DOFade(1, 1f);
                }

                switch (index)
                {
                    case 1:
                        audioSource.PlayOneShot(chapterAudios[0]);
                        yield return new WaitForSeconds(chapterAudios[0].length + 0.5f);
                        break;

                    case 3:
                        audioSource.PlayOneShot(chapterAudios[1]);
                        yield return new WaitForSeconds(chapterAudios[1].length + 0.5f);
                        break;

                    case 5:
                        audioSource.PlayOneShot(chapterAudios[2]);
                        yield return new WaitForSeconds(chapterAudios[2].length + 0.5f);
                        break;
                }
                if (index == 1)
                {
                    voiceText.GetComponent<Text>().DOFade(0, 1f).OnComplete(() => voiceText.SetActive(false));
                }
                operation.allowSceneActivation = true;
                yield return null;
            }
            yield return null;
        }
        yield return Fade(0, changSceneDuration);
        canvasGroup.blocksRaycasts = false;
    }
}