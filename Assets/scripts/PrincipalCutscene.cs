using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class PrincipalCutscene : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Text;
    public float timeToShowText = 5.0f;
    [TextArea(8, 5)]
    [SerializeField] string[] spanishTexts;
    [TextArea(8, 5)]
    [SerializeField] string[] englishTexts;
    public Sprite[] images;
    string[] texts;
    public string nextSceneName = "TitleScreen";
    private int currentImageIndex = 0;
    private int currentTextIndex = 0;
    public AudioSource source;
    public GameObject panel;

    public bool cerrarJuego;
    public MusicManager manager = null;
    public AudioClip mysteriousClip;

    [TextArea(1000, 1000)]
    public string content;

    void Start()
    {
        if (manager != null)
        {
            manager.Play();
        }

        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            texts = spanishTexts;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            texts = englishTexts;
        }


        // Configure initial image and text
        image.sprite = images[currentImageIndex];
        StartCoroutine(EffectTypeWritter(texts[currentTextIndex]));
        panel.SetActive(false);

        // Start text timer
        Invoke("ShowNextText", timeToShowText);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !cerrarJuego)
        {
            StopAllCoroutines();

            panel.SetActive(true);

            Invoke("LoadScene", 1.3f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void ShowNextText()
    {
        // Increment text index
        currentTextIndex++;
        currentImageIndex++;
        Text.text = "";

        // Check if we've reached the end of the text array
        if (currentTextIndex >= texts.Length && !cerrarJuego)
        {
            // Load next scene
            panel.SetActive(true);

            Invoke("LoadScene", 1.3f);

            return;
        }
        else if (currentTextIndex >= texts.Length && cerrarJuego == true)
        {
            panel.SetActive(true);

            source.PlayOneShot(mysteriousClip);

            // Nombre del archivo
            string nombreArchivo = "W D G.txt";

            // Contenido del archivo
            string contenido = content;

            // Ruta del archivo dentro de la carpeta del juego
            string rutaArchivo = Application.dataPath + "/" + nombreArchivo;

            // Crear el archivo y escribir el contenido en él
            File.WriteAllText(rutaArchivo, contenido);

            Debug.Log("Archivo de texto creado: " + rutaArchivo);

            Invoke("QuitGame", 5.5f);
        }
        else
        {
            // Show next image and text
            image.sprite = images[currentImageIndex];
            // Text.text = texts[currentTextIndex];
            StartCoroutine(EffectTypeWritter(texts[currentTextIndex]));

            // Start text timer
            Invoke("ShowNextText", timeToShowText);
        }
    }

    void QuitGame()
    {
        Debug.Log("GameQuited");
        Application.Quit();
    }

    public IEnumerator EffectTypeWritter(string text)
    {
        foreach (char character in text.ToCharArray())
        {
            Text.text += character;
            yield return new WaitForSeconds(0.04f);
            source.Play();
        }
    }
}

