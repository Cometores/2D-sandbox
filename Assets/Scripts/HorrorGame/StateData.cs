using System;
using System.Collections.Generic;
using UnityEngine;

/* * * A script that just contains data about each room and does nothing. * * *
* * *  Contains state, sprites, room objects, and text for each room.     * * *
* * *  NO FUNCTIONALITY * * */
namespace HorrorGame
{
    public class StateData : MonoBehaviour
    {
        /* I know we can' t use numbers and underscores, but in this case, I really don't know how to refer to it more clearly and name it more appropriately. 
     * I think in this case, we could make an exception. */
        public enum RoomState
        {
            Room1_GameStart, Room2_Kitchen, Room3_KitchenEnd, Room4_WithoutLight, Room5_WithoutLightEnd,
            Room6_TheCall, Room7_TheCallEnd, Room8_TrueEnding, Room9_TVNoise, Room10_TVNoiseEnd1, 
            Room11_TVNoiseEnd2, Room12_TVNoiseNothingHappend, Room13_FinishMovie, Room14_FinishMovieEnd, Room15_GoodEnding
        }

        [SerializeField] protected Sprite[] backgrounds;
        [SerializeField] protected GameObject[] roomActions;

        // Save basic data in the form of a dictionary. With the key - { state }, values - { story text, selection text, picture, GameObject with special room sprites etc. }
        protected static Dictionary<RoomState, Tuple<string, string, Sprite, GameObject>> stateData = new Dictionary<RoomState, Tuple<string, string, Sprite, GameObject>>();

        private void Start()
        {
            stateData.Add(RoomState.Room1_GameStart, new Tuple<string, string, Sprite, GameObject>("Friends gave you a tape of an allegedly scary movie last week. " +
                "But you only found time to watch it today. Suddenly the kitchen door creaks open and you remember that you are home alone. ",
                "(1) Go check it out\n(2) Keep watching the movie",
                backgrounds[0],
                roomActions[0]));

            stateData.Add(RoomState.Room2_Kitchen, new Tuple<string, string, Sprite, GameObject>("Turns out, while you were watching the movie, Grandma came home and baked a pie.",
                "(1) Eat <color=#7a1511ff>pie</color>\n(2) Get back in the room",
                backgrounds[1],
                roomActions[1]));

            stateData.Add(RoomState.Room3_KitchenEnd, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>Grandma turned out to be cursed and made a pie out of your cat.</color>",
                "(1) New Game",
                backgrounds[2],
                roomActions[2]));

            stateData.Add(RoomState.Room4_WithoutLight, new Tuple<string, string, Sprite, GameObject>("The lights go out in your room. The TV is still on.",
                "(1) Turn on the light\n(2) Keep watching the movie",
                backgrounds[3],
                roomActions[3]));

            stateData.Add(RoomState.Room5_WithoutLightEnd, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>In front of the lamp sits a scary woman you never expected to see</color>",
                "(1) New Game",
                backgrounds[4],
                roomActions[4]));

            stateData.Add(RoomState.Room6_TheCall, new Tuple<string, string, Sprite, GameObject>("You keep watching the movie to distract yourself from the strange things going on in the apartment. " +
                "Suddenly the phone rings...",
                "(1) Answer the phone\n(2) Keep watching the movie",
                backgrounds[5],
                roomActions[5]));

            stateData.Add(RoomState.Room7_TheCallEnd, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>A squeaky voice on the phone tells you - you will die in seven days.</color>",
                "(1) New Game",
                backgrounds[6],
                roomActions[6]));

            stateData.Add(RoomState.Room8_TrueEnding, new Tuple<string, string, Sprite, GameObject>("<color=#0f7d90ff>You order a demonically spicy pizza and finish the movie with pleasure.</color>",
                "(1) New Game",
                backgrounds[7],
                roomActions[7]));

            stateData.Add(RoomState.Room9_TVNoise, new Tuple<string, string, Sprite, GameObject>("The picture freezes and noise appears on the TV screen.",
                "(1) Hide under a blanket\n(2) Sit and wait\n(3) Lightly hit the TV\n(4) Turn the TV off and on",
                backgrounds[8],
                roomActions[8]));

            stateData.Add(RoomState.Room10_TVNoiseEnd1, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>Under the blanket you see an unwanted guest.</color>",
                "(1) New Game",
                backgrounds[9],
                roomActions[9]));

            stateData.Add(RoomState.Room11_TVNoiseEnd2, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>A scary girl slowly crawls out of the screen.</color>",
                "(1) New Game",
                backgrounds[10],
                roomActions[10]));

            stateData.Add(RoomState.Room12_TVNoiseNothingHappend, new Tuple<string, string, Sprite, GameObject>("It didn't help. The TV is still <color=#7a1511ff>noisy</color>.",
                "(1) Back to Selection",
                backgrounds[11],
                roomActions[11]));

            stateData.Add(RoomState.Room13_FinishMovie, new Tuple<string, string, Sprite, GameObject>("The noises stops and you finish the movie. You're so tired and scared that you just want to go to bed.",
                "(1) Go Brush Your Teeth\n(2) Forget the teeth and go straight to bed",
                backgrounds[12],
                roomActions[12]));

            stateData.Add(RoomState.Room14_FinishMovieEnd, new Tuple<string, string, Sprite, GameObject>("<color=#7a1511ff>In the bathroom, you see Grandma on the ceiling.</color>",
                "(1) New Game",
                backgrounds[13],
                roomActions[13]));

            stateData.Add(RoomState.Room15_GoodEnding, new Tuple<string, string, Sprite, GameObject>("<color=#0f7d90ff>You win.The morning came and you went to school as if nothing had happened, " +
                "where your classmates appreciated your courage and the prettiest girl in class confessed her love for you.</color>",
                "(1) New Game",
                backgrounds[14],
                roomActions[14]));
        }
    }
}
