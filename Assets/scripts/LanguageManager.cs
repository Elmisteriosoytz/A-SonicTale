using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject Panel;

    public void SetSpanish()
    {
        PlayerPrefs.SetInt("Spanish", 1);
        PlayerPrefs.Save();

        Panel.SetActive(true);
        Invoke("LoadScene", 2f);
    }

    public void SetEnglish()
    {
        PlayerPrefs.SetInt("Spanish", 0);
        PlayerPrefs.Save();

        Panel.SetActive(true);
        Invoke("LoadScene", 2f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
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
