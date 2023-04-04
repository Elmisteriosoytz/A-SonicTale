using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Referencia al menú desplegable de selección de objetivo
    public Dropdown targetFightDropdown;

    // Referencia al botón de confirmación de selección de objetivo
    public Button FightButton;

    // Referencia al sistema de batalla
    public TurnHandle battleSystem;

    // Este método se llama cuando se carga la escena
    private void Start()
    {
        // Crea una lista de opciones para el menú desplegable
        List<string> targetOptions = new List<string>();
        foreach (EnemyProfile emy in battleSystem.EnemiesInBattle)
        {
            targetOptions.Add(emy.Name); // Agrega la opción de name a la lista
        }

        // Agrega la lista de opciones al menú desplegable
        targetFightDropdown.AddOptions(targetOptions);
    }

    // Este método se llama cuando este script se habilita
    private void OnEnable()
    {
        // Agrega un listener al botón de confirmación de selección de objetivo
        FightButton.onClick.AddListener(OnFightButtonClicked);
    }

    // Este método se llama cuando este script se deshabilita
    private void OnDisable()
    {
        // Elimina el listener del botón de confirmación de selección de objetivo
        FightButton.onClick.RemoveListener(OnFightButtonClicked);
    }

    // Este método se llama cuando el jugador hace clic en el botón de confirmación de selección de objetivo
    private void OnFightButtonClicked()
    {
        battleSystem.PlayerAttack();
    }
}
