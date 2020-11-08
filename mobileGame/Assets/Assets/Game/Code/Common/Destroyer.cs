using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float lifetime;


    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
