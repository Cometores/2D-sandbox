using UnityEngine;


/*** Script for Cheatcode DiscoBg ***/
/* by pressing "light" - background color changes every 0.3s */
namespace AnimalDisco
{
    public class DiscoBg : CheatProofClass
    {
        [SerializeField] private GameObject background;

        [SerializeField] private string cheatCode = "light";   // cheat-code to type
        private int index;  // index of the currently readable letter
        private int lenCode; // lenght of cheatCode

        private bool isActive;


        private void Start()
        {
            lenCode = cheatCode.Length;
            index = 0;
        }

        private void ChangeLight() => background.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.75f, 1f, 1f, 1f);

        private void Update()
        {
            int cheatProofVal = CheatProof(cheatCode, index, lenCode);
            switch (cheatProofVal)
            {
                case 1:
                    // cheatcode in action
                    isActive = !isActive;
                    if (isActive) InvokeRepeating("ChangeLight", 0.3f, 0.3f);
                    else CancelInvoke();
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
    }
}
