using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMoveFloor : MonoBehaviour
{
    public int floorIndex;
    public float duration;
    public float boundary;
    float initPos;
    bool notInInitPos;
    bool isPlaying;
    Tweener forwardTweener;
    Tweener backTweener;

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener<int>(EventName.OnControllFloor, OnMove);
        EventSystem.Instance.AddEventListener<Transform>(EventName.OnPlayerDie, Regeneration);
    }

    private void Start()
    {
        initPos = transform.position.x;
        forwardTweener = transform.DOMoveX(boundary, duration).SetEase(Ease.Linear)
                    .OnComplete(MoveComplete);
        forwardTweener.SetAutoKill(false);
        forwardTweener.Pause();
        backTweener = transform.DOMoveX(initPos, duration).SetEase(Ease.Linear)
                    .OnComplete(MoveComplete);
        backTweener.SetAutoKill(false);
        backTweener.Pause();
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener<int>(EventName.OnControllFloor, OnMove);
        EventSystem.Instance.RemoveEventListener<Transform>(EventName.OnPlayerDie, Regeneration);
    }

    private void OnMove(int index)
    {
        if (index == floorIndex || index == 3)
        {
            MoveFloor();
        }
    }

    private void MoveFloor()
    {
        if (isPlaying)
        {
            transform.DOPause();
            isPlaying = false;
        }
        else
        {
            isPlaying = true;
            if (!notInInitPos)
            {
                forwardTweener.PlayForward();
            }
            else
            {
                backTweener.PlayForward();
            }
        }
    }

    private void MoveComplete()
    {
        notInInitPos = !notInInitPos;
        isPlaying = false;
        forwardTweener = transform.DOMoveX(boundary, duration).SetEase(Ease.Linear)
            .OnComplete(MoveComplete);
        forwardTweener.SetAutoKill(false);
        forwardTweener.Pause();
        backTweener = transform.DOMoveX(initPos, duration).SetEase(Ease.Linear)
                    .OnComplete(MoveComplete);
        backTweener.SetAutoKill(false);
        backTweener.Pause();
    }

    private void Regeneration(Transform trans)
    {
        transform.position = new Vector3(initPos, transform.position.y);
        forwardTweener = transform.DOMoveX(boundary, duration).SetEase(Ease.Linear)
                .OnComplete(MoveComplete);
        forwardTweener.SetAutoKill(false);
        forwardTweener.Pause();
        backTweener = transform.DOMoveX(initPos, duration).SetEase(Ease.Linear)
                    .OnComplete(MoveComplete);
        backTweener.SetAutoKill(false);
        backTweener.Pause();
    }
}