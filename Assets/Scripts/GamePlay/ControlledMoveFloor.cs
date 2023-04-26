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
    bool hasMoveDone = true;

    private void OnEnable()
    {
        EventSystem.instance.AddEventListener<int>(EventName.OnControllFloor, OnMove);
    }

    private void Start()
    {
        initPos = transform.position.x;
    }

    private void OnDisable()
    {
        EventSystem.instance.RemoveEventListener<int>(EventName.OnControllFloor, OnMove);
    }

    void OnMove(int index)
    {
        if (index == floorIndex || index == 3)
        {
            MoveFloor();
        }
    }

    void MoveFloor()
    {
        if (!hasMoveDone)
            return;

        if (!notInInitPos)
        {
            transform.DOMoveX(boundary, duration).SetEase(Ease.Linear)
                .OnComplete(MoveComplete);
        }
        else
        {
            transform.DOMoveX(initPos, duration).SetEase(Ease.Linear)
                .OnComplete(MoveComplete);
        }
    }

    void MoveComplete()
    {
        notInInitPos = !notInInitPos;
        hasMoveDone = true;
    }

}
