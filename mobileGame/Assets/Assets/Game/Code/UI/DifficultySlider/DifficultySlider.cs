using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySlider : MonoBehaviour
{

    public Animator slide;
    public Spawner spawnSystem;

    public void SetSliderSpeed(float speed)
    {
        slide.SetFloat("SpeedMultiplier", speed);
    }

    public void IncrementDifficulty()
    {
        spawnSystem.IncreaseDifficulty();
    }

}
