using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace HorrorGame
{
    public class StateScript : StateData
    {
        [SerializeField] private TextMeshProUGUI storyText;
        [SerializeField] private TextMeshProUGUI choisesText;
        [SerializeField] private Image backgroundImg;
        [SerializeField] private InputActionAsset inputActions;

        public RoomState actualState;

        private HG_InputActions _playerControls;
        private InputAction _choice1, _choice2, _choice3, _choice4;

        private Dictionary<RoomState, Action<int>> _stateLogic;

        private void Awake()
        {
            _playerControls = new HG_InputActions();
        }

        private void OnEnable()
        {
            _choice1 = _playerControls.Keyboard.Choice1;
            _choice1.performed += ctx => OnChoice(1);
            _choice1.Enable();
        
            _choice2 = _playerControls.Keyboard.Choice2;
            _choice2.performed += ctx => OnChoice(2);
            _choice2.Enable();
        
            _choice3 = _playerControls.Keyboard.Choice3;
            _choice3.performed += ctx => OnChoice(3);
            _choice3.Enable();
        
            _choice4 = _playerControls.Keyboard.Choice4;
            _choice4.performed += ctx => OnChoice(4);
            _choice4.Enable();
        }

        private void OnDisable()
        {
            _choice1.performed -= ctx => OnChoice(1);
            _choice2.performed -= ctx => OnChoice(2);
            _choice3.performed -= ctx => OnChoice(3);
            _choice4.performed -= ctx => OnChoice(4);
        }

        private void Start()
        {
            actualState = RoomState.Room1_GameStart;
            InitStateLogic();
        }

        private void OnChoice(int number)
        {
            if (_stateLogic.TryGetValue(actualState, out var action))
                action?.Invoke(number);
        }

        private void InitStateLogic()
        {
            _stateLogic = new Dictionary<RoomState, Action<int>>
            {
                [RoomState.Room1_GameStart] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room2_Kitchen);
                    else if (choice == 2) OnChange(RoomState.Room4_WithoutLight);
                },
                [RoomState.Room2_Kitchen] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room3_KitchenEnd);
                    else if (choice == 2) OnChange(RoomState.Room4_WithoutLight);
                },
                [RoomState.Room3_KitchenEnd] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room4_WithoutLight] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room5_WithoutLightEnd);
                    else if (choice == 2) OnChange(RoomState.Room6_TheCall);
                },
                [RoomState.Room5_WithoutLightEnd] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room6_TheCall] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room7_TheCallEnd);
                    else if (choice == 2) OnChange(RoomState.Room9_TVNoise);
                },
                [RoomState.Room7_TheCallEnd] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room8_TrueEnding] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room9_TVNoise] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room10_TVNoiseEnd1);
                    else if (choice == 2) OnChange(RoomState.Room11_TVNoiseEnd2);
                    else if (choice == 3) OnChange(RoomState.Room13_FinishMovie);
                    else if (choice == 4) OnChange(RoomState.Room12_TVNoiseNothingHappend);
                },
                [RoomState.Room10_TVNoiseEnd1] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room11_TVNoiseEnd2] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room12_TVNoiseNothingHappend] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room9_TVNoise);
                },
                [RoomState.Room13_FinishMovie] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room14_FinishMovieEnd);
                    else if (choice == 2) OnChange(RoomState.Room15_GoodEnding);
                },
                [RoomState.Room14_FinishMovieEnd] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                },
                [RoomState.Room15_GoodEnding] = choice =>
                {
                    if (choice == 1) OnChange(RoomState.Room1_GameStart);
                }
            };
        }

        public void OnChange(RoomState newState)
        {
            stateData[actualState].Item4.SetActive(false);
            stateData[newState].Item4.SetActive(true);

            storyText.text = stateData[newState].Item1;
            choisesText.text = stateData[newState].Item2;
            backgroundImg.sprite = stateData[newState].Item3;

            actualState = newState;
        }
    }
}
