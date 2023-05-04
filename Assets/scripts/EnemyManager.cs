using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyProfile profile;
    public int HP;

    public void TakeDamane(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            FindObjectOfType<TurnHandle>().EnemyDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = profile.Hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
