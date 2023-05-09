using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MoveFloorController : MonoBehaviour
{
    public int controllIndex;
    bool playerInTrigger;
    bool inRight;
    Animator animator;
    int toRightHash;
    int toLeftHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        toRightHash = Animator.StringToHash("ToRight");
        toLeftHash = Animator.StringToHash("ToLeft");
    }

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
            if (inRight)
            {
                animator.Play(toLeftHash);
                inRight = false;
            }
            else
            {
                animator.Play(toRightHash);
                inRight = true;
            }
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
