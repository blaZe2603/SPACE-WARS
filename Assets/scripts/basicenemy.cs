using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicenemy : MonoBehaviour
{
    
    BoxCollider2D col;
    GameManager gameManager;
    GameObject player;
    Rigidbody2D _rb;
    [SerializeField] float speed;
    Vector2 move;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        move = new Vector2(0f , speed);
        col = GameObject.Find("ground").GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            gameManager.game = true;
            collision.gameObject.SetActive(false);
            gameManager.gameover();
         
        }
        if (collision.gameObject.tag == "ground")
        {
            col.enabled = false;
        }
    }


}
