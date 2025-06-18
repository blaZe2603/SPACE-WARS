using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    GameManager gameManager;
    player playerscript;
    GameObject player;
    [SerializeField] GameObject powerup;
    [SerializeField] float health;
    [SerializeField] float speed;
    Quaternion rotation;
    bool disable = false;
    bool dead;
    Vector2 movedir;
    //public int score2 = 0;
    void Start()
    {
        StartCoroutine(power());
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        powerup = GameObject.FindGameObjectWithTag("power");
    }
    void Update()
    {
        if (!gameManager.game && !disable )
        {
            MoveEnemy();
            RotateEnemy();
        }
    }

    void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position , player.transform.position , speed * Time.deltaTime);
    }

    void RotateEnemy()
    {
        movedir = player.transform.position - transform.position;
        movedir.Normalize();
        rotation = Quaternion.LookRotation(Vector3.forward, movedir);

        if(transform.rotation != rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 200 * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Damaged());
            health -= 40f;
            if (health == -1)
            {
                Destroy(gameObject);
                gameManager.score++;
                if (Random.Range(0, 7) < 2)
                {
                    StartCoroutine(power());
                    //Instantiate(powerup, transform.position, Quaternion.identity);
                }
            }
            Destroy(collision.gameObject);
        
        }
        if (collision.gameObject.tag == "Player")
        {
            gameManager.game = true;
            collision.gameObject.SetActive(false);
            StartCoroutine(gameover2());
        }
    }
    IEnumerator gameover2()
    {

        yield return new WaitForSeconds(1);
        gameManager.gameover();
    }
    IEnumerator Damaged()
    {
        disable = true;
        yield return new WaitForSeconds(0.5f);
        disable = false;
    }
    IEnumerator power()
    {
        GameObject pow =  Instantiate(powerup, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        Destroy(pow);
    }
}
