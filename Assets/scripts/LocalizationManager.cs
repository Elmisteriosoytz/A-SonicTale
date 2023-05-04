using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [Header("Español")]
    [TextArea(8, 5)]
    [SerializeField] List<string> playerDialogSpanish;
    [SerializeField] List<Dialog> enemyDialogSpanish;
    [SerializeField] List<Dialog> randomEnemyDialogSpanish;
    [TextArea(8, 5)]
    [SerializeField] List<string> killerDialogOptionsSpanish;

    [Header("Cosas Especiales")]
    [TextArea(8,5)]
    [SerializeField] string CheckText;
    [TextArea(8, 5)]
    [SerializeField] string SonicDieText1Spanish;
    [TextArea(8, 5)]
    [SerializeField] string SonicDieText2Spanish;
    [SerializeField] List<Dialog> mercyDialogSpanish;

    [Header("English")]
    [TextArea(8, 5)]
    [SerializeField] List<string> playerDialogEnglish;
    [SerializeField] List<Dialog> enemyDialogEnglish;
    [SerializeField] List<Dialog> randomEnemyDialogEnglish;
    [TextArea(8, 5)]
    [SerializeField] List<string> killerDialogOptionsEnglish;

    [Header("Special Things")]
    [TextArea(8,6)]
    [SerializeField] string CheckTextEnglish;
    [TextArea(8, 5)]
    [SerializeField] string SonicDieText1English;
    [TextArea(8, 5)]
    [SerializeField] string SonicDieText2English;
    [SerializeField] List<Dialog> mercyDialogEnglish;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Spanish", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> PlayerDialog()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return playerDialogSpanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return playerDialogEnglish;
        }
        else
        {
            return null;
        }
    }

    public List<Dialog> EnemyDialog()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return enemyDialogSpanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return enemyDialogEnglish;
        }
        else
        {
            return null;
        }
    }

    public List<Dialog> RandomEnemyDialog()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return randomEnemyDialogSpanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return randomEnemyDialogEnglish;
        }
        else
        {
            return null;
        }
    }

    public List<string> KillerDialogOptions()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return killerDialogOptionsSpanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return killerDialogOptionsEnglish;
        }
        else
        {
            return null;
        }
    }

    public string CheckTextOption()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return CheckText;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return CheckTextEnglish;
        }
        else
        {
            return null;
        }
    }

    public string SonicDie1()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return SonicDieText1Spanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return SonicDieText1English;
        }
        else
        {
            return null;
        }
    }

    public string SonicDie2()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return SonicDieText2Spanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return SonicDieText2English;
        }
        else
        {
            return null;
        }
    }

    public List<Dialog> MercyDialog()
    {
        if (PlayerPrefs.GetInt("Spanish") == 1)
        {
            return mercyDialogSpanish;
        }
        else if (PlayerPrefs.GetInt("Spanish") == 0)
        {
            return mercyDialogEnglish;
        }
        else
        {
            return null;
        }
    }
}
