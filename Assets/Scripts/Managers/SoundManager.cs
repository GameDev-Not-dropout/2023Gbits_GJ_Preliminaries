using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicAudioSource;
    public AudioSource soundAudioSource;
    public float fadeInOutTime = 1;

    [Header("音效文件")] public List<AudioClip> SE_Clips;
    [Header("背景音文件")] public List<AudioClip> BGM_Clips;

    #region 音量的开与关
    private bool musicOn;
    public bool MusicOn
    {
        get { return musicOn; }
        set
        {
            musicOn = value;
            this.MusicMute(!musicOn);
        }
    }
    private bool soundOn;
    public bool SoundOn
    {
        get { return soundOn; }
        set
        {
            soundOn = value;
            this.SoundMute(!soundOn);
        }
    }
    #endregion


    #region 音量大小
    private int musicVolume;
    public int MusicVolume
    {
        get { return musicVolume; }
        set
        {
            if (musicVolume != value)    // 如果值没动过就啥也不干
            {
                musicVolume = value;
                if (musicOn) this.SetVolume("MusicVolume", musicVolume);    // 静音模式下不会保存音量值
            }
        }
    }

    private int soundVolume;
    public int SoundVolume
    {
        get { return soundVolume; }
        set
        {
            if (soundVolume != value)
            {
                soundVolume = value;
                if (soundOn) this.SetVolume("SoundVolume", soundVolume);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.MusicVolume = SoundConfig.MusicVolume;
        this.SoundVolume = SoundConfig.SoundVolume;
        this.MusicOn = SoundConfig.MusicOn;
        this.SoundOn = SoundConfig.SoundOn;
    }

    public void MusicMute(bool mute) => this.SetVolume("MusicVolume", mute ? 0 : musicVolume);
    public void SoundMute(bool mute) => this.SetVolume("SoundVolume", mute ? 0 : soundVolume);

    /// <summary>
    /// 修改音量，从分贝映射为音量值
    /// </summary>
    private void SetVolume(string name, int value)
    {
        float volume = value * 0.5f - 40f;
        this.audioMixer.SetFloat(name, volume);     // 调用混音器方法，修改指定变量的值
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    public void PlayMusic(BGM bgm)
    {
        SwitchBgm(BGM_Clips[(int)bgm]);
    }
    public void SwitchBgm(AudioClip clip)
    {
        StartCoroutine(BgmFadeInFadeOut(clip));
    }
    IEnumerator BgmFadeInFadeOut(AudioClip clip)
    {
        var fadeOutTime = fadeInOutTime * 0.5f;
        while (fadeOutTime > 0)
        {
            musicAudioSource.volume = fadeOutTime / fadeInOutTime * 2;
            fadeOutTime -= Time.deltaTime;
            yield return null;
        }
        var fadeInTime = fadeInOutTime * 0.5f;
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
        while (fadeInTime > 0)
        {
            musicAudioSource.volume = 1 - fadeInTime / fadeInOutTime * 2;
            fadeInTime -= Time.deltaTime;
            yield return null;
        }
        musicAudioSource.volume = 1;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(SE se)
    {
        soundAudioSource.PlayOneShot(SE_Clips[(int)se]);     // 音效只播放一次
    }





}
