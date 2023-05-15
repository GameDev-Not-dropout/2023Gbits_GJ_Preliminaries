using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FinalVideoController : MonoBehaviour
{
    public VideoPlayer video;

    private void Start()
    {
        video.loopPointReached += BackToTitle;

    }


    void BackToTitle(VideoPlayer video)
    {
        SceneFadeManager.instance.ChangeScene(0);

    }








}
