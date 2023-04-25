using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            EventSystem.instance.EmitEvent(EventName.OnGetKey);
            this.gameObject.SetActive(false);
        }
    }








}
