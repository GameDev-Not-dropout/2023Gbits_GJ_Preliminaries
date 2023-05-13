using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class UILogin : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public GameObject settingPanel;
    public VideoPlayer videoPlayer;
    public VideoClip continueVideo;
    public GameObject[] firstUI;
    public float openUITime;
    public float fadeDuration;
    float timer;
    bool hasOpenUI;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(BGM.Title);
        videoPlayer.loopPointReached += ChangeVideo;
    }

    private void Update()
    {
        if (!hasOpenUI)
        {
            if (timer >= openUITime)
            {
                OpenUI();
                hasOpenUI = true;
            }
            timer += Time.deltaTime;
        }
    }


    void OpenUI()
    {
        foreach (var item in firstUI)
        {
            item.SetActive(true);
            item.GetComponent<Image>().DOFade(1, fadeDuration);
        }
    }

    void ChangeVideo(VideoPlayer thisPlay)
    {
        thisPlay.clip = continueVideo;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenLevelSelectPanel()
    {
        SoundManager.Instance.PlaySound(SE.buttonClick);
        levelSelectPanel.SetActive(true);
    }
    public void OpenSettingPanel()
    {
        SoundManager.Instance.PlaySound(SE.buttonClick);
        settingPanel.SetActive(true);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Exit()
    {
        SoundManager.Instance.PlaySound(SE.buttonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }










}
