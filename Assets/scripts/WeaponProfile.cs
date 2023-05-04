using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponProfile : ScriptableObject
{
    public string Name;
    public int Damage;
    public float DamageMultiplier;

    public Sprite WeaponSprite;
    public AudioClip AttackSound;
    public GameObject WeaponAttack;
}
