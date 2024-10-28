using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpaceDrifter2D
{
    public class SaveSystem : MonoBehaviour
    {

        // Œcie¿ka do pliku z danymi
        private string filePath;
        private void Awake()
        {
            filePath = Application.persistentDataPath + "/gameData.json";
        }

        // Zapis danych do JSON
        public void SaveGameData(GameData gameData)
        {
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(filePath, json);
            Debug.Log("Game data saved to " + filePath);
        }

        // Odczyt danych z JSON
        public GameData LoadGameData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);
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