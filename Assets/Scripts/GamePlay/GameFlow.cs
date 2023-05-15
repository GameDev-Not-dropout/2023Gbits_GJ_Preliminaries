using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public GameObject player;
    public GameObject[] uiObj;
    public Camera mainCamera;
    public GameObject guidePanel;
    public RawImage[] rawVideo;

    public float canvasDuration;
    public float scaler;
    public List<SpriteRenderer> floors;
    public List<SpriteRenderer> transparentFloors;
    public Image[] buttonImages;
    public float duration;
    public float transparentRatio = 0.3f;
    public float cameraMoveDuration;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        foreach (var item in floors)
        {
            item.DOFade(0, 0f);
        }
        foreach (var item in transparentFloors)
        {
            item.DOFade(0, 0f);
        }
        foreach (var item in buttonImages)
        {
            item.DOFade(0, 0f);
        }

        StartCoroutine(CanvasFade());
    }

    void FadeIn()
    {
        foreach (var item in transparentFloors)
        {
            item.DOFade(transparentRatio, duration);
        }
        foreach (var item in uiObj)
        {
            item.SetActive(true);
        }

        foreach (var item in rawVideo)
        {
            item.DOFade(0, duration).OnComplete(() => item.gameObject.SetActive(false));
        }
        foreach (var item in buttonImages)
        {
            item.DOFade(1, duration);
        }
        foreach (var item in floors)
        {
            item.DOFade(1, duration).OnComplete(() => { guidePanel.SetActive(true); Time.timeScale = 0f; });
        }
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
        yield return new WaitForSeconds(canvasDuration / 2);
        yield return Fade(1);       // 出字
        yield return new WaitForSeconds(canvasDuration);
        yield return Fade(0);       // 消字
        // 摄像机放大到原尺寸
        DOTween.To((value) => { mainCamera.orthographicSize = value; }, mainCamera.orthographicSize, 12f, cameraMoveDuration).SetEase(Ease.OutCubic);
        mainCamera.transform.DOMove(new Vector3(0, 0, -10), cameraMoveDuration).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(cameraMoveDuration);
        // 人物出现
        player.GetComponent<SpriteRenderer>().enabled = true;
        FadeIn();
    }








}
