using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTransform : MonoBehaviour
{
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Abs(enemy.transform.position.x), GetComponent<RectTransform>().anchoredPosition.y);
    }
}
