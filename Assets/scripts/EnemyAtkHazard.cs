using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkHazard : MonoBehaviour
{
    public int Damage;
    public bool changeToColor;
    public PlayerState newState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(Damage);

            if (changeToColor)
            {
                collision.GetComponent<heartControl>().ChangeState(newState);
            }
        }
    }
}
