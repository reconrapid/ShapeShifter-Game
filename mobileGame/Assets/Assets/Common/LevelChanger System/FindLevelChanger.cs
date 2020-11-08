using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindLevelChanger : MonoBehaviour
{

    private LevelChanger levelChange;

    void Start()
    {
        if (GameObject.FindWithTag("SceneManager") != null) { 
            levelChange = GameObject.FindWithTag("SceneManager").GetComponent<LevelChanger>(); //Find our scenemanager object & get the LevelChanger
        }
    }

    #region ButtonCalls

    public void Play()
    {
        levelChange.Play();
    }

    public void MainMenu()
    {
        levelChange.Menu();
    }

    public void Restart()
    {
        levelChange.RestartGame();
    }

    #endregion
}
