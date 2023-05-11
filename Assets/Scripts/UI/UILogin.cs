using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogin : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public GameObject settingPanel;

    private void Start()
    {

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
