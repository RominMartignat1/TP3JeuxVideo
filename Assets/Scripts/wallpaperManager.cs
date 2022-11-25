using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpaperManager : MonoBehaviour
{
    //get every children in a list
    //private List<GameObject> children = new List<GameObject>();

   // private GameObject[] blocks;
    private float moveSpeed = 5f;
    private GameObject PlatformGeneratorManager;


    // Start is called before the first frame update
    void Start()
    {
       // blocks = GetEveryBlock(this.gameObject);
        PlatformGeneratorManager = GameObject.FindGameObjectWithTag("PlatformSpawner");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("J'ai exit un trigger");
        if (collision.gameObject.tag == "Despawner")
        {
            Debug.Log("Despawner");
            transform.position = new Vector3(transform.position.x, PlatformGeneratorManager.transform.position.y -10f, transform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);
        //if (transform.position.y < -10)
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

}
/*using UnityEngine;
using System.Collections;

public class EndlessBG : MonoBehaviour {

    private Vector3 m_BackPos;
    private float m_sMoveHeight;

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnBecameInvisible()
    {
        m_sMoveHeight = gameObject.GetComponent<Renderer>().bounds.size.y - 1f;
        // get current position
        m_BackPos = gameObject.transform.position;

        // move to new position when invisible
        gameObject.transform.position = new Vector3(m_BackPos.x, m_BackPos.y + 3 * m_sMoveHeight);
    }
}
*/