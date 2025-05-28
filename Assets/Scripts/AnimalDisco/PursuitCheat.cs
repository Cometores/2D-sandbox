using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Cheat code that makes all npcs move towards the character. */
public class PursuitCheat : CheatProofClass
{
    [SerializeField] GameObject npcArray;
    [SerializeField] GameObject player;
    [SerializeField] string cheatCode = "pursuit";   // cheat-code to type
    int index;  // index of the currently readable letter
    int lenCode; // lenght of cheatCode

    void Start()
    {
        lenCode = cheatCode.Length;
        index = 0;
    }


    void Update()
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

    void PursuitEnable()
    {
        foreach (Transform nps in npcArray.transform)
        {
            nps.GetComponent<PursuitMovement>().player = player;
            nps.GetComponent<PursuitMovement>().enabled = true;
        }
    }
}
