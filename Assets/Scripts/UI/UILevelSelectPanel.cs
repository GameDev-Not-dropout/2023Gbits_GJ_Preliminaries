using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelectPanel : MonoBehaviour
{
    public Button[] levelButton;
    public int unLockedLevelIndex;
    public GameObject[] chapterPanel;

    private void OnEnable()
    {
        unLockedLevelIndex = PlayerPrefs.GetInt("unLockedLevelIndex", 1);
        for (int i = 0; i < levelButton.Length; i++)
        {
            if (i >= unLockedLevelIndex)
            {
                levelButton[i].enabled = false;     // 未解锁的关卡不能点击
            }
            else
                levelButton[i].enabled = true;
        }
    }

    public void LoadLevel(int index)
    {
        SceneFadeManager.instance.ChangeScene(index);
    }

    public void ChapterSelect(int index)
    {
        for (int i = 0; i < chapterPanel.Length; i++)
        {
            if (i == index)
            {
                chapterPanel[i].SetActive(true);
            }
            else
                chapterPanel[i].SetActive(false);

        }
    }








}
