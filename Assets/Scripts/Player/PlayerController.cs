using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator solversAnimator;
    public PlayerMovement moveController;
    public PlayerData data;

    private void Awake()
    {
        //if new game
        data = new PlayerData();
        //else
        //data = savesystem.load
    }

    void Update()
    {
        if (data.Health == 0)
        {
            Application.Quit();
            return;
        }

        //should be optimized -> when changing directions there is a moment, when Input.GetAxisRaw("Horizontal") == 0
        if (Input.GetAxisRaw("Horizontal") != 0 )
        {
            solversAnimator.SetTrigger("Walk");
        }
        else
        {
            solversAnimator.SetTrigger("Idle");
        }

    }
}
