using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    private bool textisSet = false;

    void Start()
    {
        winnerText.text = GameManager.instance.GetWinner() + " a gagné";
    }

    void Update()
    {
        if (!textisSet)
        {
            textisSet = true;
        }
    }
}
