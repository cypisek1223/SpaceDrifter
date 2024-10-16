using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceDrifter2D
{
    public class ManagerLevel : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;  // Tablica przycisk�w do poziom�w
    [SerializeField] private LevelData[] levels;     // Tablica poziom�w
    [SerializeField] private PlanetData[] planets;   // Tablica planet danych
    [SerializeField] private Canvas levelUI;         // UI, kt�re b�dzie wy��czane

    private void Start()
    {
        // Przypisz ka�demu przyciskowi odpowiedni� funkcj� z poziomem
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;  // Kopia zmiennej i, aby unikn�� problem�w z przekazywaniem referencji
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(levels[index], planets[index]));
        }
    }

    public void OnLevelSelected(LevelData level, PlanetData planetData)
    {
        // �aduj poziom z odpowiednimi danymi
        GameManager.LevelLoad(level, planetData);
        levelUI.enabled = false;  // Wy��cz interfejs
    }

    }
}