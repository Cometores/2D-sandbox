using FlappyBird.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Core
{
    /// <summary>
    /// Manages core game functionality, including:
    /// <list type="bullet">
    ///   <item><description>Pausing and resuming the game</description></item>
    ///   <item><description>Restarting the current level</description></item>
    ///   <item><description>Exiting the game</description></item>
    ///   <item><description>Tracking and updating the score</description></item>
    /// </list>
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
        
        # region Level logic
        
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
        
        # endregion
    }
}