using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private GameObject powerUps;
    private GameObject respawnArea;
    private GameObject[] players;
    void Start()
    {
        respawnArea = GameObject.Find("RespawnAera");
        players = GameObject.FindGameObjectsWithTag("Player");
        powerUps = GameObject.Find("PowerUps");
    }

    public GameObject[] GetPlayers()
    {
        if(players.Length == 0) {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        return players;
    }

    public GameObject[] GetChilds(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            array[i] = parent.transform.GetChild(i).gameObject;
        }
        return array;
    }

    public GameObject GetChildWithTag(GameObject parent, string tag)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.tag == tag)
            {
                return parent.transform.GetChild(i).gameObject;
            }
            else
            {
                GameObject child = GetChildWithTag(parent.transform.GetChild(i).gameObject, tag);
                if (child != null)
                {
                    return child;
                }
            }
        }
        return null;
    }

    public GameObject GetRandomInactiveChild(GameObject parent)
    {
        GameObject[] array = GetChilds(parent);
        List<GameObject> inactiveChildren = new List<GameObject>();
        foreach (GameObject child in array)
        {
            if (!child.activeSelf)
            {
                inactiveChildren.Add(child);
            }
        }
        if (inactiveChildren.Count > 0)
        {
            int randomChild = UnityEngine.Random.Range(0, inactiveChildren.Count);
            return inactiveChildren[randomChild];
        }
        else
        {
            return null;
        }
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

    public Vector3 GetPositionOfHighestPlayer()
    {
        Vector3 positionToReturn = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            if (positionToReturn.y < players[i].transform.position.y)
            {
                positionToReturn.y = players[i].transform.position.y;
            }
        }
        return positionToReturn;
    }

    public GameObject GetFirstAvailableObject(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (!gameObjects[i].activeInHierarchy)
            {
                gameObjects[i].SetActive(true);
                return gameObjects[i];
            }
        }
        return null;
    }
}
