using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public GameObject[] points; //Stores our possible spawn points
    public GameObject[] enemySprites; //Stores our list of possible enemy sprites

    public float waitTime;


    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("Wait", waitTime);
    }

    private void Spawn()
    {
        int rand = Random.Range(0, enemySprites.Length); //Pick a sprite enemy to spawn
        for (int i = 0; i < points.Length; i++)
        {
            Instantiate(enemySprites[rand], points[i].GetComponent<Transform>().position, Quaternion.identity);
        }
    }

    private IEnumerator Wait(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Spawn();
    }
}
