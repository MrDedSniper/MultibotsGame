using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhotonUILogIn : MonoBehaviour
{
    public UnityEvent ButtonClickedEvent;

    [SerializeField] private Button button;
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
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log Out";
        label.text = "Welcome!";
        label.color = Color.green;
        dot.color = Color.green;
    }  

    public void OnLogout()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Goodbye!";
        label.color = Color.red;
        dot.color = Color.red;
    }
}
