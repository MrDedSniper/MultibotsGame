using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviourPunCallbacks
{
    public UnityEvent OnSuccessEvent;
    public UnityEvent OnDisconectEvent;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }    

    public void OnTryToLogin()
    {
        Connect();
    }

    private void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            return;
        }

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = Application.version;
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        OnSuccessEvent.Invoke();
        PhotonNetwork.CreateRoom("New Room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("OnDisconnected");
        OnDisconectEvent.Invoke();
    }
}