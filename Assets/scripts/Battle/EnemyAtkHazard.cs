using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackState { normal, blue, orange }
public class EnemyAtkHazard : MonoBehaviour
{
    public int Damage;
    public bool changeToColor;
    public PlayerState newState;
    public AttackState state;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (changeToColor)
        {
            collision.GetComponent<heartControl>().ChangeState(newState);
        }

        if (state != AttackState.blue)
        {
            if (collision.GetComponent<PlayerHealth>())
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(Damage);
            }
        }
        else
        {
            if (collision.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                Debug.Log("Good Job");

                return;
            }
            else
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(Damage);

                Debug.LogError("its moving");
            }
        }
    }
}
