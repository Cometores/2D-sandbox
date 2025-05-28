using UnityEngine;

/*** Script, that spawns lights by pressing Key-4 ***/
namespace AnimalDisco
{
    public class LightSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] LightsPrefabs;
        private Camera camMain;

        private void Awake() => camMain = Camera.main;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Vector3 spawnVector = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 1);   // Random Vieport coordinates
                var spawnPos = camMain.ViewportToWorldPoint(spawnVector);   // Converting in WorldPoint
                spawnPos.z = 0;
                int lightNum = Random.Range(0, 3);      // Choosing randomly from prefab invariants

                Instantiate(LightsPrefabs[lightNum], spawnPos, Quaternion.identity);
            }
        }
    }
}
