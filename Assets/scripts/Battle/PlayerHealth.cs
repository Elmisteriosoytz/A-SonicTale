using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int HP;
    public int MaxHp;
    public Slider HpSlider;
    public TextMeshProUGUI HpText;
    public AudioSource source;
    public AudioClip damageClip;
    public AudioClip healClip;
    public GameObject GameOverScreen;
    public GameObject Music;
    public string scene;
    public AudioClip deathclip;

    public void TakeDamage(int Dmg)
    {
        HP -= Dmg;
        source.PlayOneShot(damageClip);
        HpSlider.value = (HP);
        HpText.text = $"{HP}/{MaxHp}";

        if(HP <= 0)
        {
            Death();
        }
    }

    public void TakeHeal(int Heal)
    {
        if (HP >= MaxHp)
            return;

        HP += Heal;
        source.PlayOneShot(healClip);
        HpSlider.value = (HP);
        HpText.text = $"{HP}/{MaxHp}";

        if (HP >= MaxHp)
        {
            HP = 200;
            HpSlider.value = (HP);
            HpText.text = $"{HP}/{MaxHp}";
        }
    }

    void Death()
    {
        StartCoroutine(Death12());
    }

    IEnumerator Death12()
    {
        Music.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        FindObjectOfType<TurnHandle>().state = BattleState.Lost;
        source.PlayOneShot(deathclip);
        GameOverScreen.SetActive(true);

        yield return new WaitForSeconds(50f);

        SceneManager.LoadScene(scene);
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
