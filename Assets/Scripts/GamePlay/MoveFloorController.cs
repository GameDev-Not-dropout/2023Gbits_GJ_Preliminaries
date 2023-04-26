using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorController : MonoBehaviour
{
    public int controllIndex;
    bool playerInTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            playerInTrigger = true;
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.J))
        {
            EventSystem.instance.EmitEvent(EventName.OnControllFloor, controllIndex);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.T_Player)
        {
            playerInTrigger = false;
        }
    }








}
