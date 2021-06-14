using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.position);
    }
}
