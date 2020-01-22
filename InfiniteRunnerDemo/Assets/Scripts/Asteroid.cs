using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        // Moves asteroid to the left
        transform.position = new Vector3(   transform.position.x - Time.deltaTime * moveSpeed,
                                            transform.position.y,
                                            transform.position.z);

        // Destroys asteroid if it goes too far off screen
        if(transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
