using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpaceDrifter2D
{
    public class SaveSystem : MonoBehaviour
    {

        // Œcie¿ka do pliku z danymi
        private string filePath;
        [SerializeField] DataLevel gameData;
        private void Awake()
        {
            filePath = Application.persistentDataPath + "/gameData.json";
            SaveGameData(gameData);
            //LoadGameData();
        }
        private void OnApplicationQuit()
        {
            SaveGameData(gameData);
        }

        // Zapis danych do JSON
        public void SaveGameData(DataLevel _gameData)
        {
            string json = JsonUtility.ToJson(_gameData);
            File.WriteAllText(filePath, json);
            Debug.Log("Game data saved to " + filePath);
        }

        // Odczyt danych z JSON
        public DataLevel LoadGameData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                DataLevel gameData = JsonUtility.FromJson<DataLevel>(json);
                return gameData;
            }
            else
            {
                Debug.LogError("Save file not found in " + filePath);
                return null;
            }
        }
    }
}