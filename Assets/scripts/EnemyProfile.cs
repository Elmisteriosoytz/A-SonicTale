using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyProfile : ScriptableObject
{
    public string Name;
    public int Hp;
    public float Defense;
    public int EXP;
    public int Rings;

    public Sprite EnemyVisual1;
    public Sprite EnemyVisual2;

    public GameObject[] EnemiesAttacks;
    public Vector3[] StartPositions;
}
