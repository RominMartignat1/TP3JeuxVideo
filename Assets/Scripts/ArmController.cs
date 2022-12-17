using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{

    private GameObject player;
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(mousePos,new Vector3(0, 0, 1));
    }
}
