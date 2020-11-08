using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private int score = 0;
    public TextMeshProUGUI scoreDisplay; //Text for current score
    public TextMeshProUGUI finalScoreDisplay; //Text for final score once the player dies 
    public TextMeshProUGUI highScore; //Text for players high score
    public TextMeshProUGUI money; //Text for how much money the player has earned

    public GameObject crown;

    private float counter;
    private float waitTime = 0.1f; //Change this to make the score count faster or slower. 
    public float duration; //How long counting score at the end should take
    private int countToMoney; //How much we need to count to

    public AudioClip countSound;

    private bool runOnce = true;

    //NOTE: Time.time is the time since the game begun in seconds. Time.deltaTime is the time it toke for the last frame to render. So if we were running the game at 60 fps, we'd be dividing 1 by 60 to get the time to render 1 frame which would be 0.016. 

    private void Update()
    {
        if (GameObject.FindWithTag("Player") != null) //If player exists (is not dead) 
        {
            counter += Time.deltaTime;

            if (counter > waitTime) //Every however long waitTime is
            {
                score++;
                scoreDisplay.text = score.ToString();
                counter = counter - waitTime; //Reset counter NOTE: Could also just reset this to zero but subtracting the waitTime is more accurate over time
            }
        }
        else if (runOnce == true) //If player is dead & this is the first time running through this
        {
            runOnce = false;

            if (score > 500)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 50); //Increase money by 50
                countToMoney = 50;
            }
            else if (score > 250)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 25); //Increase money by 25
                countToMoney = 25;
            }
            else if (score > 100)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 10); //Increase money by 10
                countToMoney = 10;
            }
            else
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 5); //Increase money by 50
                countToMoney = 5;
            }


            StartCoroutine(CountScore());
            CalculateHighScore();
        }
    }

    IEnumerator CountScore()
    {
        int temp, temp2;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;  //What percent of our total time the current frame occupies
            temp = (int)Mathf.Lerp(0, score, progress);
            finalScoreDisplay.text = "SCORE: \n" + temp.ToString();

            temp2 = (int)Mathf.Lerp(0, countToMoney, progress);
            money.text = "+" + temp2.ToString();


            GameObject.FindWithTag("DummyAudio").GetComponent<FindAudioManager>().SelectEffect(countSound); //Play the count sound effect

            yield return null; //Wait until next frame
        }
        temp = score;
        finalScoreDisplay.text = "SCORE: \n" + temp.ToString();
        money.text = "+" + countToMoney.ToString();
    }

    private void CalculateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            crown.SetActive(true); //Show crown icon
        }
        highScore.text = "HIGH SCORE: \n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

}


