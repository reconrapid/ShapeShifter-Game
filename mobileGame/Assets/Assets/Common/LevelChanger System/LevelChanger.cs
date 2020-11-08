using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator transitions;

    public static LevelChanger Instance = null;


    public void Awake()
    {
        // If there is not already an instance, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //Set to dont destroy on load so our LevelChanger will persist between scenes
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        } 
    }

    public void Play()
    {
        StartCoroutine(LoadScene(1));
    }
    public void Menu()
    {
        StartCoroutine(LoadScene(0));
    }
    public void RestartGame()
    {
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int level)
    {
        transitions.SetTrigger("fade_out");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(level); //Use SceneManager class to load the desired scene/level
        transitions.SetTrigger("fade_in");
        yield return new WaitForSeconds(1.5f);
    }

}
