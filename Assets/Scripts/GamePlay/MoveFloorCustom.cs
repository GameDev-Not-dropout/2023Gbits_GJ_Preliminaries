using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorCustom : MonoBehaviour
{
    public float duration;
    public float endBoundaryLeft;
    public float endBoundaryRight;
    bool hasMove;
    float initPosX;
    float rightPosX;
    float leftPosX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == Tags.T_Player)
        {
            if (hasMove)
                return;

            InitMove();
        }
    }

    void InitMove()
    {
        hasMove = true;
        initPosX = transform.position.x;
        rightPosX = initPosX + endBoundaryRight;
        leftPosX = initPosX + endBoundaryLeft;
        transform.DOLocalMoveX(rightPosX, duration * 1.2f).OnComplete(StartMove);
    }

    void StartMove()
    {
        transform.DOKill();
        MovePingPong(rightPosX, leftPosX);
    }

    /// <summary>
    /// 来回移动
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    void MovePingPong(float from, float to)
    {
        transform.DOLocalMoveX(to, duration).SetEase(Ease.Linear)
            .OnComplete(() => MovePingPong(to, from));
       


    }








}
