using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float speed = 5.0f;//control the speed
    private Rigidbody2D rb;
    public bool isDead = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        //get rigibody2d Component of the BirdbBek
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetMouseButtonDown(0) --> left side mouse button pressed down
        if(Input.GetMouseButtonDown(0)){
            rb.linearVelocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        isDead = true;
        audioManager.PlaySFX(audioManager.death);
        gameManager.GameOver();
    }
}
