using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState
{
    Nothing, // nothing, only wait for state
    Start,   //Start of the Battle
    PlayerTurn, //player action
    EnemyTalk, //enemy is talking
    EnemyTurn,   //enemy action
    FinishedTurn,   //all turns finished
    Won,     //no enemies left
    Lost     //player dead
}
public class TurnHandle : MonoBehaviour
{
    // Variables del sistema de batalla

    // Estado de la batalla
    public BattleState state;
    public TextMeshProUGUI CurrentTurn;

    // Enemigos en la batalla
    public EnemyProfile[] EnemiesInBattle;

    // Variables de control del turno de los enemigos
    public bool enemyActed;
    private GameObject[] EnemyAtks;
    public int currentTurn = 0;
    public int MaxTurn = 20;
    public float StartAnimDuration;

    // Elementos de interfaz de usuario
    public GameObject EnemyDialogBox;
    public TextMeshProUGUI PlayerDialogue;
    public Text EnemyDialogue;
    public GameObject PlayerUi;
    public heartControl PlayerHeart;

    // Objetos de la batalla
    [Header("Objetos de Batalla")]
    public GameObject Enemy;
    public WeaponProfile currentWeapon;
    public GameObject Slash;
    public Image[] EnemyVisuals;
    public GameObject Miss;
    public GameObject Damage;
    public string sceneToLoad;
    public EnemyManager enemyManager;
    public Slider DamageSlider;
    public TextMeshProUGUI damageText;

    // Objetos de diálogo
    [Header("Diálogo")]
    public TyperController typer;
    public TyperController Enemytyper;
    public AudioClip writeSound;
    public int index;

    public LocalizationManager localization;

    // Variables de control de diálogo
    public bool WasWrited { get; set; }
    public bool WasWritedEnemy { get; set; }

    // Animación y botones del jugador
    public Animator EnemyAnim;
    public Button[] playerButton;

    // Efectos de sonido
    [Header("SFX")]
    public AudioSource source;
    public AudioClip SlashSound;
    public AudioClip SonicDeathSound;
    public AudioClip SelectSound;
    public GameObject StartAnim;
    public MusicManager Music;

