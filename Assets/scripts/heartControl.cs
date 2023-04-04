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
    public Rigidbody2D rig;
    public float damagetimer;
    public SpriteRenderer sprite;
    public int jumpForce;
    Solid ghosts;

    public void SetHeart()
    {
        sprite = GetComponent<SpriteRenderer>();
        ghosts = GetComponent<Solid>();
        transform.position = Startingpos;
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
            rig.isKinematic = true;

            Color HeartColor = new Color(1f, 0f, 0f, 1f); // Crear un color rojo brillante

            sprite.color = HeartColor;
        }
        else if (state == PlayerState.Orange)
        {
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
            //añadir plataformeo
            rig.isKinematic = false; 
            rig.gravityScale = 1;

            Color blueColor = Color.HSVToRGB(180f / 360f, 0.7f, 0.9f);

            sprite.color = blueColor;

            // Obtener entrada de teclado
            float horizontalInput = Input.GetAxis("Horizontal");
            bool jumpInput = Input.GetKeyDown(KeyCode.UpArrow);

            // Calcular la dirección del movimiento
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, 0f);

            // Aplicar fuerza de salto si se presiona la tecla de salto
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rig.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            }

            // Mover el objeto utilizando el Rigidbody
            rig.velocity = new Vector3(movementDirection.x * speed, rig.velocity.y, 0f);
        }
        
        if (state != PlayerState.celeste)
        {
            speed = 5;
        }
    }

    private void FixedUpdate()
    {
        if (state != PlayerState.Blue)
        {
            // Obtener entrada de teclado
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcular la dirección del movimiento
            Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f);

            // Mover el objeto utilizando el Rigidbody
            rig.velocity = movementDirection * speed;

            // Limitar el movimiento del objeto
            float newX = Mathf.Clamp(transform.position.x, -horizontalLimit, horizontalLimit);
            float newY = Mathf.Clamp(transform.position.y, minusVerticalLimit, verticalLimit);
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
}
