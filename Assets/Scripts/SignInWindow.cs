using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class SignInWindow : AccountDataWindowBase
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private TMP_Text _loadingLabel;
    
    protected override void SubscriptionElementsUi()
    {
        _loadingLabel.enabled = false; 
        base.SubscriptionElementsUi();
        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        _loadingLabel.enabled = true;
            
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, 
        result => { Debug.Log($"Success: {_username}");
            EnterInGameScene();
        }, 
        error => { Debug.Log($"Fail: {error.ErrorMessage}"); });
    }
}
