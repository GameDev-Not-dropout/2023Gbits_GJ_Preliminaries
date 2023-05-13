using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class SubGameFlow : MonoBehaviour
{

    public List<SpriteRenderer> floors;
    public List<SpriteRenderer> transparentFloors;
    public GameObject[] uiObj;
    public RawImage[] rawVideo;
    public SpriteRenderer player;

    public float duration;
    public float transparentRatio = 0.3f;


    private void Start()
    {
        player.enabled = false;
        foreach (var item in floors)
        {
            item.DOFade(0, 0.1f);
        }
        foreach (var item in transparentFloors)
        {
            item.DOFade(0, 0.1f);
        }

        StartCoroutine(CanvasFade());
    }

    IEnumerator CanvasFade()
    {
        yield return new WaitForSeconds(duration * 2);
        FadeIn();
    }


    void FadeIn()
    {
        foreach (var item in floors)
        {
            item.DOFade(1, duration);
        }
        foreach (var item in transparentFloors)
        {
            item.DOFade(transparentRatio, duration).OnComplete(()=> player.enabled = true);
        }
        foreach (var item in uiObj)
        {
            item.SetActive(true);
        }

        foreach (var item in rawVideo)
        {
            item.DOFade(0, duration).OnComplete(() => item.gameObject.SetActive(false));
        }
    }



}
