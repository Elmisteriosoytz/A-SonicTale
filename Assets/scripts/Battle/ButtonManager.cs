using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void ChangeWeapon(WeaponProfile weapon)
    {
        StartCoroutine(FindObjectOfType<TurnHandle>().PlayerChangeWeapon(weapon));
    }
}
