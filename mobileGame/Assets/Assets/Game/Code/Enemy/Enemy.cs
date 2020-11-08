using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly int damage = 1;
    public float speed;

    public ParticleSystem killEffect;
    private ParticleSystem.MainModule particle;
    private Color color;
    public AudioClip deathSound;

    private void Start()
    {
        particle = killEffect.main; //Get Main Module of particle effect & store this in our variable. 
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // Move down over time
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //If we hit the object tagged player
        {
            if (other.GetComponent<SpriteRenderer>().sprite != GetComponent<SpriteRenderer>().sprite) //If the sprite of the enemy does not match the sprite of the player
            {
                other.GetComponent<Player>().health -= damage; //Do damage to the players health
                Destroy(gameObject); //Destroy the enemy 
            }
            else
            {
                ChangeColour(); //Change particle colour to be the same as the current shape 
                particle.startColor = color;

                Instantiate(killEffect, transform.position, Quaternion.identity); //Spawn special effect 
                GameObject.FindWithTag("DummyAudio").GetComponent<FindAudioManager>().SelectEffect(deathSound);
                Destroy(gameObject); //Destroy the enemy 
            }
        }
    }

    private void ChangeColour()
    {
        if (GetComponent<SpriteRenderer>().sprite.name == "Circle")
        {
            color = new Color32(42, 32, 170, 120);
        }
        else if (GetComponent<SpriteRenderer>().sprite.name == "Square")
        {
            color = new Color32(192, 0, 116, 120);
        }
        else
        {
            color = new Color32(0, 128, 70, 120);
        }
    }
}
