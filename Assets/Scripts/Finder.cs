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

    public Vector3 GetPositionOfTheHighestPlatform()
    {
        GameObject[] arrayOfActive = GetChildsActive(parentOfPlatform);
        Vector3 positionToReturn = Vector3.zero;
        for (int i = 0; i < arrayOfActive.Length; i++)
        {
            if (positionToReturn.y < arrayOfActive[i].transform.position.y)
            {
                positionToReturn = arrayOfActive[i].transform.position;
            }
        }
        return positionToReturn;
    }
}
