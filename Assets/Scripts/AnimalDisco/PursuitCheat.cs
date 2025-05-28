using UnityEngine;

/* Cheat code that makes all npcs move towards the character. */
namespace AnimalDisco
{
    public class PursuitCheat : CheatProofClass
    {
        [SerializeField] private GameObject npcArray;
        [SerializeField] private GameObject player;
        [SerializeField] private string cheatCode = "pursuit";   // cheat-code to type
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
                    PursuitEnable();
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

        private void PursuitEnable()
        {
            foreach (Transform nps in npcArray.transform)
            {
                nps.GetComponent<PursuitMovement>().player = player;
                nps.GetComponent<PursuitMovement>().enabled = true;
            }
        }
    }
}
