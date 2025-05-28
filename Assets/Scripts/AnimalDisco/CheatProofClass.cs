using UnityEngine;

/* * * A class for checking any cheat code * * */
namespace AnimalDisco
{
    public class CheatProofClass : MonoBehaviour
    {
        protected static int CheatProof(string cheatCode, int index, int lenCode)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown((KeyCode)cheatCode[index]))
                {
                    if (index + 1 == lenCode) return 1; // cheat code entered successfully
                    return 0; // correct cheat code letter
                }
                else return -1; // wrong letter
            }
            return -2;
        }
    }
}
