using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text winnerText;
    private bool textisSet = false;

    void Start()
    {
        winnerText.text = GameManager.instance.GetWinner() + " a gagné";
    }

    // Update is called once per frame
    void Update()
    {
        if (!textisSet)
        {
            Debug.Log(GameManager.instance.GetWinner());
           
            textisSet = true;
        }
    }
}
