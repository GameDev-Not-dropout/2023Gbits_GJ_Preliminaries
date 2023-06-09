using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool hasGetKey;
    public bool isFinalLevel;

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener(EventName.OnGetKey, OnGetKey);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener(EventName.OnGetKey, OnGetKey);
    }

    void OnGetKey()
    {
        hasGetKey = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player && hasGetKey)
        {
            SoundManager.Instance.PlaySound(SE.door);
            collision.transform.GetComponentInChildren<AudioSource>().gameObject.SetActive(false);
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            if (levelIndex == PlayerPrefs.GetInt("unLockedLevelIndex", 1))
            {
                PlayerPrefs.SetInt("unLockedLevelIndex", levelIndex + 1);
            }
            if (isFinalLevel)
            {
                SoundManager.Instance.musicAudioSource.Stop();
                SceneFadeManager.instance.ChangeScene(levelIndex + 1, true);
                return;
            }
            SceneFadeManager.instance.ChangeScene(levelIndex + 1);  // 直接进入下一关
        }
    }










}
