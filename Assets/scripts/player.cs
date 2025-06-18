using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player: MonoBehaviour
{
    BoxCollider2D Col;
    Rigidbody2D _rb;
    Camera _camera;
    GameManager _gameManager;
    [SerializeField] AudioSource AudioSource;
    public AudioClip lasersound;
    [SerializeField] float walk = 5f;
    float inputh;
    float inputv;
    float speed = 25.0f;
    bool shoot = false;
    float wait;
    public bool invis;
    Vector2 move;
    [SerializeField] GameObject laser;
    public Transform shot;
    void Start()
    {
        Col = gameObject.GetComponent<BoxCollider2D>();
       _rb = gameObject.GetComponent<Rigidbody2D>();
       _camera = Camera.main;
        
    }
    public void play(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    void Update()
    {
        wait = Time.deltaTime;
        inputh = Input.GetAxisRaw("Horizontal");
        inputv = Input.GetAxisRaw("Vertical");

        move = new Vector2(inputh, inputv) * walk;

        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(invi());
        }
    }
    void FixedUpdate()
    {
        moveplayer();
        rotateplayer();
        if (shoot && wait*60 >= 0.05f)
        {
            StartCoroutine(fire());
            wait = 0;
        }

    }
    void waittime()
    {
        if(wait*60 >= 0.2f)
        {
            StartCoroutine(fire());
            wait = 0;
        }
    }
    void moveplayer() 
    {
        if(inputh != 0 || inputv != 0 )
        {
            if(inputh != 0 && inputv != 0)
            {
                move *= 0.7f;
            }
            _rb.velocity = move;
        }
        else
        {
            move = new Vector2(0f,0f);
            _rb.velocity = move;
        }
    }

    void rotateplayer()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 dir = mouse - transform.position;
        transform.up = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "power")
        {   

            StartCoroutine(invi());
            Destroy(collision.gameObject);
        }
    }
    IEnumerator fire()
    {
        shoot = false;
        GameObject bullet = Instantiate(laser, shot.position ,shot.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        play(lasersound);
        yield return new WaitForSeconds(4);
        Destroy(bullet);
    }
    IEnumerator invi()
    {
        invis = true;
        Col.enabled = false;
        yield return new WaitForSeconds(3);
        Col.enabled = true;
        invis = false;
    }
}
