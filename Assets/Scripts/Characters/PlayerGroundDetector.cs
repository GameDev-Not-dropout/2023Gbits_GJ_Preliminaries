using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;  // 球的半径
    [SerializeField] LayerMask groundLayer;
    Vector2 direction = Vector2.up;
    RaycastHit2D[] hit2D = new RaycastHit2D[1];
    [SerializeField] float distance = 0.1f;

    // 投射碰撞球体，不会产生GC，返回数组长度
    public bool IsGrounded
    {
        get
        {
            bool result = Physics2D.CircleCastNonAlloc(transform.position, detectionRadius, direction, hit2D, distance, groundLayer) != 0;
            if (hit2D[0] && hit2D[0].transform.tag == Tags.T_MoveFloor)
            {
                transform.parent.SetParent(hit2D[0].transform);
            }
            else
            {
                transform.parent.SetParent(null);
            }
            return result;
        }
    }
        


    /// <summary>
    /// 将投射出来的球体显示在编辑器窗口
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
