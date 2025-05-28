using UnityEngine;

namespace AnimalDisco
{
    public class Doge : CheatProofClass
    {
        [SerializeField] private GameObject npcArray;
        [SerializeField] private Sprite dogeSprite;
        [SerializeField] private string cheatCode = "doge";   // cheat-code to type
        private int index;  // index of the currently readable letter
        private int lenCode; // lenght of cheatCode

        private void Start()
        {
            lenCode = cheatCode.Length;
            index = 0;
        }

        private void Update()
        {
            int cheatProofVal = CheatProof(cheatCode, index, lenCode);
            switch (cheatProofVal)
            {
                case 1:
                    /* Our Cheatcode execution */
                    SpriteChanger();
                    index = 0;
                    break;

                case 0:
                    // correct letter
                    index++;
                    break;

                case -1:
                    // wrong letter
                    index = 0;
                    break;

                default:
                    // frame without letter
                    break;
            }
        }

        private void SpriteChanger()
        {
            foreach (var nps in npcArray.transform.GetComponentsInChildren<SpriteRenderer>())
                nps.sprite = dogeSprite;
        }
    }
}
