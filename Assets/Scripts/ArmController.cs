using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using teams;

public class ArmController : MonoBehaviour
{

    private PlayerController playerController;
    void Start()
    {
        playerController = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        float horizontal = 0;
        float vertical = 0;
        if(playerController.GetTeam() == Teams.Blue) {
            horizontal = Input.GetAxis("HorizontalRight");
            vertical = Input.GetAxis("VerticalRight");
        } else if(playerController.GetTeam() == Teams.Red) {
            horizontal = Input.GetAxis("HorizontalRightP2");
            vertical = Input.GetAxis("VerticalRightP2");
        }
        if(Input.GetJoystickNames().Length > 0) {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(horizontal, vertical) * -180 / Mathf.PI + 90);
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(mousePos,new Vector3(0, 0, 1));
    }
}
