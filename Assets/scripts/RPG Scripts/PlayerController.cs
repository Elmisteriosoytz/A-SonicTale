using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public bool isMoving;
    private Vector2 input;
    public Rigidbody2D rig;
    public LayerMask solidobjectslayer;
    public Animator anim;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                anim.SetFloat("moveX", input.x);
                anim.SetFloat("moveY", input.y);

                var targetpos = transform.position;
                targetpos.x += input.x;
                targetpos.y += input.y;

                if (isWalkable(targetpos))
                  StartCoroutine(Move(targetpos));
            }
        }

        anim.SetBool("isMoving", isMoving);
    }

    IEnumerator Move (Vector3 targetpos)
    {
        isMoving = true;

        while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetpos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos) {
        if(Physics2D.OverlapCircle(targetPos, 0.3f, solidobjectslayer) != null)
        {
            return false;
        }


        return true;
    }
}
