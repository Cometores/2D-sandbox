using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace FlappyBird
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuUI;
        private bool _isPaused;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                TogglePause();
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0f : 1f;
            menuUI.SetActive(_isPaused);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void Exit()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
