using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using teams;

public class ArmController : MonoBehaviour
{
    private PlayerController playerController;
    void Start()
    {
        playerController = transform.parent.transform.parent.GetComponent<PlayerController>();
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
        Debug.Log(Input.GetJoystickNames().Length);
        if((Input.GetJoystickNames().Length == 1 && playerController.GetTeam() == Teams.Red) || Input.GetJoystickNames().Length >= 2) {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(horizontal, vertical) * -180 / Mathf.PI + 90);
        } else {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.LookAt(mousePos,new Vector3(0, 0, 1));
        }
    }
}
