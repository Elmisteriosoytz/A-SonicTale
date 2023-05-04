using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBoxCode : MonoBehaviour
{
    public TurnHandle gameManager;
    public GameObject FightBox;
    public float AttackMultiplier;
    public int Attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gameManager.StopAllCoroutines();
        gameManager.PlayerDialogue.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Z))
       { 
            float v = Attack * AttackMultiplier;
            Attack = Mathf.FloorToInt(v);
            gameManager.Attack(Attack);

            FightBox.SetActive(false);
       }
    }
}
