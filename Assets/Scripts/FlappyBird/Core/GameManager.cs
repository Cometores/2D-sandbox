using FlappyBird.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Core
{
    /// <summary>
    /// The GameManager class handles the core game management functionalities such as pausing the game,
    /// restarting the level, and exiting the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public static int CurrentScore { get; set; }
        public static int BestScore { get; private set; }
        public static bool IsBeaten => CurrentScore > BestScore;

        [SerializeField] private GameObject pauseMenuUI;
        private bool _isPaused;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            BestScore = PlayerPrefs.GetInt("bestScore", 0);
        }

        #region Pause input logic

        private void OnEnable()
        {
            if (InputHandler.Instance != null)
                InputHandler.Instance.OnPause += TogglePause;
        }

        private void OnDisable()
        {
            if (InputHandler.Instance != null)
                InputHandler.Instance.OnPause -= TogglePause;
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
            pauseMenuUI.SetActive(_isPaused);
        }

        #endregion

        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitGame()
        {
            Application.Quit();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}