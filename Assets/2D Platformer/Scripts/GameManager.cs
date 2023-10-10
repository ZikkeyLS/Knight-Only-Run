using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int coinsCounter = 0;
        public bool useFPSLimit = true;

        public AudioSource audioSource;

        public AudioClip checkpointSound;
        public float checkpointVolume = 0.1f;

        public AudioClip winSound;
        public float winVolume = 0.1f;

        public GameObject playerGameObject;
        private PlayerController player;
        public GameObject deathPlayerPrefab;
        public WinScreen winScreen;
        public TMPro.TMP_Text coinText;

        private Checkpoint _checkpoint;
        private int _checkpointCoins;

        private GameObject _deathPlayer;

        private const int TargetFPS = 60;
        private const string MainMenuName = "MainMenu";

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Instance of GameManager already exists!");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            if (useFPSLimit)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = TargetFPS;
            }

            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        private void Update()
        {
            coinText.text = coinsCounter.ToString();
            if(player.deathState == true)
            {
                playerGameObject.SetActive(false);
                _deathPlayer = Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
                _deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                player.deathState = false;
                Invoke("RetryLevel", 2);
            }
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void RetryLevel()
        {
            if (_checkpoint == null)
            {
                Reload();
                return;
            }

            Destroy(_deathPlayer);
            playerGameObject.SetActive(true);
            playerGameObject.transform.position = new Vector3(_checkpoint.transform.position.x, _checkpoint.transform.position.y, playerGameObject.transform.position.z);
            coinsCounter = _checkpointCoins;        
        }

        public void TrySetCheckpoint(Checkpoint checkpoint)
        {
            if (checkpoint == _checkpoint)
                return;

            PlaySound(checkpointSound, checkpointVolume);
            _checkpoint = checkpoint;
            _checkpointCoins = coinsCounter;
        }

        public void PlaySound(AudioClip sound, float volume)
        {
            audioSource.PlayOneShot(sound, volume);
        }

        public void Win()
        {
            player.lockMovement = true;
            PlaySound(winSound, winVolume);

            if (GameData.Instance.Levels.Count != GameData.Instance.CurrentLevel - 1)
                GameData.Instance.CurrentLevel += 1;

            if (GameData.Instance.Values.MaxLevel.GetValue() < GameData.Instance.CurrentLevel)
            {
                GameData.Instance.Values.MaxLevel.SetValue(GameData.Instance.CurrentLevel);
            }

            GameData.Instance.Values.Coins.SetValue(GameData.Instance.Values.Coins.GetValue() + coinsCounter);

            winScreen.gameObject.SetActive(true);
            winScreen.Initialize();
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(MainMenuName);
        }

        public void GoNext()
        {
            SceneManager.LoadScene(GameData.Instance.Levels[GameData.Instance.CurrentLevel].Name);
        }
    }
}
