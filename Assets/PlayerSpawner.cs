using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawner : NetworkManager {

    public int RedPlayers = 0;
    public int BluePlayers = 0;

    public GameObject[] BlueTeamSpawnPoints;
    public GameObject[] RedTeamSpawnPoints;

    private System.Random _rng = new System.Random();

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Team newTeam;

        if (RedPlayers > BluePlayers)
            newTeam = Team.Blue;
        else if (RedPlayers < BluePlayers)
            newTeam = Team.Red;
        else
            newTeam = _rng.Next(2) == 0 ? Team.Blue : Team.Red;

        GameObject spawnPoint;

        if (newTeam == Team.Blue)
        {
            spawnPoint = BlueTeamSpawnPoints[_rng.Next(BlueTeamSpawnPoints.Length)];
            BluePlayers++;
        }
        else
        {
            spawnPoint = RedTeamSpawnPoints[_rng.Next(RedTeamSpawnPoints.Length)];
            RedPlayers++;
        }

        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, null);

        newPlayer.GetComponent<PlayerController>().Team = newTeam;
        newPlayer.GetComponent<PlayerSetupScript>().PlayerSpawner = this;

        NetworkServer.AddPlayerForConnection(conn, newPlayer, playerControllerId);
    }
}
