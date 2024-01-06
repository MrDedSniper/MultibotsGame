using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayfabUILogIn : MonoBehaviour
{
    [SerializeField] private bool isErrorHappening = false;
    
    public UnityEvent ButtonClickedEvent;

    [SerializeField] private Button button;
    [SerializeField] private Button errorButton;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image dot;

    private void Start()
    {      
        button.onClick.AddListener(ButtonClicked);
    }    

    private void ButtonClicked()
    {
        ButtonClickedEvent.Invoke();
    }    

    public void OnLogin()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Done!";
        label.text = "Welcome!";
        label.color = Color.green;
        dot.color = Color.green;
    }

    public void OnError() 
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Error!";
        label.color = Color.red;
        dot.color = Color.red;
    }

    public void OnBaseState()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Offline";
        label.color = Color.red;
        dot.color = Color.red;
    }
}
