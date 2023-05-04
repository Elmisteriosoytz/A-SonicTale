using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Normal, //nothing
    Orange, // if player don´t move he gets damage
    Blue, // platformer like
    celeste, // velocidad aumentada
}

public class heartControl : MonoBehaviour
{
    public Vector2 Startingpos;
    public float speed = 5f; // velocidad de movimiento del corazón
    public float horizontalLimit = 1.4f; // límite horizontal de movimiento del corazón
    public float verticalLimit = 0.4f; // límite vertical de movimiento del corazón
    public float minusVerticalLimit;
    public PlayerState state;
    public float damagetimer;
    public SpriteRenderer sprite;
    Solid ghosts;

    [Header("BlueMovement")]
    public Rigidbody2D rig;
    public float speed1 = 5;
    public float JumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float CheckRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float JumpTime;
    private bool isJumping;


    [Header("Debug")]
    [SerializeField] bool debug = false;

    public void SetHeart()
    {
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        ghosts = GetComponent<Solid>();
        transform.position = Startingpos;
    }

    private void Start()
    {
        if (debug)
        {
            SetHeart();
        }
    }

    public void ChangeState(PlayerState newState)
    {
        state = newState;
    }

    void Update()
    {
        if (state != PlayerState.Orange)
        {
            ghosts.makeGhost = false;
        }

        if (state == PlayerState.Normal)
        {
            rig.isKinematic = false;
            rig.gravityScale = 0;

            Color HeartColor = new Color(1f, 0f, 0f, 1f); // Crear un color rojo brillante

            sprite.color = HeartColor;
        }
        else if (state == PlayerState.Orange)
        {
            rig.isKinematic = false;
            rig.gravityScale = 0;
            // Convertir el valor HSV de naranja (30, 1, 1) a RGB
            Color orangeColor = Color.HSVToRGB(30f / 360f, 1f, 1f);

            //cambiar el color
            sprite.color = orangeColor;

            // si no se mueve dañar al jugador
            if (rig.velocity != Vector2.zero)
            {
                ghosts.makeGhost = true;
                return;
            }
            else if (damagetimer <= 0f)
            {
                ghosts.makeGhost = false;
                damagetimer = 0.5f;
                this.GetComponent<PlayerHealth>().TakeDamage(2);
            }
            else if (damagetimer > 0)
            {
                ghosts.makeGhost = false;
                damagetimer -= Time.deltaTime;
            }
        }
        else if (state == PlayerState.celeste)
        {
            rig.isKinematic = false;
            rig.gravityScale = 0;

            //cambiar el color
            Color LightblueColor = Color.HSVToRGB(210f / 360f, 0.67f, 0.93f);

            //cambiar el color
            sprite.color = LightblueColor;

            speed = 20;
        }
        else if (state == PlayerState.Blue)
        {
            //preparar todo
            rig.isKinematic = false;
            rig.gravityScale = 1;

            Color blueColor = new Color(0 / 255f, 58 / 255f, 255 / 255f);

            sprite.color = blueColor;

            //plataformeo
            isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);

            if (isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
            {
                isJumping = true;
                jumpTimeCounter = JumpTime;
                rig.velocity = Vector2.up * JumpForce;
            }

            if (Input.GetKey(KeyCode.UpArrow) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rig.velocity = Vector2.up * JumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                isJumping = false;
            }
        }
        
        if (state != PlayerState.celeste)
        {
            speed = 5;
        }
    }

    private void FixedUpdate()
    {
        if (state == PlayerState.Blue)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rig.velocity = new Vector2(moveInput * speed1, rig.velocity.y);
        }
        else if (state != PlayerState.Blue)
        {
            // Obtener entrada de teclado
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcular la dirección del movimiento
            Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f);

            // Mover el objeto utilizando el Rigidbody
            rig.velocity = movementDirection * speed;
        }
    }
}
