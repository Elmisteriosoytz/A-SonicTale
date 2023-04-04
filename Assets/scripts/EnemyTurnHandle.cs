using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandle : MonoBehaviour
{
    public bool FinishedTurn;
    public int AttackAmounts;
    public int atkNumb1;

    // Start is called before the first frame update
    void Start()
    {
        FinishedTurn = false;

        int atkNumb = Random.Range(0, AttackAmounts);
        atkNumb1 = atkNumb;
        GetComponent<Animator>().SetInteger("AtkDex", atkNumb);
    }

    public void AtkDone()
    {
        FinishedTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
