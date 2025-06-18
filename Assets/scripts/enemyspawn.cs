using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] spawnpoint;
    [SerializeField] GameObject meteor;
    [SerializeField] GameObject bigmeteor;
    [SerializeField] GameObject smallmeteor;
    [SerializeField] GameObject smallmeteordown;
    [SerializeField] float spawntimer = 2f;
    [SerializeField] float spawntimeinc = 7f;

    void Start()
    {
        StartCoroutine(spawn());
        StartCoroutine(spawnrateincr());
        StartCoroutine(spawnm());
    }
    IEnumerator spawnm()
    {
        gameManager.timelive++;
        GameObject smallm = Instantiate(smallmeteor, spawnpoint[Random.Range(0, 4)].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(smallm);
        if (!gameManager.game)
        {
            StartCoroutine(spawnm());
        }
        
    }
    IEnumerator spawn()
    {
        int spawnpointplace = Random.Range(0, spawnpoint.Length);
        int enemytype = Random.Range(0, 6);
        if (enemytype >= 0 && enemytype <= 3)
        {
            Instantiate(meteor, spawnpoint[spawnpointplace].transform.position, Quaternion.identity);
        }

        else if (enemytype >= 4)
        {
        
                Instantiate(bigmeteor, spawnpoint[spawnpointplace].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawntimer);

        if (!gameManager.game)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawnrateincr()
    {
        yield return new WaitForSeconds(spawntimeinc);

        if (spawntimer >= 0.5f)
        {
            spawntimer -= 0.2f;
        }
    }
}
