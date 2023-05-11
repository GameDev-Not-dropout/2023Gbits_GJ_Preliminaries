using UnityEngine;
using DG.Tweening;

public class MoveFloor : MonoBehaviour
{
    public bool isLeftSide = true;
    public float duration;
    public float endBoundary;
    public float offset = 2f;
    float initPos;
    public bool moveHorizontal = true;
    public bool isStopInStart;
    public int floorIndex;
    public int resumeFloorIndex;

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<int>(EventName.OnChangeMoveFloor, OnChangeScene);
    }
    private void OnDestroy()
    {
        EventSystem.Instance.RemoveEventListener<int>(EventName.OnChangeMoveFloor, OnChangeScene);
    }

    private void Start()
    {
        if (moveHorizontal)
        {
            if (!isLeftSide)
                endBoundary += 80;

            initPos = transform.position.x;
            if (isStopInStart)
                return;
            MovePingPong(initPos, endBoundary - offset, true);
        }
        else
        {
            initPos = transform.position.y;
            if (isStopInStart)
                return;
            MovePingPong(initPos, endBoundary, false);
        }


    }

    /// <summary>
    /// 来回移动
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    void MovePingPong(float from, float to, bool isMoveHorizontal)
    {
        if (isMoveHorizontal)
        {
            transform.DOMoveX(to, duration).SetEase(Ease.Linear)
                .OnComplete(() => MovePingPong(to, from, true));
        }
        else
        {
            transform.DOMoveY(to, duration).SetEase(Ease.Linear)
                .OnComplete(() => MovePingPong(to, from, false));
        }

    }

    void OnChangeScene(int index)
    {
        if (index == resumeFloorIndex)
        {
            if (isStopInStart)
            {
                if (moveHorizontal)
                {
                    MovePingPong(initPos, endBoundary - offset, true);  // 初始化
                }
                else
                    MovePingPong(initPos, endBoundary, false);
                isStopInStart = false;
            }

            transform.DOPlayForward();  // 恢复移动
            return;
        }

        if (index != floorIndex)
            return;



        transform.DOPause();
    }







}
