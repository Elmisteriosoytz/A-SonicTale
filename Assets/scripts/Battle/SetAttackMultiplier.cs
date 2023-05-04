using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackMultiplier : MonoBehaviour
{
    public float Multiplier;
    public FightBoxCode fightBoxCode;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bar"))
        {
            fightBoxCode.AttackMultiplier = Multiplier;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
