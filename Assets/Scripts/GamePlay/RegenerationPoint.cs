using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationPoint : MonoBehaviour
{


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            EventSystem.Instance.EmitEvent(EventName.OnRegenerationPointRef, transform);
        }
    }










}
