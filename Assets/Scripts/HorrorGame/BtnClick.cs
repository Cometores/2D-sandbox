using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* * * Script for pressing keys on the phone * * */
public class BtnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected static int sixCounter;    // amount of button presses 6 for True Ending
    [SerializeField] GameObject stateScript;  // need to change the state 
    [SerializeField] Image btnImg;
    [SerializeField] Sprite unpressedSprite, pressedSprite;
    [SerializeField] AudioClip compressClip, uncompressClip, soundClip;
    [SerializeField] AudioSource source;

    public void OnPointerDown(PointerEventData eventData)
    {
        // click on the button
        if (gameObject.name == "Btn6") sixCounter++;
        else sixCounter = 0;

        source.PlayOneShot(soundClip);
        btnImg.sprite = pressedSprite;
        source.PlayOneShot(compressClip);

        if (sixCounter == 3) StartCoroutine(TrueEnding());
            
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // release the button
        btnImg.sprite = unpressedSprite;
        source.PlayOneShot(uncompressClip);
    }

    IEnumerator TrueEnding()
    {
        yield return new WaitForSeconds(0.7f);
        stateScript.GetComponent<StateScript>().OnChange(StateData.RoomState.Room8_TrueEnding);
    }
}


