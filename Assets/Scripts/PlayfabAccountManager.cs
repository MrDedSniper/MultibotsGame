using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _accountInfoLabel;
    [SerializeField] private TMP_Text _usernameLabel;
    [SerializeField] private TMP_Text _creationDateLabel;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
    }
    
    private void OnGetAccount(GetAccountInfoResult result)
    {
        _accountInfoLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId}";
        _usernameLabel.text = $"Username: {result.AccountInfo.Username}";
        _creationDateLabel.text = $"Created: {result.AccountInfo.Created}";
        
        
    }
    
    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }
}
