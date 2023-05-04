using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBarMovement : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public float speed;
    public float moveDirection = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (transform.localPosition.x > rightLimit.localPosition.x)
        {
            moveDirection = -1f;
        }
        else if (transform.localPosition.x < leftLimit.localPosition.x)
        {
            moveDirection = 1f;
        }

        Vector2 movement = new Vector2(moveDirection * speed * Time.deltaTime, 0f);
        rb.velocity = movement;
    }
}
