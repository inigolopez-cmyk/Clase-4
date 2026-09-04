
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputAction inputmovement;
    public InputAction rotateMovement;
    public InputAction shoot;

    public Rigidbody2D rb2D;
    public GameObject bullet;

    public int lifes;
    bool isDamage = false;

    public float currentTime;
    public float maxTime;

    UpdateUI uiScript;

    public List<GameObject> bulletPool = new List<GameObject>();

    private void OnEnable()
    {
        inputmovement.Enable();
        rotateMovement.Enable();
        shoot.Enable();
    }

    private void OnDisable()
    {
        inputmovement.Disable();
        rotateMovement.Disable();
        shoot.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movement = inputmovement.ReadValue<Vector2>();
        rb2D.linearVelocity = movement * 5;
        rb2D.linearVelocity = Vector2.ClampMagnitude(rb2D.linearVelocity,10);

        Vector2 lookDir = rotateMovement.ReadValue<Vector2>();
        Vector2 Look = Camera.main.ScreenToWorldPoint(lookDir);
        Vector2 Direction = Look - rb2D.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
        rb2D.rotation = angle;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifes = 3;

        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
        uiScript.AddLifes(lifes);
        
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0)
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            uiScript.OpenGameOver();
        }

        if(isDamage)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            currentTime += Time.deltaTime;
            if(currentTime >maxTime)
            {
                currentTime = 0;
                isDamage = false;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        
        if(shoot.triggered)
        {
            GameObject temp = GetBullet();
            temp.SetActive(true);
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
            //GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rbtemp = temp.GetComponent<Rigidbody2D>();
            rbtemp.AddForce(transform.up * 5, ForceMode2D.Impulse);
        }
    }

    GameObject GetBullet()
    {
        foreach(GameObject b in bulletPool)
            {
                if(b.activeInHierarchy == false)
                {
                return b;
                }
            }
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.SetActive(false);
        bulletPool.Add(temp);
        return temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            lifes--;
            uiScript.AddLifes(lifes);
            isDamage = true;
        }
    }
}
