using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonRPG
{
    /// <summary>
    /// Class for loading the dungeon scene after a fight.
    /// </summary>
    public class DungeonLoader : MonoBehaviour
    {
        public void LoadDungeonScene() => SceneManager.LoadScene("DungeonRPG");
    }
}
