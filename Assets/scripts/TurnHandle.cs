using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RedBlueGames.Tools.TextTyper;

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

    // Enemigos en la batalla
    public EnemyProfile[] EnemiesInBattle;

    // Variables de control del turno de los enemigos
    private bool enemyActed;
    private GameObject[] EnemyAtks;
    public int currentTurn = 0;
    public int MaxTurn = 20;

    // Elementos de interfaz de usuario
    public GameObject EnemyDialogBox;
    public TextMeshProUGUI PlayerDialogue;
    public Text EnemyDialogue;
    public GameObject PlayerUi;
    public heartControl PlayerHeart;

    // Objetos de la batalla
    [Header("Objetos de Batalla")]
    public GameObject Enemy;
    public GameObject Slash;
    public GameObject Miss;
    public GameObject Damage;

    // Objetos de diálogo
    [Header("Diálogo")]
    public TyperController typer;
    public TyperController Enemytyper;
    public string CheckText;
    [TextArea(8, 5)]
    public List<string> playerDialog;
    [SerializeField] List<Dialog> enemyDialog;
    [SerializeField] List<Dialog> randomEnemyDialog;
    [TextArea(5, 5)]
    [SerializeField] string[] firstFightDialog;
    public AudioClip writeSound;
    public int index;

    // Variables de control de diálogo
    public bool WasWrited { get; set; }
    public bool WasWritedEnemy { get; set; }

    // Animación y botones del jugador
    public Animator SonicAnim;
    public Button[] playerButton;

    // Efectos de sonido
    [Header("SFX")]
    public AudioSource source;
    public AudioClip SlashSound;
    public AudioClip SonicDeathSound;
    public AudioClip SelectSound;
    public GameObject StartAnim;
    public GameObject Music;

    public bool firstFight = false;
    public bool firstFightButton { get; set; }
    bool isWriting = false;

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
        SonicAnim = Enemy.GetComponent<Animator>(); // Animador del enemigo
        StartAnim.SetActive(true);

        yield return new WaitForSeconds(1f);

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
            
            PlayerDialogue.gameObject.SetActive(true);
            PlayerHeart.gameObject.SetActive(false);
            state = BattleState.PlayerTurn;

            //for each enemy in the enemy profile list, create an animated sprite for them
            //set this enemies health and charm hp to the profiles health and charm hp
        }
        else if (state == BattleState.PlayerTurn)
        {
            //wait for the player to attack
            

            if (currentTurn < playerDialog.Count && playerDialog[currentTurn] != null)
            {
                string playerDialogue = playerDialog[currentTurn];
                if (WasWrited == false)
                    typer.SetDialog(playerDialogue, PlayerDialogue);
            }
            else
            {
                // Aquí elegimos una opción de diálogo aleatoria de una lista predefinida de opciones
                if (WasWrited == false)
                {
                    List<string> killerDialogOptions = new List<string>() {
                    "  * Le dices a Sonic que no hay esperanza, parece no darle importancia",
                    "  * Acaba con él de una vez",
                    "  * Es inútil intentar huir, Sonic.",
                    "  * Mantente Determinado, en el momento que lo logres lo gozaras como nada",
                    "  * Ya era hora de que alguien pusiera fin a su carrera.",
                    "  * Le dices a Sonic que tu eres más fuerte y Sonic parece saberlo pero no le importa",
                    "  * YOU ARE STRONGER THAN HIM"
                    };
                    int randomIndex = Random.Range(0, killerDialogOptions.Count);
                    string randomDialog = killerDialogOptions[randomIndex];
                    typer.SetDialog(randomDialog, PlayerDialogue);
                }
            }
        }
        else if (state == BattleState.EnemyTalk)
        {
            Music.SetActive(true);

            if (currentTurn < enemyDialog.Count)
               enemyDialogs = (enemyDialog[currentTurn].texto);

            if (currentTurn < enemyDialog.Count)
            {
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
                        //StopCoroutine(EffectTypeWritterEnemy(enemyDialogs));
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
                    SonicAnim.SetBool("moving", true);
                    state = BattleState.EnemyTurn;
                }
                else if (randomThing > 50 && WasWritedEnemy == false)
                {
                    string[] enemyRandomDialogs = { "Asi que no quisiste reinvidicarte", "aun asi no sere TANNNNNNNNN duro contigo" };
                    string[] enemyRandomDialogs1 = { "Estas pasando un mal rato?", "espero que si" };
                    string[] enemyRandomDialogs2 = { "Aprendi a hablar asi despues de jugar Undertale", "no sabia que pasaria en la vida real" };
                    string[] enemyRandomDialogs3 = { "¿Sigues aqui?", "parece que estas determinado a matarme" };
                    string[] enemyRandomDialogs4 = { "eeeeeeeeeeeeeeeeee", "jaja hablo como Sans" };

                    int randomdialog2 = Random.Range(0, randomEnemyDialog.Count);
                    finalList = randomEnemyDialog[randomdialog2].texto;
                    //finalList.Add(enemyRandomDialogs);
                    //finalList.Add(enemyRandomDialogs1);
                    //finalList.Add(enemyRandomDialogs2);
                    //finalList.Add(enemyRandomDialogs3);
                    ///finalList.Add(enemyRandomDialogs4);
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
                        //StopCoroutine(EffectTypeWritterEnemy(enemyDialogs));
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
            //EnemyDialogue.text = textToType;
        }
    }

    public IEnumerator PlayerAttack()
    {
        if (currentTurn <= MaxTurn)
        {
            //addd minigame

            //add enemy life

            //attack effect
            SonicAnim.SetTrigger("Attack");
            GameObject slash = Instantiate(Slash, Enemy.transform.position, Quaternion.identity);
            source.PlayOneShot(SlashSound);
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
            Music.SetActive(false);

            GameObject slash = Instantiate(Slash, Enemy.transform.position, Quaternion.identity);
            source.PlayOneShot(SonicDeathSound);
            source.PlayOneShot(SlashSound);

            GameObject damage = Instantiate(Damage, Enemy.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

            SonicAnim.SetTrigger("Die");

            Destroy(damage, 2f);
            typer.SetDialog("  * lo lograste", PlayerDialogue);
            Destroy(slash, 0.8f);

            yield return new WaitForSeconds(8f);

            Application.Quit();
        }
    }

    public IEnumerator PlayerHealth(int life)
    {
        source.PlayOneShot(SelectSound);

        PlayerHeart.GetComponent<PlayerHealth>().TakeHeal(life);

        typer.SetDialog($"  * Food healed you {life} PS", PlayerDialogue);

        yield return new WaitForSeconds(3f);

        playerFinishedTurn();
    }

    public void PlayerItem(int hp)
    {
        StartCoroutine(PlayerHealth(hp));
    }

    public void Attack()
    {
        StartCoroutine(PlayerAttack());
    }

    public void PlayerAct()
    {
        //bring up the menu

        StartCoroutine(PlayerCheck());
    }

    int mercyTurn;

    public void Mercy()
    {
        // jajaja nothing because is like sans battle
        mercyTurn++;

        if (mercyTurn == 25)
        {
            StartCoroutine(MercySpecial());
            return;
        }

        playerFinishedTurn();
    }

    public IEnumerator MercySpecial()
    {
        //añadir final bueno XD

        yield return null;
    }

    public IEnumerator PlayerCheck()
    {
        source.PlayOneShot(SelectSound);

        typer.SetDialog(CheckText, PlayerDialogue);

        yield return new WaitForSeconds(5f);

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
            EnemyDialogBox.SetActive(false);
            SonicAnim.SetBool("moving", true);
            state = BattleState.EnemyTurn;
        }
    }
}
