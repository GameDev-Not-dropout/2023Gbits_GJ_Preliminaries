using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutFloor : MonoBehaviour
{
    public float duration = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == Tags.T_Player)
        {
            this.GetComponent<SpriteRenderer>().DOFade(0, duration)
                .OnComplete(
                () => 
                { 
                    Destroy(this.gameObject);
                    SoundManager.Instance.PlaySound(SE.floorFadeOut);
                }
                );
        }
    }








}
