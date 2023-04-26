using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float canvasDuration;
    public float scaler;
    public List<SpriteRenderer> floors;
    public Image settingButtonImage;
    public float duration;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        foreach (var item in floors)
        {
            item.DOFade(0, 0.1f);
        }
        settingButtonImage.DOFade(0, 0.1f);

        StartCoroutine(CanvasFade());
    }

    void FadeIn()
    {
        foreach (var item in floors)
        {
            item.DOFade(1, duration);
        }
        settingButtonImage.DOFade(1, duration);
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

    IEnumerator CanvasFade()
    {
        yield return Fade(1);
        yield return new WaitForSeconds(canvasDuration);
        yield return Fade(0);
        FadeIn();
    }








}
