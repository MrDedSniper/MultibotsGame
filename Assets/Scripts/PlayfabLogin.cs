using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using UnityEngine;
using System;

public class PlayfabLogin : MonoBehaviour
{
    public UnityEvent OnSuccessEvent;
    public UnityEvent OnErrorEvent;

    private bool isLogged = false;

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
        if (!isLogged)
        {
            var request = new LoginWithCustomIDRequest()
            {
                CustomId = "Player 1",
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
            isLogged = true;
        }
        else
        {
            return;
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