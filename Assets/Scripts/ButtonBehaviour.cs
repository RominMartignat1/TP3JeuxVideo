using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent startGame;

    public void OnButtonStartGamePressed()
    {
        startGame.Invoke();
        GameManager.instance.StartGame(2f);
    }

    public void OnButtonToMenuPressed()
    {
        GameManager.instance.StartMenu(2f);
    }

    public void OnQuitPress()
    {
        Debug.Log("application quit");
        Application.Quit();
    }

}
