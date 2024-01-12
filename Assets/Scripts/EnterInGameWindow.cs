using UnityEngine;
using UnityEngine.UI;

public class EnterInGameWindow : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;
    private void Start()
    {
        _signInButton.onClick.AddListener(OpenSighInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountInWindow);
    }
    private void OpenSighInWindow()
    {
        _signInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
    private void OpenCreateAccountInWindow()
    {
        _createAccountCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
}