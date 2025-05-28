using UnityEngine;


/*** Script, that makes Sprite on Background GameObject darker or brighter ***/
namespace AnimalDisco
{
    public class BackgroundChanger : MonoBehaviour
    {
        private SpriteRenderer sr;
        [SerializeField] private float DarkerVal = 0.1f; // factor for darker or brighter

        private void Awake() => sr = GetComponent<SpriteRenderer>();

        private void Update()
        {
            // Color = RGB_Transparency 
            if (Input.GetKeyDown(KeyCode.Alpha7)) sr.color -= new Color(DarkerVal, DarkerVal, DarkerVal, 0);
            if (Input.GetKeyDown(KeyCode.Alpha8)) sr.color += new Color(DarkerVal, DarkerVal, DarkerVal, 0);
        }
    }
}
