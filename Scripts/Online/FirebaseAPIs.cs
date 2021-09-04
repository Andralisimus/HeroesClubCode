using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using System.Linq;

public class FirebaseAPIs : MonoBehaviour
{
    private static readonly string databaseURL = "https://heroclub-c8c46-default-rtdb.europe-west1.firebasedatabase.app/";
    private static fsSerializer serializer = new fsSerializer();

    public delegate void EmptyCallback();
    public delegate void GetPlayersCallback(Dictionary<string, PlayerPosition> players);

    public static void PostPlayer(PlayerPosition position, EmptyCallback callback)
    {
        RestClient.Put<PlayerPosition>($"{databaseURL}lobbies/1/{position.userId}.json", position).Then(response => {
            callback();
        });
    }

    public static void GetOtherPlayers(GetPlayersCallback callback)
    {
        RestClient.Get($"{databaseURL}lobbies/1.json").Then(response =>
        {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, PlayerPosition>), ref deserialized);

            var players = deserialized as Dictionary<string, PlayerPosition>;
            callback(players);
        });
    }
}
