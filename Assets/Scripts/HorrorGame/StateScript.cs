using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/* * * Functionality of our state. Uses switch to determine the room and if to determine the key pressed * * */
public class StateScript : StateData
{
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] TextMeshProUGUI choisesText;
    [SerializeField] Image backgroundImg;

    public RoomState actualState;

    public void OnChange(RoomState newState)
    {
        // Turn off the old room and turn on the right one, and change the text and background image
        stateData[actualState].Item4.SetActive(false);
        stateData[newState].Item4.SetActive(true);

        storyText.text = stateData[newState].Item1;
        choisesText.text = stateData[newState].Item2;
        backgroundImg.sprite = stateData[newState].Item3;

        actualState = newState;
    }

    void Start() => actualState = RoomState.Room1_GameStart;


    void Update()
    {
        switch (actualState)    
        {
            case RoomState.Room1_GameStart:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room2_Kitchen);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room4_WithoutLight);
                break;

            case RoomState.Room2_Kitchen:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room3_KitchenEnd);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room4_WithoutLight);
                break;

            case RoomState.Room3_KitchenEnd:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room4_WithoutLight:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room5_WithoutLightEnd);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room6_TheCall);
                break;

            case RoomState.Room5_WithoutLightEnd:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room6_TheCall:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room7_TheCallEnd);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room9_TVNoise);
                break;

            case RoomState.Room7_TheCallEnd:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room8_TrueEnding:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room9_TVNoise:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room10_TVNoiseEnd1);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room11_TVNoiseEnd2);
                else if (Input.GetKeyDown(KeyCode.Alpha3)) OnChange(RoomState.Room13_FinishMovie);
                else if (Input.GetKeyDown(KeyCode.Alpha4)) OnChange(RoomState.Room12_TVNoiseNothingHappend);
                break;

            case RoomState.Room10_TVNoiseEnd1:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room11_TVNoiseEnd2:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room12_TVNoiseNothingHappend:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room9_TVNoise);
                break;

            case RoomState.Room13_FinishMovie:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room14_FinishMovieEnd);
                else if (Input.GetKeyDown(KeyCode.Alpha2)) OnChange(RoomState.Room15_GoodEnding);
                break;

            case RoomState.Room14_FinishMovieEnd:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            case RoomState.Room15_GoodEnding:
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnChange(RoomState.Room1_GameStart);
                break;

            default:
                break;
        }
    }
}
