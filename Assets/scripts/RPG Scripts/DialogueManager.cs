using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image portrait;
    public string sceneToLoad;
    public GameObject panel;
    public TextMeshProUGUI text;
    int index = 0;
    bool isWriting = false;

    private Dialogue sentences;
    public AudioSource source;
    public AudioClip writeSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] dialogsss = sentences.dialogs;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (text.text == dialogsss[index] && isWriting == false)
            {
                NextLine(dialogsss);
            }
            else
            {
                StopAllCoroutines();
                isWriting = false;
                text.text = dialogsss[index];
            }
        }
    }

    public void StartDialog(Dialogue dialog)
    {
        sentences = dialog;
        index = 0;
        string[] dialogsss = sentences.dialogs;
        text.text = string.Empty;
        portrait.sprite = sentences.image[index];
        StartCoroutine(TypeSentence(dialogsss));
    }

    

    IEnumerator TypeSentence (string[] sentencesss)
    {
        isWriting = true;

        foreach (char character in sentencesss[index].ToCharArray())
        {
            source.PlayOneShot(sentences.sounds[index]);
            text.text += character;
            yield return new WaitForSeconds(0.04f);
        }

        isWriting = false;
    }

    void NextLine(string[] textf)
    {
        if (index < textf.Length - 1)
        {
            index++;
            text.text = string.Empty;
            portrait.sprite = sentences.image[index];
            StartCoroutine(TypeSentence(textf));
        }
        else
        {
            StartCoroutine(EndDialog());
        }
    }

    
    public IEnumerator EndDialog()
    {
        panel.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneToLoad);
    }
}
