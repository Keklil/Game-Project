using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool active;
    //Transform wall;
    void Start()
    {
        //active = GetComponent<Activate>().act;
    }

    // Update is called once per frame
    void Update()
    {
        active = GetComponent<Activate>().act = true;
        if (active)
        {
            Destroy(gameObject);
        }

    }
}
