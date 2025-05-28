using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*** Script for Cheatcode Lockdown ***/
/* by pressing "zxc" - animal with a Mask runs across the screen */
public class Lockdown : CheatProofClass
{
    [SerializeField] GameObject lockdownAnimal;     // our animal with mask
    [SerializeField] string cheatCode = "zxc";   // cheat-code to type
    int index;  // index of the currently readable letter
    int lenCode; // lenght of cheatCode

    Camera camMain;


    void Start()
    {
        lenCode = cheatCode.Length;
        index = 0;
        camMain = Camera.main;
    }


    void Update()
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
