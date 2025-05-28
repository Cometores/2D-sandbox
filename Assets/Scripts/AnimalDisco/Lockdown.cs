using UnityEngine;


/*** Script for Cheatcode Lockdown ***/
/* by pressing "zxc" - animal with a Mask runs across the screen */
namespace AnimalDisco
{
    public class Lockdown : CheatProofClass
    {
        [SerializeField] private GameObject lockdownAnimal;     // our animal with mask
        [SerializeField] private string cheatCode = "zxc";   // cheat-code to type
        private int index;  // index of the currently readable letter
        private int lenCode; // lenght of cheatCode

        private Camera camMain;


        private void Start()
        {
            lenCode = cheatCode.Length;
            index = 0;
            camMain = Camera.main;
        }


        private void Update()
        {
            int cheatProofVal = CheatProof(cheatCode, index, lenCode);
            switch (cheatProofVal)
            {
                case 1:
                    /* Our Cheatcode execution */
                    Vector3 spawnVector = new Vector3(0, 0.5f, 0); // left side of screen (x, y)
                    var spawnPos = camMain.ViewportToWorldPoint(spawnVector);
                    spawnPos.z = 0;

                    Instantiate(lockdownAnimal, spawnPos, Quaternion.identity);

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
