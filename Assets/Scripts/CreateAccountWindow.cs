using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField] private InputField _mailField;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private TMP_Text _loadingLabel;

    private string _mail;
    
    protected override void SubscriptionElementsUi()
    {
        _loadingLabel.enabled = false;
        base.SubscriptionElementsUi();
        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        _loadingLabel.enabled = true;
        
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _password
        }, 
            result => { Debug.Log($"Success: {_username}");
                EnterInGameScene();
            }, 
            error => { Debug.Log($"Fail: {error.ErrorMessage}"); });
    }

    private void UpdateMail(string mail)
    {
        _mail = mail;
    }
}
