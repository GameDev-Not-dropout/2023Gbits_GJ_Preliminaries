using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettingPanel : MonoBehaviour
{
    public GameObject settingPanel;

    public Toggle toggleMusic;
    public Toggle toggleSound;

    public Slider sliderMusic;
    public Slider sliderSound;


    private void Start()
    {
        this.toggleMusic.isOn = SoundConfig.MusicOn;
        this.toggleSound.isOn = SoundConfig.SoundOn;
        this.sliderMusic.value = SoundConfig.MusicVolume;
        this.sliderSound.value = SoundConfig.SoundVolume;
    }


    public void OpenSettingPanel()
    {
        SoundManager.Instance.PlaySound(SE.UIClick);
        settingPanel.SetActive(true);
    }

    public void ReLoadThisLevel()
    {
        SoundManager.Instance.PlaySound(SE.UIClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToLoginScene()
    {
        SoundManager.Instance.PlaySound(SE.UIClick);
        SceneManager.LoadScene(0);
    }


    /// <summary>
    /// 关闭界面按钮
    /// </summary>
    public void ClosePanel()
    {
        SoundManager.Instance.PlaySound(SE.UIClick);  // 播放点击按钮音效
        this.settingPanel.SetActive(false);
        PlayerPrefs.Save();          // 保存音量设置
    }

    /// <summary>
    /// 音乐音量开关
    /// </summary>
    public void MusicToogle(bool on)
    {
        SoundConfig.MusicOn = on;    // 给配置文件赋值记录状态
        SoundManager.Instance.PlaySound(SE.UIClick);    // 播放点击按钮音效

    }
    /// <summary>
    /// 声音音量开关
    /// </summary>
    public void SoundToogle(bool on)
    {
        SoundConfig.SoundOn = on;
        SoundManager.Instance.PlaySound(SE.UIClick);
    }

    public void MusicVolume(float vol)
    {
        SoundConfig.MusicVolume = (int)vol;
        PlaySound();
    }

    public void SoundVolume(float vol)
    {
        SoundConfig.SoundVolume = (int)vol;
        PlaySound();
    }



    float lastPlay = 0;
    /// <summary>
    /// 每次调节音量都要播放一下音效来表示当前音量合不合适
    /// </summary>
    private void PlaySound()
    {
        if (Time.realtimeSinceStartup - lastPlay > 0.1)
        {
            lastPlay = Time.realtimeSinceStartup;
            SoundManager.Instance.PlaySound(SE.UIClick);
        }
    }








}
