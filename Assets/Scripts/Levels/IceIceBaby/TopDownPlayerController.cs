using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    private const int MAX_PEOPLE_SHOT = 3;

    public float speed = 3f;

    public Camera cam;
    public Rigidbody2D rb2D;
    
    private Vector2 mousePos;
    private Vector2 movement;


    private static int carsShot = 0;
    private static int peopleShot = 0;

    public static int CarsShot
    {
        get => carsShot;
        set
        {
            carsShot = value;
            GUIManager.Instance.AddGameInfoLeft("Cars destroyed: " + value);
        }
    }

    public static int PeopleShot
    {
        get => peopleShot;
        set
        {
            peopleShot = value;
            GUIManager.Instance.AddGameInfoRight("Killed people: " + value);

            if (value >= MAX_PEOPLE_SHOT)
            {
                SaveSystem.SavePlayer();
                GUIManager.Instance.OpenGameOverMenu();
            }
        }
    }

    private void Awake()
    {
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        peopleShot = 0;
        carsShot = 0;
    }

    private void OnDestroy()
    {
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.DeleteGameInfo();
        }
    }

    void Update()
    {
        if (GameManager.Instance.paused) return;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.paused) return;
        
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb2D.MovePosition(rb2D.position + movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tag.InstantDeath.ToString())
        {
            AudioManager.Instance.PlayGrunt();
            SaveSystem.SavePlayer();

            GUIManager.Instance.OpenGameOverMenu();
        }
    }

}
