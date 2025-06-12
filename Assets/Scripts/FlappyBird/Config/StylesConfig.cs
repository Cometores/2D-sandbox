using UnityEngine;

namespace FlappyBird.Config
{
    [CreateAssetMenu(fileName = "StylesConfig", menuName = "Configs/FlappyBird/Styles")]
    public class StylesConfig : ScriptableObject
    {
        [Header("Buttons")]
        public Color normalColor = Color.white;
        public Color hoveredColor = Color.gray;
    }
}
