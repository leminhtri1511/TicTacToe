using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TTT.Scripts.Data
{
    public class GameDataService : MonoBehaviour
    {
        public static GameDataService Instance { get; private set; }

        public GameData Data { get; private set; }

        private const string SAVE_KEY = "GAME_DATA";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public async UniTask InitializeAsync()
        {
            await UniTask.Yield();

            Load();
        }

        private void Load()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY))
            {
                Data = new GameData();
                return;
            }

            string json = PlayerPrefs.GetString(SAVE_KEY);

            Data = JsonUtility.FromJson<GameData>(json) ?? new GameData();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(Data);

            PlayerPrefs.SetString(
                SAVE_KEY,
                json
            );

            PlayerPrefs.Save();
        }

        public void ResetData()
        {
            Data = new GameData();

            Save();
        }
    }
}