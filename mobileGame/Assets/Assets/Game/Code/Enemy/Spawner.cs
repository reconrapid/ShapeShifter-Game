using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Game Objects")]
    public GameObject[] enemyRegularPatterns; //Stores our enemy pattern prefab
    public GameObject[] enemyComplexPatterns; //Stores our enemy pattern prefab
    public GameObject enemyQuickTimePattern;
    public Animator warning;
    private bool warningBool = true;

    public GameObject[] enemyPrefabs; //Stores our list of possible enemy prefabs 

    private float timeToSpawn;

    private bool startOfGame = true;
    private bool difficultyMax = false;

    private float timeToIncrease = 0;
    private float resetTimeToIncrease = 40;
    private int difficultyLevel = 1;

    [Header("Settings")]
    [Tooltip("Inital time it takes for the enemies to spawn")]
    public float resetTimer;
    [Tooltip("Minimum possible time between enemy spawns")]
    public float minSpawnTime;

    [Tooltip("Speed of the enemies")]
    public float enemySpeed;

    private void Awake()
    {
        StartCoroutine(Wait(2.5f)); //Call the wait coroutine & give the player some time to prepare before we start spawning enemies 
        SetEnemySpeed(enemySpeed);
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        startOfGame = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (startOfGame == false) //So long as it isnt the start of the game (We've went through the wait coroutine to give the player time to prepare)
        {
            if (difficultyMax == true)
            {
                if (timeToIncrease <= 0)
                {
                    IncreaseDifficulty();
                    timeToIncrease = resetTimeToIncrease;
                }
                else
                {
                    Debug.Log(timeToIncrease);
                    timeToIncrease -= Time.deltaTime;
                }
            }

            if (timeToSpawn <= 0) //If time to spawn new enemy 
            {
                if (difficultyLevel % 3 == 0)
                {
                    if (warningBool == true)
                    {
                        warning.SetTrigger("Warn");
                        warningBool = false;
                    }
                    QuickTime();
                }
                else if (difficultyLevel % 2 == 0)
                {
                    OffTimeSpawn();
                }
                else
                {
                    RegularSpawn();
                }
            }
            else
            {
                timeToSpawn -= Time.deltaTime;
            }
        }
    }

    public void RegularSpawn()
    {
        int rand = Random.Range(0, enemyRegularPatterns.Length); //Pick a random pattern to spawn 
        Instantiate(enemyRegularPatterns[rand], transform.position, Quaternion.Euler(0, 0, 90)); //Spawn the pattern prefab (Rotated 90 degrees so I dont have to change the spawn points within the prefabs)
        timeToSpawn = resetTimer; //Reset spawn timer 
    }

    public void OffTimeSpawn()
    {
        if (GameObject.FindWithTag("ComplexPattern") == null)
        {
            int rand = Random.Range(0, enemyComplexPatterns.Length); //Pick a random pattern to spawn 
            Instantiate(enemyComplexPatterns[rand], transform.position, Quaternion.Euler(0, 0, 90)); //Spawn the pattern prefab (Rotated 90 degrees so I dont have to change the spawn points within the prefabs)
            timeToSpawn = resetTimer; //Reset spawn timer 
        }
    }

    public void QuickTime()
    {
        if (GameObject.FindWithTag("ComplexPattern") == null)
        {
            Instantiate(enemyQuickTimePattern, transform.position, Quaternion.Euler(0, 0, 90)); //Spawn the pattern prefab (Rotated 90 degrees so I dont have to change the spawn points within the prefabs)
            timeToSpawn = resetTimer; //Reset spawn timer 
        }
    }

    public void IncreaseDifficulty()
    {
        difficultyLevel++;
        warningBool = true;

        if (resetTimer < minSpawnTime)
        {
            SetEnemySpawn(resetTimer - 0.2f);
        }

        //If we reached the most difficult level on the slider
        if (difficultyLevel == 5)
        {
            difficultyMax = true;
        }

        Debug.Log("DIFFICULTY INCREASE");
    }

    public void SetEnemySpawn(float newTime)
    {
        resetTimer = newTime;
    }

    public void SetEnemySpeed(float newSpeed)
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPrefabs[i].GetComponent<Enemy>().speed = newSpeed; //set new enemy speed
        }
    }

}
