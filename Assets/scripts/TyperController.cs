using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TyperController : MonoBehaviour
{
    public TurnHandle gameManager;
    public float textDelay = 0.003f;
    public AudioSource source;
    public AudioClip writeSound;
    public int index;
    public bool isEnemy = false;

    public void SetDialog(string text, TextMeshProUGUI controller, string[] enemyText = null)
    {
        if (isEnemy == false)
            gameManager.WasWrited = false;
        else if (isEnemy == true)
            gameManager.WasWritedEnemy = false;

        controller.text = "";
        StopAllCoroutines();

        if (isEnemy == false)
            StartCoroutine(EffectTypeWritter(text, controller));
        else if (isEnemy == true)
        {
            index = 0;

            //StartCoroutine(EffectTypeWritterEnemy(text, controller));
        }
    }

    public IEnumerator EffectTypeWritter(string text, TextMeshProUGUI controller)
    {
        foreach (char character in text.ToCharArray())
        {
            gameManager.WasWrited = true;
            
            controller.text += character;
            yield return new WaitForSeconds(textDelay);
            source.Play();
        }
    }

    public IEnumerator EffectTypeWritterEnemy(string text, Text controller)
    {
        foreach (char character in text.ToCharArray())
        {
            gameManager.WasWritedEnemy = true;
            source.PlayOneShot(writeSound);
            controller.text += character;
            yield return new WaitForSeconds(textDelay);
        }
    }

    void NextLine()
    {

    }
}
