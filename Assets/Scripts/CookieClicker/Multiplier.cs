using UnityEngine;

namespace CookieClicker
{
    [CreateAssetMenu(menuName = "Configs/CookieClicker/Multiplier")]
    public class Multiplier : ScriptableObject
    {
        [TextArea(2, 15)]
        public string description;
        [Header("Base Cost")]
        public int baseCost;
        [Header("Multiplier")]
        public float multiplier;
        [Header("Max Amount")]
        public int maxAmount;
    }
}