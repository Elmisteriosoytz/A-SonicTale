using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerDetector : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject Tails;
    public Dialogue dialog;
    public GameObject dialogBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCamera.GetComponent<CinemachineBrain>().enabled = false;
            mainCamera.transform.position = new Vector3(28f, -4f, -10f);
            Tails.transform.position = new Vector3(24.5f, -2.5f, 0);
            Tails.GetComponent<PlayerController>().anim.SetFloat("moveX", 1);
            Tails.GetComponent<PlayerController>().anim.SetFloat("moveX", 1);
            Tails.GetComponent<PlayerController>().anim.SetBool("isMoving", false);
            Tails.GetComponent<PlayerController>().enabled = false;

            dialogBox.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialog(dialog);
        }
    }
}
