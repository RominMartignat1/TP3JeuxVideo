using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent startGame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
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
