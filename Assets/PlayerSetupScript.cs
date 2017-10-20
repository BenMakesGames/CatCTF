using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetupScript : NetworkBehaviour {

    public Behaviour[] componentsToDisable;

    public PlayerSpawner PlayerSpawner;

    private Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach(Behaviour b in componentsToDisable)
            {
                b.enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;

            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        if (gameObject.GetComponent<PlayerController>().Team == Team.Red)
            PlayerSpawner.RedPlayers--;
        else
            PlayerSpawner.BluePlayers--;
    }
}
