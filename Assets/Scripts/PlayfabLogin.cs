using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;

public class PlayfabLogin : MonoBehaviour
{
    private const string AuthGuidKey = "auth_guid_key";
    
    public UnityEvent OnSuccessEvent;
    public UnityEvent OnErrorEvent;

    private bool isLogged;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "A9CF3";
        }
    }

    // Нажатие на кнопку входа
    public void OnTryToLogin()
    {
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
        
        if (!isLogged)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = id,
                CreateAccount = !needCreation
            };

            PlayFabClientAPI.LoginWithCustomID(request,
                result =>
                {
                    PlayerPrefs.SetString(AuthGuidKey, id);
                    OnLoginSuccess(result);
                }, OnLoginError);
        }
    }

    // Ивенты различных сценариев
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Complete Login");
        OnSuccessEvent.Invoke();
    }

    private void OnLoginError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
        OnErrorEvent.Invoke();
    }
}