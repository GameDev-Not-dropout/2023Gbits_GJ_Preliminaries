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
    public bool isC3;

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

            if (isC3)
            {
                return result;
            }

            if (result && hitTrigger[0].transform.name != tempHit.name)
            {
                tempHit = hitTrigger[0].transform;
                return true;
            }
            else
                return false;
        }
    }

    private void OnEnable()
    {
        EventSystem.Instance.AddEventListener(EventName.OnChangeStyle, ChangeStyle);
    }

    private void OnDisable()
    {
        EventSystem.Instance.RemoveEventListener(EventName.OnChangeStyle, ChangeStyle);
    }

    float lastChangeTime;

    private void ChangeStyle()
    {
        if (Time.time - lastChangeTime <= 0.5f)   // 切换场景需要0.5s时间间隔
            return;

        lastChangeTime = Time.time;

        if (transform.localPosition.y < -1)
        {
            transform.localPosition = new Vector3(0, -0.5f, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, -1.2f, 0);
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