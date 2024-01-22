using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
   [SerializeField] private ServerSettings _serverSettings;
   [SerializeField] private TMP_Text _stateUiText;

   //Кнопки UI
   [SerializeField] private Button _startButton;
   [SerializeField] private Button _exitButton;
   [SerializeField] private Button _roomForFriendsButton;
   [SerializeField] private Button _launchButton;

   [SerializeField] private TMP_InputField _privateRoomName;

   //Класс, получающий название выбранной карты
   [SerializeField] private RoomNameSetter _roomNameSetter;

   //Класс, отвечающий за работу предупреждений в UI
   [SerializeField] private WarningsScripts _warningsScripts;

   private LoadBalancingClient _loadBalancingClient;

   private const string GAME_MODE_KEY = "gm";
   private const string AI_MODE_KEY = "ai";

   private const string MAP_PROP_KEY = "C0";
   private const string GOLD_PROP_KEY = "C1";

   private TypedLobby _sqlLobby = new TypedLobby("customSqlLobby", LobbyType.SqlLobby);

   private string roomNameText;

   private EnterRoomParams _selectedParams;
   
   private bool isRoomOpen = true;

   private void Start()
   {
      _loadBalancingClient = new LoadBalancingClient();
      _loadBalancingClient.AddCallbackTarget(this);

      _loadBalancingClient.ConnectUsingSettings(_serverSettings.AppSettings);
      _privateRoomName.onEndEdit.AddListener(OnEndEditPrivateRoomName);
   }

   private void OnDestroy()
   {
      _loadBalancingClient.RemoveCallbackTarget(this);
   }

   private void Update()
   {
      if (_loadBalancingClient == null)
      {
         return;
      }

      _loadBalancingClient.Service();

      var state = _loadBalancingClient.State.ToString();
      _stateUiText.text = state;
   }

   public void OnConnected()
   {

   }

   public void OnConnectedToMaster()
   {
      Debug.Log("OnConnectedToMaster");
      _startButton.gameObject.SetActive(true);
      _roomForFriendsButton.gameObject.SetActive(true);
   }

   
   //Раздичные комнаты для примера
   public void ForestRoomCreate()
   {
      roomNameText = _roomNameSetter._roomName.text;

      var forestRomOptions = new RoomOptions
      {
         MaxPlayers = 12,
         PublishUserId = true,
         CustomRoomPropertiesForLobby = new[] {MAP_PROP_KEY, GOLD_PROP_KEY},
         CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {{GOLD_PROP_KEY, 400}, {MAP_PROP_KEY, "ForestMap"}}
      };

      var ForestRoom = new EnterRoomParams
      {
         RoomName = roomNameText,
         RoomOptions = forestRomOptions,
         ExpectedUsers = new[] {"123456"},
         Lobby = _sqlLobby
      };

      SaveSelectedRoomParams(ForestRoom);
   }

   public void DesertRoomCreate()
   {
      roomNameText = _roomNameSetter._roomName.text;

      var desertRomOptions = new RoomOptions
      {
         MaxPlayers = 12,
         PublishUserId = true,
         CustomRoomPropertiesForLobby = new[] {MAP_PROP_KEY, GOLD_PROP_KEY},
         CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {{GOLD_PROP_KEY, 400}, {MAP_PROP_KEY, "DesertMap"}}
      };

      var DesertRoom = new EnterRoomParams
      {
         RoomName = roomNameText,
         RoomOptions = desertRomOptions,
         ExpectedUsers = new[] {"123456"},
         Lobby = _sqlLobby
      };

      SaveSelectedRoomParams(DesertRoom);
   }

   public void SwampRoomCreate()
   {
      roomNameText = _roomNameSetter._roomName.text;

      var swampRomOptions = new RoomOptions
      {
         MaxPlayers = 12,
         PublishUserId = true,
         CustomRoomPropertiesForLobby = new[] {MAP_PROP_KEY, GOLD_PROP_KEY},
         CustomRoomProperties = new ExitGames.Client.Photon.Hashtable {{GOLD_PROP_KEY, 400}, {MAP_PROP_KEY, "SwampMap"}}
      };

      var SwampRoom = new EnterRoomParams
      {
         RoomName = roomNameText,
         RoomOptions = swampRomOptions,
         ExpectedUsers = new[] {"123456"},
         Lobby = _sqlLobby
      };

      SaveSelectedRoomParams(SwampRoom);
   }
   
   //Сохраняем параметры выбранной комнаты
   public void SaveSelectedRoomParams(EnterRoomParams roomParams)
   {
      _selectedParams = roomParams;
   }

   //Создание комнаты
   public void CreateRoomButtonClicked()
   {
      if (_selectedParams == null)
      {
         Debug.LogError("Вы не выбрали карту!");
         _warningsScripts.MapNotSelected();
         return;
      }
      
      _loadBalancingClient.OpCreateRoom(_selectedParams);
      
   }
   
   //Выход из комнаты
   public void ExitRoomButtonClicked()
   {
      if (_loadBalancingClient.CurrentRoom != null)
      {
         _loadBalancingClient.OpLeaveRoom(false, true);
         Debug.Log("Вы вышли из комнаты");
      }
      else
      {
         Debug.LogWarning("Вы не подключены к комнате!");
      }
   }

   public void LockTheRoom()
   {
      if (!_loadBalancingClient.InRoom)
      {
         return;
      }

      isRoomOpen = false;

      if (_loadBalancingClient.PlayersInRoomsCount > 0)
      {
         StartMatch();
      }
   }

   /*public void PrivateRoomNaming(TMP_InputField text)
   {
      _privateRoomName.gameObject.SetActive(true);
      
      _privateRoomName = text;
      
      _privateRoomName.onEndEdit.Invoke("CreatePrivateRoom");
      
         
         //.Invoke("CreatePrivateRoom");
   }*/

   private void OnEndEditPrivateRoomName(string text)
   {
      if (!string.IsNullOrEmpty(text))
      {
         CreatePrivateRoom(text);
      }
   }

   public void PrivateRoomNaming()
   {
      _privateRoomName.gameObject.SetActive(true);
   }

   private void CreatePrivateRoom(string roomName)
   {
      _privateRoomName.gameObject.SetActive(false);

      isRoomOpen = false;

      if (_selectedParams == null)
      {
         Debug.LogError("Вы не выбрали карту!");
         _warningsScripts.MapNotSelected();
         return;
      }

      _loadBalancingClient.OpCreateRoom(_selectedParams);

      Debug.Log("<color=green>> Приватная комната создана! Имя комнаты: " + roomName + "</color>");
   }

   public void CopyNameOfPrivateRoom()
   {
      GUIUtility.systemCopyBuffer = _privateRoomName.text;
   }

   private void StartMatch()
   {
      Debug.Log("<color=green>> УСЛОВНЫЙ ЗАПУСК МАТЧА <<</color>");
   }

   public void OnDisconnected(DisconnectCause cause)
   {
      
   }

   public void OnRegionListReceived(RegionHandler regionHandler)
   {
      
   }

   public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
   {
      
   }

   public void OnCustomAuthenticationFailed(string debugMessage)
   {
     
   }

   public void OnFriendListUpdate(List<FriendInfo> friendList)
   {
      
   }

   public void OnCreatedRoom()
   {
      Debug.Log("OnCreatedRoom");
   }

   public void OnCreateRoomFailed(short returnCode, string message)
   {
      
   }

   public void OnJoinedRoom()
   {
      _exitButton.gameObject.SetActive(true);
      _startButton.gameObject.SetActive(false);
      _roomForFriendsButton.gameObject.SetActive(false);
      _launchButton.gameObject.SetActive(true);
      
      Debug.Log("OnJoinedRoom");
      Debug.Log($"Вы создали комнату на карте: {roomNameText}");
   }

   public void OnJoinRoomFailed(short returnCode, string message)
   {
      
   }

   public void OnJoinRandomFailed(short returnCode, string message)
   {
      Debug.Log("OnJoinRandomFailed");
      _loadBalancingClient.OpCreateRoom(new EnterRoomParams());
   }

   public void OnLeftRoom()
   {
      _startButton.gameObject.SetActive(true);
      _roomForFriendsButton.gameObject.SetActive(true);
      _exitButton.gameObject.SetActive(false);
      _launchButton.gameObject.SetActive(false);
   }

   public void OnJoinedLobby()
   {
      /*var sqlLobbyFilter = $"{MAP_PROP_KEY} = Map3 AND {GOLD_PROP_KEY} BETWEEN 300 AND 500";

      var opJoinRandomRoomParams = new OpJoinRandomRoomParams();
      _loadBalancingClient.OpJoinRandomRoom(opJoinRandomRoomParams);*/
   }

   public void OnLeftLobby()
   {
      
   }

   public void OnRoomListUpdate(List<RoomInfo> roomList)
   {
      
   }

   public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
   {
      
   }
}
