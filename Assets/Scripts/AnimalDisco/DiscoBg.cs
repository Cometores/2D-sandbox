using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*** Script for Cheatcode DiscoBg ***/
/* by pressing "light" - background color changes every 0.3s */
public class DiscoBg : CheatProofClass
{
    [SerializeField] GameObject background;

    [SerializeField] string cheatCode = "light";   // cheat-code to type
    int index;  // index of the currently readable letter
    int lenCode; // lenght of cheatCode

    bool isActive;


    void Start()
    {
        lenCode = cheatCode.Length;
        index = 0;
    }

    void ChangeLight() => background.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.75f, 1f, 1f, 1f);

    void Update()
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
