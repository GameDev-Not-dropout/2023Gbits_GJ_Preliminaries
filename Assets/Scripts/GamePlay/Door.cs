using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool hasGetKey;

    private void OnEnable()
    {
        EventSystem.instance.AddEventListener(EventName.OnGetKey, OnGetKey);
    }

    private void OnDisable()
    {
        EventSystem.instance.RemoveEventListener(EventName.OnGetKey, OnGetKey);
    }

    void OnGetKey()
    {
        hasGetKey = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player && hasGetKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // 直接进入下一关
        }
    }










}
