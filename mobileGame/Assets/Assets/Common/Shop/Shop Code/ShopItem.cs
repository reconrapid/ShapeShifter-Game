using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : ScriptableObject
{
    public string trailName; //Name of the trail
    public ParticleSystem trailEffect; //The trail effect

    public Sprite icon; //Icon to show for the trail
    public int cost; //Cost of the trail
    public int ID;
}
