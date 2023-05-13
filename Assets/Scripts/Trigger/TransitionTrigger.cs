using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTrigger : MonoBehaviour
{
    Camera mainCamera;
    public Camera sceneCamera2;
    bool isFromLeft;
    int chapterIndex;

    private void Start()
    {
        mainCamera = Camera.main;
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            chapterIndex = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            chapterIndex = 2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            chapterIndex = 3;
        }
    }
    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnTransitionTriggerEnter, OnTransitionTriggerEnter);
        EventSystem.Instance.AddEventListener(EventName.OnChangeCamera, ()=> isFromLeft = !isFromLeft);

    }
    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnTransitionTriggerEnter, OnTransitionTriggerEnter);
        EventSystem.Instance.RemoveEventListener(EventName.OnChangeCamera, () => isFromLeft = !isFromLeft);

    }

    private void OnTransitionTriggerEnter(Transform playerTrans)
    {
        if (playerTrans.position.x < transform.position.x)
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
                if (chapterIndex == 1)
                {
                    EventSystem.Instance.EmitEvent(EventName.OnChangeStyle);
                }
                TransitionManager.Instance.SpawnShockWaves(pos, 2);
                collision.transform.position = pos;
                SoundManager.Instance.PlaySound(SE.overLine);
                return; 
            }
            else    // 从线右边跳转到线左边：即从B跳到A
            {
                TransitionManager.Instance.SpawnShockWaves(pos, 1);
                pos.x = pos.x - 80f;
                if (chapterIndex == 1)
                {
                    EventSystem.Instance.EmitEvent(EventName.OnChangeStyle);
                }
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
