using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TransitionTrigger : MonoBehaviour
{
    Camera mainCamera;
    public Camera sceneCamera2;
    bool isFromLeft;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != Tags.T_Player)
            return;

        if (collision.transform.position.x < transform.position.x)
            isFromLeft = true;
        else
            isFromLeft = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != Tags.T_Player)
            return;

        Vector3 pos = collision.transform.position;

        if (pos.x < transform.position.x && isFromLeft == true)
            return;
        else if (pos.x > transform.position.x && isFromLeft == false)
            return;

        
        // A场景在左，B场景在右时才执行跳转，具体跳转到哪由玩家相对于线的位置来决定
        if (mainCamera.transform.position.x < 40 && sceneCamera2.transform.position.x > 40)
        {
            if (pos.x < 40)     // 从线左边跳转到线右边：即从A跳到B
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x + 80f;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                collision.transform.position = pos;
                SoundManager.Instance.PlaySound(SE.overLine);
                return; 
            }
            else    // 从线右边跳转到线左边：即从B跳到A
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x - 80f;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                collision.transform.position = pos;
                SoundManager.Instance.PlaySound(SE.overLine);
                return;
            }
        }

        // B场景在左，A场景在右时才执行跳转，具体跳转到哪由玩家相对于线的位置来决定
        if (mainCamera.transform.position.x > 40 && sceneCamera2.transform.position.x < 40)
        {
            if (pos.x < 40)     // 从线右边跳转到线左边：即从A跳到B
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x + 80f;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                collision.transform.position = pos;
                SoundManager.Instance.PlaySound(SE.overLine);
                return; 
            }
            else     // 从线左边跳转到线右边：即从B跳到A
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x - 80f;
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                collision.transform.position = pos;
                SoundManager.Instance.PlaySound(SE.overLine);
                return;
            }
        }
    }










}
