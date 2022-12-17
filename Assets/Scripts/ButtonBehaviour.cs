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
        if (GameManager.instance.enabled) Debug.Log("existe");
        GameManager.instance.StartGame(2f);
    }

    public void OnButtonToMenuPressed()
    {
        if (GameManager.instance.enabled) Debug.Log("existe");
        GameManager.instance.StartMenu(2f);
    }

    /*public void OnButtonToEndPressed()
    {
        if (GameManager.instance.enabled) Debug.Log("existe");
        GameManager.instance.StartEnding(2f);
    }*/

    public void OnQuitPress()
    {
        Debug.Log("application quit");
        Application.Quit();
    }

}
