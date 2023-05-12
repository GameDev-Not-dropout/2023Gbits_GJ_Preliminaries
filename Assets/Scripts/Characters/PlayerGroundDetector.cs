using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;  // 球的半径
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask TriggerLayer;
    Vector2 direction = Vector2.up;
    RaycastHit2D[] hit2D = new RaycastHit2D[1];
    RaycastHit2D[] hitTrigger = new RaycastHit2D[1];
    public float maxTriggerLength = 20f;
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
    Transform tempHit;
    private void Start()
    {
        tempHit = transform;
    }
    public bool IsEnterTransitionTrigger
    {
        get 
        {
            Vector3 origin = new Vector3(transform.position.x - maxTriggerLength / 2, transform.position.y);
            bool result = Physics2D.RaycastNonAlloc(origin, Vector3.right, hitTrigger, maxTriggerLength, TriggerLayer) != 0;
            Debug.DrawLine(origin, new Vector3(transform.position.x + maxTriggerLength / 2, transform.position.y), Color.green);

            if (result && hitTrigger[0].transform.name != tempHit.name)
            {
                tempHit = hitTrigger[0].transform;
                return true;
            }
            else
                return false;
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
