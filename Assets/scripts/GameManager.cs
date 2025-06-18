using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverUI;
    public GameObject gameoverUI1;
    public TextMeshProUGUI scoretext;
    public int score;
    public bool game = false;
    public float timelive;
    public void gameover()
    {
        gameoverUI.SetActive(true);
        gameoverUI1.SetActive(false);
        setup();
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void setup()
    {
        scoretext.text = "Asteroids shot down :- " + score.ToString() + "\n" + "Meteors dodged :- " + timelive.ToString() + "\n" + "Time surived :- " + Mathf.Round(timelive*1.5f).ToString();
    }
    
}
