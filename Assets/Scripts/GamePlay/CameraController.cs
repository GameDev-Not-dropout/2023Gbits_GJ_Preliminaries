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
    //Camera mainCam;

    //private void Start()
    //{
    //    mainCam = Camera.main;
    //}

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
        if (this.gameObject.GetComponent<Camera>().orthographicSize < 10f)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.position.y, bottomBoundary, topBoundary), -10);

    }


    //void Shake()
    //{
    //    mainCam.DOShakePosition(1f, new Vector3(0, 2f, 0));
    //}






}
