using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegenerationManager : MonoBehaviour
{
    public static RegenerationManager Instance;
    Transform RegenerationPoint;

    Camera mainCam;
    public Camera scene2Cam;
    public Transform player;
    Vector3 mainCamPoint;
    Vector3 scene2CamPoint;
    public SpriteRenderer Left_SceneBG;
    public SpriteRenderer Right_SceneBG;
    public Sprite A_SceneSprite;
    public Sprite B_SceneSprite;
    public bool isC3;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, Regeneration);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnRegenerationPointRef, RecordRegenerationPoint);
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, Regeneration);

    }

    private void Start()
    {
        mainCam = Camera.main;
    }



    /// <summary>
    /// 记录重生点和摄像机位置
    /// </summary>
    public void RecordRegenerationPoint(Transform newRecord)
    {
        RegenerationPoint = newRecord;
        mainCamPoint = mainCam.transform.position;
        scene2CamPoint = scene2Cam.transform.position;
    }

    public void Regeneration(Transform playerPos)
    {
        if (!isC3)
        {
            SceneFadeManager.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex, true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        PlayerDieCouroutine(playerPos);
    }

    void PlayerDieCouroutine(Transform playerPos)
    {
        SceneFadeManager.instance.RegenarationFadeWithTween(0f, 1f, () => MoveCam(playerPos));
    }
    
    void MoveCam(Transform playerPos)
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (RegenerationPoint.position.x < 40)
            {
                mainCam.transform.position = new Vector3(0f, mainCam.transform.position.y, -10);
                scene2Cam.transform.position = new Vector3(80f, mainCam.transform.position.y, -10);
            }
            else
            {
                mainCam.transform.position = new Vector3(80f, mainCam.transform.position.y, -10);
                scene2Cam.transform.position = new Vector3(0f, mainCam.transform.position.y, -10);
            }
            playerPos.position = RegenerationPoint.position;

            StartCoroutine(WaitForDieBlack());
            return;
        }


        if (mainCamPoint.x < 40)
        {
            if (mainCam.transform.position.x > 40)  // 主相机从场景B移到场景A
            {
                mainCam.transform.position = new Vector3(0f, mainCam.transform.position.y, -10);
                if (Left_SceneBG != null)
                    Left_SceneBG.sprite = B_SceneSprite;  // 透明背景切换为B场景图片

                // B场景左边平台开始移动，A场景左边平台停止移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 3);
            }

        }
        else
        {
            if (mainCam.transform.position.x < 40)  // 主相机从场景A移到场景B
            {
                mainCam.transform.position = new Vector3(80f, mainCam.transform.position.y, -10);
                if (Left_SceneBG != null)
                    Left_SceneBG.sprite = A_SceneSprite;  // 透明背景切换为A场景图片
                // A场景左边平台开始移动，B场景左边平台停止移动
                EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 1);
            }

        }
        if (scene2CamPoint.x < 40 && scene2Cam.transform.position.x > 40)
        {
            EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 4);
        }
        else if (scene2CamPoint.x > 40 && scene2Cam.transform.position.x < 40)
        {
            EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 2);
        }
        scene2Cam.transform.position = scene2CamPoint;
        if (scene2Cam.transform.position.x < 40)
        {
            if (Right_SceneBG != null)
                Right_SceneBG.sprite = B_SceneSprite;  // 透明背景切换为B场景图片
        }
        else
        {
            if (Right_SceneBG != null)
                Right_SceneBG.sprite = A_SceneSprite;  // 透明背景切换为A场景图片
        }

        playerPos.position = RegenerationPoint.position;

        StartCoroutine(WaitForDieBlack());
        //if (SceneManager.GetActiveScene().buildIndex != 3 && SceneManager.GetActiveScene().buildIndex != 4)
        //    return;

        //if (RegenerationPoint.position.x < 40)      // 玩家在场景1
        //{
        //    if (RegenerationPoint.position.x < TransitionManager.Instance.TriggerAPos)    // 此时在线的左边
        //        EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 3);
        //    else
        //        EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 4);
        //}
        //else   // 玩家在场景2
        //{
        //    if (RegenerationPoint.position.x < TransitionManager.Instance.TriggerBPos)    // 此时在线的左边
        //        EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 1);
        //    else
        //        EventSystem.Instance.EmitEvent(EventName.OnChangeMoveFloor, 2);
        //}
    }

    IEnumerator WaitForDieBlack()
    {
        yield return new WaitForSeconds(2f);
        SceneFadeManager.instance.RegenarationFadeWithTween(1f, 0f, () => EventSystem.Instance.EmitEvent(EventName.OnSceneFadeEnd));
    }

}
