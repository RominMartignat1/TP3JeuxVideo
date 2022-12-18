using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    private bool textisSet = false;

    void Start()
    {
        winnerText.text = GameManager.instance.GetWinner() + " a gagn�";
    }

    void Update()
    {
        if (!textisSet)
        {
            Debug.Log(GameManager.instance.GetWinner());

            textisSet = true;
        }
    }
}
