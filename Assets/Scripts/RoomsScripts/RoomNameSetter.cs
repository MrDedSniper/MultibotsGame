using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomNameSetter : MonoBehaviour
{
    [SerializeField] internal TMP_Text _roomName;
    public void SetRoomName(string name)
    {
        _roomName.text = name;
    }

    public void OnForestButtonClicked()
    {
        SetRoomName("Forest");
    }

    public void OnDesertButtonClicked()
    {
        SetRoomName("Desert");
    }

    public void OnSwampButtonClicked()
    {
        SetRoomName("Swamp");
    }
}