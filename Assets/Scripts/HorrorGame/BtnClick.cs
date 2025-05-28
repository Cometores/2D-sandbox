using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* * * Script for pressing keys on the phone * * */
namespace HorrorGame
{
    public class BtnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject stateScript;  // need to change the state 
        [SerializeField] private Image btnImg;
        [SerializeField] private Sprite unpressedSprite, pressedSprite;
        [SerializeField] private AudioClip compressClip, uncompressClip, soundClip;
        [SerializeField] private AudioSource source;
        
        private static int _sixCounter;    // amount of button presses 6 for True Ending

        public void OnPointerDown(PointerEventData eventData)
        {
            // click on the button
            if (gameObject.name == "Btn6") _sixCounter++;
            else _sixCounter = 0;

            source.PlayOneShot(soundClip);
            btnImg.sprite = pressedSprite;
            source.PlayOneShot(compressClip);

            if (_sixCounter == 3) StartCoroutine(TrueEnding());
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // release the button
            btnImg.sprite = unpressedSprite;
            source.PlayOneShot(uncompressClip);
        }

        private IEnumerator TrueEnding()
        {
            yield return new WaitForSeconds(0.7f);
            _sixCounter = 0;
            stateScript.GetComponent<StateScript>().OnChange(StateData.RoomState.Room8_TrueEnding);
        }
    }
}


