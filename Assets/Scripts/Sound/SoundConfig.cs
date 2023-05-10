using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundConfig
{
    public static bool MusicOn
    {
        get
        {
            return PlayerPrefs.GetInt("Music", 1) == 1;     // 获取值
        }
        set
        {
            PlayerPrefs.SetInt("Music", value ? 1 : 0);     // 保存
            SoundManager.Instance.MusicOn = value;      // 修改音量值
        }

    }

    public static bool SoundOn
    {
        get
        {
            return PlayerPrefs.GetInt("Sound", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("Sound", value ? 1 : 0);
            SoundManager.Instance.SoundOn = value;
        }
    }

    public static int MusicVolume
    {
        get
        {
            return PlayerPrefs.GetInt("MusicVolume", 50);
        }

        set
        {
            PlayerPrefs.SetInt("MusicVolume", value);
            SoundManager.Instance.MusicVolume = value;
        }
    }
    public static int SoundVolume
    {
        get
        {
            return PlayerPrefs.GetInt("SoundVolume", 50);
        }

        set
        {
            PlayerPrefs.SetInt("SoundVolume", value);
            SoundManager.Instance.SoundVolume = value;
        }
    }

    /// <summary>
    /// 析构函数，脚本销毁时自动保存一次
    /// </summary>
    ~SoundConfig()
    {
        PlayerPrefs.Save();
    }
}
