using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelectPanel : MonoBehaviour
{
    public Button[] levelButton;
    public int unLockedLevelIndex;

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
        SceneManager.LoadScene(index);
    }










}