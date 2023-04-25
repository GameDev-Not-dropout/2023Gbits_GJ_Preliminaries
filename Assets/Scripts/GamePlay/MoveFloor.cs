using UnityEngine;
using DG.Tweening;

public class MoveFloor : MonoBehaviour
{
    public bool isLeftSide = true;
    public float duration;
    public float rightBoundary;
    public float offset = 2f;
    float initPos;

    private void Start()
    {
        if (!isLeftSide)
            rightBoundary += 80;
        initPos = transform.position.x;
        MovePingPong(initPos, rightBoundary - offset);

    }

    void MovePingPong(float from, float to)
    {
        transform.DOMoveX(to, duration).SetEase(Ease.Linear)
            .OnComplete(() => MovePingPong(to, from));
    }









}
