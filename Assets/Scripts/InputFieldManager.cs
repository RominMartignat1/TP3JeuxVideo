using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (Input.GetButtonDown("Cancel")) Application.Quit();

        if (Input.GetButtonDown("Submit"))
        {
            if (playerNameText.text.Length < 3)
            {
                inputField.Select();
                inputField.text = "";
                placeHolderText.text = "Le nom doit avoir au moins 3 caractères";
            }
            else
            {
               // GameManager.instance.SetPlayerName(playerNameText.text);
                GameManager.instance.StartNextlevel(0.2f);
            }
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
}

