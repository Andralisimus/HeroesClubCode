using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public GameObject otherPlayerPrefab;
    private List<GameObject> otherPlayers = new List<GameObject>();
    private float delay = 1f;

    void Start()
    {
        StartCoroutine(loadPlayers());
    }

    private IEnumerator loadPlayers()
    {
        yield return new WaitForSeconds(delay);
        FirebaseAPIs.GetOtherPlayers(dictonary =>
        {

            List<string> keys = dictonary.Keys.ToList();

            foreach (string key in keys)
            {
                if (key != LoginPage.userId)
                {
                    GameObject existingPlayer = otherPlayers.FirstOrDefault(o => o.GetComponent<OtherPlayer>().id == key);
                    if (existingPlayer == null)
                    {
                        GameObject otherPlayer = Instantiate(otherPlayerPrefab, transform);
                        OtherPlayer script = otherPlayer.GetComponent<OtherPlayer>();

                        script.setID(key);
                        otherPlayers.Add(otherPlayer);
                        script.updatePosition(dictonary[key]);
                    }
                    else
                    {
                        existingPlayer.GetComponent<OtherPlayer>().updatePosition(dictonary[key]);
                    }
            }
        }
        });
        StartCoroutine(loadPlayers());
    }
}
