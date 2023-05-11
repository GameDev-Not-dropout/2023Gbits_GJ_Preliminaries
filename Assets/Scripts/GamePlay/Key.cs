using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject copy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            EventSystem.Instance.EmitEvent(EventName.OnGetKey);     // 开门
            SoundManager.Instance.PlaySound(SE.getKey);
            this.gameObject.SetActive(false);
            if (copy != null)
            {
                copy.SetActive(false);
            }
        }
    }








}
