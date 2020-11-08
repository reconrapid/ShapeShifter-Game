using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    private Vector2 goToPos;
    [Header("Settings")]
    [Tooltip("How far the player moves when he does")]
    public float movementDistance;
    [Tooltip("The speed at which the player gets to the target location")]
    public float speed;
    [Tooltip("The amount of health the player has & how much damage must be done for the player to die")]
    public int health;

    //Floats to restrain player within screen area
    [Tooltip("The furthest left we want the player to be able to move")]
    public float leftLimit;
    [Tooltip("The furthest right we want the player to be able to move")]
    public float rightLimit;

    [Header("Camera Shake")]
    [Tooltip("How long to shake the camera")]
    public float shakeLength;
    [Tooltip("How magnitude of the camera shake")]
    public float shakeIntensity;

    [Header("Game Objects")]
    public GameObject gameOverScreen;
    public CameraShake cameraShake;

    [Tooltip("Sound effect for when the player moves")]
    public AudioClip swipeSound;

    //Sprites
    public Sprite[] sprites;

    [Header("Player Trails")]
    public ShopItem[] playerTrails;

    private void Start()
    {
        goToPos = transform.position; //Get current pos

        //FINDING & CREATEING PLAYER TRAIL
        int i = PlayerPrefs.GetInt("CurrentItem"); //Get the current trail
        for (int x = 0; x < playerTrails.Length; x++)
        {
            if (playerTrails[x].ID == i) //If the ID matches the ID of the selected trail
            {
                Instantiate(playerTrails[x].trailEffect, this.transform);
                break;
            }
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            gameOverScreen.SetActive(true); //Display gameOver screen
            Destroy(gameObject); //Destroy Player 
        }

        PlayerMovement();
        ChangeShape();
    }

    private void PlayerMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, goToPos, speed * Time.deltaTime); //Smoothly move toward position 

        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < rightLimit) //MOVE UP
        {
            StartCoroutine(cameraShake.Shake(shakeLength, shakeIntensity)); //Shake Camera
            goToPos = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + movementDistance, leftLimit, rightLimit));

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > leftLimit) //MOVE DOWN
        {
            StartCoroutine(cameraShake.Shake(shakeLength, shakeIntensity)); //Shake Camera
            goToPos = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y - movementDistance, leftLimit, rightLimit));

        }
    }

    private void ChangeShape()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpriteControl(0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SpriteControl(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SpriteControl(2);
        }
    }

    private void SpriteControl(int shape)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[shape]; //Change Sprite 

        if (PlayerPrefs.GetInt("CurrentItem") == 0) //If using default trail 
        {
            transform.GetChild(0).GetComponent<Trail>().ChangeTrail(sprites[shape]);
        }
    }

}


