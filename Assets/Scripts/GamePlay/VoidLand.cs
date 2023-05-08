using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidLand : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            // 死亡动画
            //SceneFadeManager.instance.ReSetPlayerPosition();
            EventSystem.instance.EmitEvent(EventName.OnPlayerDie, collision.transform);

        }
    }









}
