﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject Playerprefab;

    private void Start()
    {
        Vector3 pos = new Vector3(-15.80474f, 3.369533f, 23.96897f);
        PhotonNetwork.Instantiate(Playerprefab.name, pos, Quaternion.identity);
    }

    private void Update()
    {
        
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player (0) entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player (0) left room", otherPlayer.NickName);
    }
}
