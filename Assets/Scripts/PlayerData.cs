using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int health;
    private int level = 0;

    public int Health
    {
        get => health;
        set => health = value;
    }


    public PlayerData()
    {
        health = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
