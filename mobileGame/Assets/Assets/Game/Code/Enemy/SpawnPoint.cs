using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject[] enemySprite; //Stores our list of possible enemy sprites
    public GameObject xSprite; //Stores our "X" sprite 

    public int difficulty;
    public float waitTime;

    //Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("Wait", waitTime);
    }

    private void Spawn()
    {
        if (difficulty == 1)
        {
            Instantiate(xSprite, transform.position, Quaternion.identity);
        }
        else
        {
            int rand = Random.Range(0, enemySprite.Length); //Pick a sprite enemy to spawn
            Instantiate(enemySprite[rand], transform.position, Quaternion.identity);
        }
    }

    private IEnumerator Wait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Spawn();
    }
}

