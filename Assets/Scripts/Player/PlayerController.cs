using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator solversAnimator;
    public PlayerMovement moveController;
    //private static PlayerData data;

    /*public static PlayerData PlayerData
    {
        get => data;
    }*/


    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 )
        {
            solversAnimator.SetTrigger("Walk");
        }
        else
        {
            solversAnimator.SetTrigger("Idle");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            Die();
        }
    }

    private void Die()
    {
        moveController.Freeze = true;

        SaveSystem.SavePlayer();
        GUIManager.Instance.OpenGameOverMenu();
    }

}
