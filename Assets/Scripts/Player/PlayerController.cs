using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public bool inSecretLevel;

    public LevelController currentLevelController;

    public Animator solversAnimator;
    public PlayerMovement moveController;


    void Update()
    {
        if (GameManager.Instance.paused) return;

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
        if (collision.tag == Tag.InstantDeath.ToString())
        {
            Die();
        }
        else if (collision.tag == Tag.SecretLevel.ToString())
        {
            currentLevelController.OpenSecretLevel(true);
        }
    }

    private void Shoot()
    {

    }

    private void Die()
    {
        moveController.Freeze = true;

        AudioManager.Instance.PlayGrunt();
        SaveSystem.SavePlayer();

        //if (inSecretLevel)
        //{
        //    GameManager.Instance.ReloadLevel();
        //}
       // else
       // {
            GUIManager.Instance.OpenGameOverMenu();
        //}
    }
}
