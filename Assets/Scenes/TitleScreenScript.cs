using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public string nextSceneName;
    public GameObject panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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
}
