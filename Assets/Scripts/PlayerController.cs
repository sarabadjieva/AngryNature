using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement moveController;
    public PlayerData data;

    private void Awake()
    {
        //if new game
        data = new PlayerData();
        //else
        //data = savesystem.load
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (data.Health == 0)
        {
            Application.Quit();
            return;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
