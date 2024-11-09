using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;
    private Vector2 initialPosition;
    private bool movingRight = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveBird();
    }

    private void MoveBird()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x > initialPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;

            if (transform.position.x < initialPosition.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }
}
