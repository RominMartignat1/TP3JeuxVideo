using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    int n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnButtonPress()
    {
        if (GameManager.instance.enabled) Debug.Log("existe");
        GameManager.instance.StartNextlevel(2f);
    }
}