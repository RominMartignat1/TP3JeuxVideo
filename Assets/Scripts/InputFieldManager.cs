using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    

    public enum PLAYER { PLAYER1, PLAYER2 };
    private PLAYER playerNumber;
    private bool playerIsSet = false;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text playerNameText;
    [SerializeField] private Text placeHolderText;

   
    private void Start()
    {
        
    }

    void Update()
    {
        if (!playerIsSet)
        {
            SetPlayer();
        }
        
    }

    private void SetPlayer()
    {
        if (inputField.gameObject.name == "InputFieldPlayerOne")
        {
            playerNumber = PLAYER.PLAYER1;
        }
        else 
        {
            playerNumber = PLAYER.PLAYER2;
        }
        Debug.Log(playerNumber);
        playerIsSet = true;
    }

    public void SaveText()
    {
        GameManager.instance.savePlayerName(playerNumber, playerNameText.text);
    }
}

