using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    private bool isMute;

    public GameObject muteButton; //Mute Button


    public void Awake()
    {
        //Application.targetFrameRate = 60; //Target frame rate to be 60 fps
        #region MuteButton Check
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            mixer.SetFloat("volume", -80);
            muteButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            isMute = true;
        }
        else
        {
            mixer.SetFloat("volume", 0);
            muteButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            isMute = false;
        }
        #endregion

    }

    public void Mute()
    {
        isMute = !isMute; //Flip mute to the value it currently isnt.

        if (isMute == true)
        {
            PlayerPrefs.SetInt("Mute", 1);
            Debug.Log(isMute);
            mixer.SetFloat("volume", -80);
            muteButton.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            Debug.Log(isMute);
            mixer.SetFloat("volume", 0);
            muteButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

}
