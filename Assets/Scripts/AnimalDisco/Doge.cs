using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doge : CheatProofClass
{
    [SerializeField] GameObject npcArray;
    [SerializeField] Sprite dogeSprite;
    [SerializeField] string cheatCode = "doge";   // cheat-code to type
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

    void SpriteChanger()
    {
        foreach (var nps in npcArray.transform.GetComponentsInChildren<SpriteRenderer>())
            nps.sprite = dogeSprite;
    }
}
