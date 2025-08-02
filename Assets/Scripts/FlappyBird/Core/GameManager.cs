using FlappyBird.Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject pauseText;
        
        public static GameManager Instance { get; private set; }
        public static int CurrentScore { get; set; }
        public static int BestScore { get; private set; }
        public static bool IsBeaten => CurrentScore > BestScore;
        public bool isPaused;

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

        private void Start()
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
            pauseText.SetActive(false);
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
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            pauseMenuUI.SetActive(isPaused);
            pauseText.SetActive(isPaused);
        }

        #endregion
        
        # region Game logic
        
        public void RestartGame()
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
        
        #region Debug Tools
        
#if UNITY_EDITOR
        private static bool IsPlaying => Application.isPlaying;

        [Button, EnableIf(nameof(IsPlaying))]
        private void SetCurrentScore999()
        {
            CurrentScore = 999;
        }
        
#endif
        
        #endregion
    }
}