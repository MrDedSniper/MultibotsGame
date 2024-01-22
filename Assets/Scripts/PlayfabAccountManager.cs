using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using CatalogItem = PlayFab.ClientModels.CatalogItem;
using GetCatalogItemsRequest = PlayFab.ClientModels.GetCatalogItemsRequest;
using GetCatalogItemsResult = PlayFab.ClientModels.GetCatalogItemsResult;

public class PlayfabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _accountInfoLabel;
    [SerializeField] private TMP_Text _usernameLabel;
    [SerializeField] private TMP_Text _creationDateLabel;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);
    }
    
    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        Debug.Log("OnGetCatalogSuccess");
        ShowItems(result.Catalog);
    }

    private void ShowItems(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            Debug.Log($"{item.ItemId}");
        }
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
