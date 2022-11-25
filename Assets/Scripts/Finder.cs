using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject parentOfPlatform;
    private GameObject wallPaperParent;
    void Start()
    {
        parentOfPlatform =  GameObject.Find("Platforms");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPlatformParent()
    {
        return parentOfPlatform;
    }

    public GameObject GetWallPaperParent()
    {
        return wallPaperParent;
    }


    public GameObject[] GetChilds(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i))
            {
                array[i] = parent.transform.GetChild(i).gameObject;
            }

        }

        return array;
    }

    public GameObject[] GetChildsActive(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf)
            {
                array[i] = parent.transform.GetChild(i).gameObject;
            }

        }  

        return array;
    }



    public GameObject[] GetChildsNotActive(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (!parent.transform.GetChild(i).gameObject.activeSelf)
            {
                array[i] = parent.transform.GetChild(i).gameObject;
            }

        }

        return array;
    }

    public GameObject GetAChildNotActive(GameObject parent)
    {
        
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (!parent.transform.GetChild(i).gameObject.activeSelf)
            {
                return parent.transform.GetChild(i).gameObject;
            }

        }

        return null;
    }


    public int GetNumberOfActiveChild(GameObject parent)
    {
        int numberOfChildActive = 0;

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf)
            {

                numberOfChildActive++;
            }

        }

        return numberOfChildActive;
    }



    public List<GameObject> GetListOfChilds(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf)
            {
                list.Add(parent.transform.GetChild(i).gameObject);
            }

        }
        return list;
    }

    public Vector3 GetPositionOfTheHighestPlatform()
    {
        List<GameObject> arrayOfActive = GetListOfChilds(parentOfPlatform);
        Vector3 positionToReturn = Vector3.zero;
        for (int i = 0; i < arrayOfActive.Count; i++)
        {
            if (positionToReturn.y < arrayOfActive[i].transform.position.y)
            {
               positionToReturn.y = arrayOfActive[i].transform.position.y;
               
            }
        }
        return positionToReturn;
    }
}
