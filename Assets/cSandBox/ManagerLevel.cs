using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceDrifter2D
{
    public class ManagerLevel : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;  // Tablica przycisków do poziomów
    [SerializeField] private LevelData[] levels;     // Tablica poziomów
    [SerializeField] private PlanetData[] planets;   // Tablica planet danych
    [SerializeField] private Canvas levelUI;         // UI, które bêdzie wy³¹czane

    private void Start()
    {
        // Przypisz ka¿demu przyciskowi odpowiedni¹ funkcjê z poziomem
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;  // Kopia zmiennej i, aby unikn¹æ problemów z przekazywaniem referencji
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(levels[index], planets[index]));
        }
    }

    public void OnLevelSelected(LevelData level, PlanetData planetData)
    {
        // £aduj poziom z odpowiednimi danymi
        GameManager.LevelLoad(level, planetData);
        levelUI.enabled = false;  // Wy³¹cz interfejs
    }

    }
}