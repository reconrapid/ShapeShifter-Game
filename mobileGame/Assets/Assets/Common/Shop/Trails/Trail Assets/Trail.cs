using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{

    public ParticleSystem trailEffect;


    public void ChangeTrail(Sprite newSprite)
    {
        trailEffect.textureSheetAnimation.SetSprite(0, newSprite);
    }
}