    public bool firstFight = false;
    public bool firstFightButton { get; set; }
    public bool SansLike = false;
    public bool SuperSonicBattle;
    bool isWriting = false;
    bool wasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBattle());
    }

    public IEnumerator StartBattle()
    {
        // Configuración de variables de inicio
        WasWritedEnemy = false; // Indica si el enemigo escribió su diálogo
        WasWrited = false; // Indica si el jugador escribió su diálogo
        firstFightButton = false;
        source = GetComponent<AudioSource>(); // Componente de audio de la escena
        EnemyAnim = Enemy.GetComponent<Animator>(); // Animador del enemigo
        StartAnim.SetActive(true);

        yield return new WaitForSeconds(StartAnimDuration);

        StartAnim.SetActive(false);
        state = BattleState.Start; // Establece el estado inicial de la batalla
        enemyActed = false; // El enemigo aún no ha actuado en el turno
    }

    int randomThing;
    int random;
    string[] finalList;
    string[] enemyDialogs;
    // Update is called once per frame
    void Update()
    {
        if (state == BattleState.Start)
        {
            //setup the level
            PlayerUi.SetActive(true);
            foreach (EnemyProfile emy in EnemiesInBattle)
            {
                EnemyVisuals[0].sprite = emy.EnemyVisual1;
                EnemyVisuals[1].sprite = emy.EnemyVisual2;

                enemyManager.profile = emy;
                DamageSlider.maxValue = emy.Hp;
            }
            
            PlayerDialogue.gameObject.SetActive(true);
            PlayerHeart.gameObject.SetActive(false);
            state = BattleState.PlayerTurn;

            //for each enemy in the enemy profile list, create an animated sprite for them
            //set this enemies health and charm hp to the profiles health and charm hp
        }
        else if (state == BattleState.PlayerTurn)
        {
            //wait for the player to attack
            

            if (currentTurn < localization.PlayerDialog().Count && localization.PlayerDialog()[currentTurn] != null)
            {
                List<string> playerDia = localization.PlayerDialog();

                string playerDialogue = playerDia[currentTurn];
                if (WasWrited == false)
                    typer.SetDialog(playerDialogue, PlayerDialogue);
            }
            else
            {
                // Aquí elegimos una opción de diálogo aleatoria de una lista predefinida de opciones
                if (WasWrited == false)
                {
                    List<string> killerDialogOptions = localization.KillerDialogOptions();

                    int randomIndex = Random.Range(0, killerDialogOptions.Count);
                    string randomDialog = killerDialogOptions[randomIndex];
                    typer.SetDialog(randomDialog, PlayerDialogue);
                }
            }
        }
        else if (state == BattleState.EnemyTalk)
        {
            if (mercyTurn == MaxMercy / 3 || mercyTurn == MaxMercy / 2 || mercyTurn >= MaxMercy)
            {
                enemyDialogs = (localization.MercyDialog()[currentMercy].texto);
                if (WasWritedEnemy == false)
                {
                    EnemyDialogBox.SetActive(true);
                    StartDialog(enemyDialogs);
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (EnemyDialogue.text == enemyDialogs[index] && isWriting == false)
                    {
                        NextLine(enemyDialogs);
                    }
                    else
                    {
                        StopAllCoroutines();
                        isWriting = false;
                        EnemyDialogue.text = enemyDialogs[index];
                    }
                }

                return;
            }


            if (currentTurn < localization.EnemyDialog().Count)
               enemyDialogs = (localization.EnemyDialog()[currentTurn].texto);

            if (currentTurn < localization.EnemyDialog().Count)
            {
                if (WasWritedEnemy == false)
                {
                    EnemyDialogBox.SetActive(true);
                    if (wasPlayed == false)
                    {
                        Music.Play();
                        wasPlayed = true;
                    }
                    StartDialog(enemyDialogs);
                }


                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (EnemyDialogue.text == enemyDialogs[index] && isWriting == false)
                    {
                        NextLine(enemyDialogs);
                    }
                    else
                    {
                        StopAllCoroutines();
                        isWriting = false;
                        EnemyDialogue.text = enemyDialogs[index];
                    }
                }
            }
            else
            {

                if (randomThing < 50)
                {
                    EnemyDialogBox.SetActive(false);
                    EnemyAnim.SetBool("moving", true);
                    state = BattleState.EnemyTurn;
                }
                else if (randomThing > 50 && WasWritedEnemy == false)
                {

                    int randomdialog2 = Random.Range(0, localization.RandomEnemyDialog().Count);
                    finalList = localization.RandomEnemyDialog()[randomdialog2].texto;
                    EnemyDialogBox.SetActive(true);
                    StartDialog(finalList);
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (EnemyDialogue.text == finalList[index] && isWriting == false)
                    {
                        NextLine(finalList);
                    }
                    else
                    {
                        StopAllCoroutines();
                        isWriting = false;
                        EnemyDialogue.text = finalList[index];
                    }
                }
            }
        }
        else if (state == BattleState.EnemyTurn)
        {
            if (EnemiesInBattle.Length <= 0)
            {
                //there no enemies so finish the enemy turn
                EnemyFinishedTurn();
            }
            else
            {
                if (!enemyActed)
                {
                    //turn on the player heart
                    PlayerHeart.gameObject.SetActive(true);
                    PlayerHeart.SetHeart();

                    //create all battle effects is enemy logics
                    foreach (EnemyProfile emy in EnemiesInBattle)
                    {
                        if (currentTurn < emy.EnemiesAttacks.Length)
                        {
                            Instantiate(emy.EnemiesAttacks[currentTurn], emy.StartPositions[currentTurn], Quaternion.identity);
                        }
                        else
                        {
                            int AtkNumb = Random.Range(0, emy.EnemiesAttacks.Length);
                            Instantiate(emy.EnemiesAttacks[AtkNumb], emy.StartPositions[AtkNumb], Quaternion.identity);
                        }
                    }

                    //find all attacks in scene to check when there done
                    EnemyAtks = GameObject.FindGameObjectsWithTag("Enemy");

                    enemyActed = true;
                }
                else
                {
                    bool enemyfin = true;
                    foreach (GameObject emy in EnemyAtks)
                    {
                        if (!emy.GetComponent<EnemyTurnHandle>().FinishedTurn)
                        {
                            enemyfin = false;
                        }
                    }

                    if (enemyfin)
                    {
                        EnemyFinishedTurn();
                    }
                }
            }
            //enemy take turn
        }
        else if (state == BattleState.FinishedTurn)
        {

            //turn is over turn off the player heart
            damageText.gameObject.SetActive(false);
            DamageSlider.gameObject.SetActive(false);
            PlayerHeart.ChangeState(PlayerState.Normal);
            randomThing = Random.Range(0, 100);
            PlayerHeart.gameObject.SetActive(false);
            WasWrited = false;
            WasWritedEnemy = false;
            foreach (Button but in playerButton)
            {
                but.interactable = true;
            }
            currentTurn++;

            if (currentTurn >= MaxTurn)
            {
                CurrentTurn.gameObject.SetActive(true);
                CurrentTurn.text = $"CurrentTurn: {currentTurn}";

                state = BattleState.Start;
            }

            //check if the player is alive
            if (PlayerHeart.GetComponent<PlayerHealth>().HP <= 0)
            {
                state = BattleState.Lost;
            }
            else
            {
                state = BattleState.Start;
            }

        }
        else if (state == BattleState.Won)
        {

        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && state == BattleState.EnemyTurn)
        {
            StopCoroutine("TypeText");
        }
    }

    public IEnumerator PlayerAttack(int AttackForce)
    {
        #region //SansLike Attack
        if (SansLike)
        {
            #region NormalSonicBattle
            if (!SuperSonicBattle)
            {
                if (currentTurn <= MaxTurn)
                {
                    //add enemy life

                    //attack effect
                    EnemyAnim.SetTrigger("Attack");
                    GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                    source.PlayOneShot(currentWeapon.AttackSound);
                    PlayerDialogue.gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.5f);
                    GameObject Missa = Instantiate(Miss, new Vector3(0, 4.5f, 0), Quaternion.identity);

                    Destroy(slash, 0.8f);
                    yield return new WaitForSeconds(0.8f);
                    Missa.GetComponent<Rigidbody2D>().gravityScale = 2;
                    Destroy(Missa, 3f);

                    if (firstFightButton == true)
                    {
                        firstFight = true;
                        firstFightButton = false;
                    }

                    yield return new WaitForSeconds(0.2f);

                    playerFinishedTurn();
                }
                else if (currentTurn > MaxTurn)
                {
                    Music.gameObject.SetActive(false);

                    GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                    source.PlayOneShot(SonicDeathSound);
                    source.PlayOneShot(currentWeapon.AttackSound);

                    GameObject damage = Instantiate(Damage, Enemy.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

                    EnemyAnim.SetTrigger("Die");

                    Destroy(damage, 2f);
                    typer.SetDialog(localization.SonicDie1(), PlayerDialogue);
                    Destroy(slash, 0.8f);

                    yield return new WaitForSeconds(3f);

                    typer.SetDialog(localization.SonicDie2(), PlayerDialogue);

                    yield return new WaitForSeconds(5f);

                    SceneManager.LoadScene(sceneToLoad);
                }
                #endregion
            }
            else if (currentWeapon.Name == "Shadow´s Gun" && SuperSonicBattle)
            {
                int TotalDamage = (int)((AttackForce += currentWeapon.Damage) * currentWeapon.DamageMultiplier);
                
                enemyManager.TakeDamane(TotalDamage);

                if (!EnemyDead)
                {
                    //attack effect
                    EnemyAnim.SetTrigger("Attack");
                    GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                    source.PlayOneShot(currentWeapon.AttackSound);
                    PlayerDialogue.gameObject.SetActive(false);
                    damageText.SetText($"{TotalDamage}");

                    damageText.gameObject.SetActive(true);
                    DamageSlider.gameObject.SetActive(true);

                    DamageSlider.value = enemyManager.HP;
                    yield return new WaitForSeconds(0.5f);

                    Destroy(slash, 0.8f);
                    yield return new WaitForSeconds(0.8f);

                    damageText.gameObject.SetActive(false);
                    DamageSlider.gameObject.SetActive(false);

                    yield return new WaitForSeconds(0.2f);

                    if (!EnemyDead)
                        playerFinishedTurn();
                }
                else if (EnemyDead)
                {
                    //agregar muerte de Super Sonic
                    //attack effect
                    Music.gameObject.SetActive(false);

                    GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                    GameObject slash2 = Instantiate(Slash, Enemy.transform.position, Quaternion.identity);
                    source.PlayOneShot(SonicDeathSound);
                    source.PlayOneShot(currentWeapon.AttackSound);
                    source.PlayOneShot(SlashSound);

                    GameObject damage = Instantiate(Damage, Enemy.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

                    EnemyAnim.SetTrigger("Die");

                    Destroy(damage, 2f);
                    typer.SetDialog("You Won, you gained 1000000000 EXP and 200 Rings", PlayerDialogue);
                    Destroy(slash, 0.8f);
                    Destroy(slash2, 0.8f);

                    yield return new WaitForSeconds(3f);

                    typer.SetDialog("its look like he won´t revive again", PlayerDialogue);

                    yield return new WaitForSeconds(5f);

                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                int TotalDamage = (int)((AttackForce += currentWeapon.Damage) * currentWeapon.DamageMultiplier * 0);

                enemyManager.TakeDamane(TotalDamage);

                EnemyAnim.SetTrigger("Attack");
                GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                source.PlayOneShot(currentWeapon.AttackSound);
                PlayerDialogue.gameObject.SetActive(false);
                damageText.SetText($"{TotalDamage}");

                damageText.gameObject.SetActive(true);
                DamageSlider.gameObject.SetActive(true);

                DamageSlider.value = enemyManager.HP;
                yield return new WaitForSeconds(0.5f);

                Destroy(slash, 0.8f);
                yield return new WaitForSeconds(0.8f);

                EnemyDialogBox.SetActive(true);
                enemyDialogs = new string[] { $"Sabes, soy inmune a tu {currentWeapon.Name}, deberias esforzarte mas" };
                StartDialog(enemyDialogs);

                damageText.gameObject.SetActive(false);
                DamageSlider.gameObject.SetActive(false);

                yield return new WaitForSeconds(4f);
                EnemyDialogBox.SetActive(false);
                EnemyDialogue.text = string.Empty;
                WasWrited = false;
                WasWritedEnemy = false;

                playerFinishedTurn();
            }
        }
        #endregion

        #region normalAttack
        if (!SansLike)
        {
            int TotalDamage = (int)((AttackForce += currentWeapon.Damage) * currentWeapon.DamageMultiplier);

            enemyManager.TakeDamane(TotalDamage);

            if (!EnemyDead)
            {
                //attack effect
                EnemyAnim.SetTrigger("Attack");
                GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                source.PlayOneShot(currentWeapon.AttackSound);
                PlayerDialogue.gameObject.SetActive(false);
                damageText.SetText($"{TotalDamage}");

                damageText.gameObject.SetActive(true);
                DamageSlider.gameObject.SetActive(true);

                DamageSlider.value = enemyManager.HP;
                yield return new WaitForSeconds(0.5f);

                Destroy(slash, 0.8f);
                yield return new WaitForSeconds(0.8f);

                damageText.gameObject.SetActive(false);
                DamageSlider.gameObject.SetActive(false);

                yield return new WaitForSeconds(0.2f);

                if (!EnemyDead)
                    playerFinishedTurn();
            }
            else if (EnemyDead)
            {
                //agregar muerte de Super Sonic
                //attack effect
                Music.gameObject.SetActive(false);

                GameObject slash = Instantiate(currentWeapon.WeaponAttack, Enemy.transform.position, Quaternion.identity);
                source.PlayOneShot(SonicDeathSound);
                source.PlayOneShot(currentWeapon.AttackSound);
                source.PlayOneShot(SlashSound);

                GameObject damage = Instantiate(Damage, Enemy.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

                EnemyAnim.SetTrigger("Die");

                Destroy(damage, 2f);
                typer.SetDialog($"You Won, you gained {EnemiesInBattle[0].EXP} EXP and {EnemiesInBattle[0].Rings} Rings", PlayerDialogue);
                Destroy(slash, 0.8f);

                yield return new WaitForSeconds(5f);

                SceneManager.LoadScene(sceneToLoad);
            }
        }
        #endregion
    }

    public bool EnemyDead = false;

    public IEnumerator PlayerHealth(int life)
    {
        source.PlayOneShot(SelectSound);

        PlayerHeart.GetComponent<PlayerHealth>().TakeHeal(life);

        typer.SetDialog($"  * Food healed you {life} PS", PlayerDialogue);

        yield return new WaitForSeconds(3f);

        playerFinishedTurn();
    }

    public IEnumerator PlayerChangeWeapon(WeaponProfile newWeapon)
    {
        source.PlayOneShot(SelectSound);

        currentWeapon = newWeapon;

        typer.SetDialog($"You are now using {newWeapon.Name}", PlayerDialogue);

        yield return new WaitForSeconds(3f);

        playerFinishedTurn();
    }

    public void PlayerWeapon(WeaponProfile weapon)
    {
        StartCoroutine(PlayerChangeWeapon(weapon));
    }

    public void PlayerItem(int hp)
    {
        StartCoroutine(PlayerHealth(hp));
    }

    public void Attack(int attack)
    {
        StartCoroutine(PlayerAttack(attack));
    }

    public void PlayerAct()
    {
        //bring up the menu

        StartCoroutine(PlayerCheck());
    }

    public int mercyTurn;
    public int MaxMercy = 10;
    int currentMercy = 0;
    public void Mercy()
    {
        // jajaja nothing because is like sans battle
        mercyTurn++;

        if (mercyTurn == MaxMercy)
        {
            StartCoroutine(MercySpecial());
            return;
        }

        playerFinishedTurn();
    }

    public IEnumerator MercySpecial()
    {
        //añadir final bueno XD
        Music.gameObject.SetActive(false);

        SceneManager.LoadScene("GoodEnding");

        yield return null;
    }

    public IEnumerator PlayerCheck()
    {
        source.PlayOneShot(SelectSound);

        typer.SetDialog(localization.CheckTextOption(), PlayerDialogue);
        
        yield return new WaitForSeconds(7f);

        playerFinishedTurn();
    }

    public void PlaySelectSound()
    {
        source.PlayOneShot(SelectSound);
    }

    void playerFinishedTurn()
    {
        //once the players turn has enden
        PlayerUi.SetActive(false);

        state = BattleState.EnemyTalk;
    }


    void EnemyFinishedTurn()
    {
        //destroy all attacks
        foreach(GameObject obj in EnemyAtks)
        {
            Destroy(obj);
        }

        enemyActed = false;

        state = BattleState.FinishedTurn;
    }

    public void StartDialog(string[] text)
    {
        index = 0;
        EnemyDialogue.text = string.Empty;
        StartCoroutine(EffectTypeWritterEnemy(text));
    }

    public IEnumerator EffectTypeWritterEnemy(string[] text)
    {
        isWriting = true;

        foreach (char character in text[index].ToCharArray())
        {
            WasWritedEnemy = true;
            source.PlayOneShot(writeSound);
            EnemyDialogue.text += character;
            yield return new WaitForSeconds(0.04f);
        }

        isWriting = false;
    }

    void NextLine(string[] text)
    {
        if (index < text.Length - 1)
        {
            index++;
            EnemyDialogue.text = string.Empty;
            StartCoroutine(EffectTypeWritterEnemy(text));
        }
        else
        {
            if (mercyTurn == MaxMercy / 2 || mercyTurn == MaxMercy / 3 || mercyTurn >= MaxMercy)
                currentMercy++;

            EnemyDialogBox.SetActive(false);
            EnemyAnim.SetBool("moving", true);
            state = BattleState.EnemyTurn;
        }
    }
}
