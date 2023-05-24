using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float bottomBoundary;
    public float topBoundary;

    const int APosX = 0;
    const int BPosX = 80;

    //public bool isC3;
    //public bool isDie;
    Camera mainCam;

    public Camera scene2Cam;

    private void Start()
    {
        mainCam = Camera.main;

        mainCam.orthographicSize = mainCam.orthographicSize * 1920 / 1080 * Screen.height / Screen.width;
        bottomBoundary = 12f * 1920 / 1080 * Screen.height / Screen.width - 12f;
        if (topBoundary == 0)
        {
            topBoundary = 14.5f - 12f * 1920 / 1080 * Screen.height / Screen.width;
        }
        if (topBoundary == 60)
        {
            topBoundary = 60f - 12f * 1920 / 1080 * Screen.height / Screen.width;
        }

        scene2Cam.aspect = mainCam.aspect;
        scene2Cam.orthographicSize = scene2Cam.orthographicSize * 1920 / 1080 * Screen.height / Screen.width;
    }

    //private void OnEnable()
    //{
    //    EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, DoShakeT);
    //    EventSystem.Instance.AddEventListener(EventName.OnSceneFadeEnd, DoShakeF);
    //}
    //private void OnDisable()
    //{
    //    EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, DoShakeT);
    //    EventSystem.Instance.RemoveEventListener(EventName.OnSceneFadeEnd, DoShakeF);
    //}

    //void DoShakeT(Transform tran)
    //{
    //    isDie = true;
    //    Shake();
    //}
    //void DoShakeF()
    //{
    //    isDie = false;
    //}

    private void LateUpdate()
    {
        //if (isC3 && isDie)
        //    return;
        if (this.gameObject.GetComponent<Camera>().orthographicSize <= 6f)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.position.y, bottomBoundary, topBoundary), -10);
        scene2Cam.transform.position = new Vector3(scene2Cam.transform.position.x, mainCam.transform.position.y, -10);
    }

    //void Shake()
    //{
    //    mainCam.DOShakePosition(1f, new Vector3(0, 2f, 0));
    //}
}