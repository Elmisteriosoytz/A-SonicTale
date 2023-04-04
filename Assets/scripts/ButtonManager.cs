using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Referencia al men� desplegable de selecci�n de objetivo
    public Dropdown targetFightDropdown;

    // Referencia al bot�n de confirmaci�n de selecci�n de objetivo
    public Button FightButton;

    // Referencia al sistema de batalla
    public TurnHandle battleSystem;

    // Este m�todo se llama cuando se carga la escena
    private void Start()
    {
        // Crea una lista de opciones para el men� desplegable
        List<string> targetOptions = new List<string>();
        foreach (EnemyProfile emy in battleSystem.EnemiesInBattle)
        {
            targetOptions.Add(emy.Name); // Agrega la opci�n de name a la lista
        }

        // Agrega la lista de opciones al men� desplegable
        targetFightDropdown.AddOptions(targetOptions);
    }

    // Este m�todo se llama cuando este script se habilita
    private void OnEnable()
    {
        // Agrega un listener al bot�n de confirmaci�n de selecci�n de objetivo
        FightButton.onClick.AddListener(OnFightButtonClicked);
    }

    // Este m�todo se llama cuando este script se deshabilita
    private void OnDisable()
    {
        // Elimina el listener del bot�n de confirmaci�n de selecci�n de objetivo
        FightButton.onClick.RemoveListener(OnFightButtonClicked);
    }

    // Este m�todo se llama cuando el jugador hace clic en el bot�n de confirmaci�n de selecci�n de objetivo
    private void OnFightButtonClicked()
    {
        battleSystem.PlayerAttack();
    }
}
