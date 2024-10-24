using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceDrifter2D
{
    public class ManagerLevel : MonoBehaviour
    {
         [SerializeField] private Button[] levelButtons;
         [SerializeField] private LevelData[] levels;     
         [SerializeField] private PlanetData[] planets;
         [SerializeField] public Canvas LevelUI;

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
             GameManager.LevelLoad(level, planetData);
              LevelUI.enabled = false;
         }

    }
}

