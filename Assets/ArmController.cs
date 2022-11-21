using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector3 direction = mousePos - player.transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        


        //Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        

        //Vector3 mousePos = Input.mousePosition;
        //this.GetComponent<Rigidbody2D>().centerOfMass = player.transform.position;
        //float angle = Mathf.Atan2(mousePos.y - player.transform.position.y, mousePos.x - player.transform.position.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.RotateAround(player.transform.position, mousePos, 0);
        
        //MAKE THE ARM FACE THE MOUSE while spinning around the player
        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        //transform.up = direction;


        //make the arm spin around the player while facing the mouse
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
        float angle = Mathf.Atan2(mousePos.y - player.transform.position.y, mousePos.x - player.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.RotateAround(player.transform.position,  new Vector3(0, 0, 1), angle);



    }
}
