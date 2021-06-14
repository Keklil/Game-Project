using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2.5f;
    public float waitTime = 0.5f;
    bool canGo = true;
    bool collisionPlayer = false;

    void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collisionPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionPlayer) { 
            if (canGo)
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
            
        }
        if (transform.position == point1.position) {
            Transform t = point1;
            point1 = point2;
            point2 = t;
            canGo = false;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting() {         
        yield return new WaitForSeconds(waitTime);
        canGo = true;
        
    }
}
