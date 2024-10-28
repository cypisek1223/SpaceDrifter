using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpaceDrifter2D
{
   
    public class ManagerLevel : MonoBehaviour
    {
        [System.Serializable]
        public class LevelButtons
        {
            public Button button;
            public GameObject stars;
            public GameObject padlock;
        }

        [SerializeField] private LevelButtons[] levelButtons;
        [SerializeField] private LevelData[] levels;     
         [SerializeField] private PlanetData[] planets;
         [SerializeField] public Canvas LevelUI;


         private void Start()
         {
             // Przypisz ka¿demu przyciskowi odpowiedni¹ funkcjê z poziomem
             for (int i = 0; i < levelButtons.Length; i++)
             {
                 int index = i; 
                levelButtons[i].button.onClick.AddListener(() => OnLevelSelected(levels[index], planets[index]));
             }
            
            string sceneKey = $"{SceneManager.GetActiveScene().name}_FirstTime";
            if (PlayerPrefs.GetInt(sceneKey, 0) == 0)
            {
                //DO ODKOMENTOWANIA 
                //for (int i = 4; i < levelButtons.Length; i++)
                //{
                //    int index = i;
                //    levelButtons[i].button.interactable = false;
                //    levelButtons[i].stars.SetActive(false);
                //    levelButtons[i].padlock.SetActive(true);
                //}

            }
        }

         public void OnLevelSelected(LevelData level, PlanetData planetData)
         {
             GameManager.LevelLoad(level, planetData);
             //LevelUI.enabled = false;
         }

    }
}

