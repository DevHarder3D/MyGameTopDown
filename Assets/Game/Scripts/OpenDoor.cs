using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject newDoor;

    public void OpenDoors()
    {
        Destroy(door);
        newDoor.gameObject.SetActive(true);
    }
}
