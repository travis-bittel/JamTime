using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a room in the game. Holds a list of the objects inside of the room.
public class Room : MonoBehaviour
{
    public float width = 16.0f;
    public float height = 9.0f;
    public List<RoomObject> roomObjects;

    private void Start()
    {
        // Get all children and add them to the roomObjects list
        foreach (Transform child in transform)
        {
            RoomObject obj = child.GetComponent<RoomObject>();
            if (obj != null)
            {
                roomObjects.Add(obj);
            }
        }
        UpdateRoomObjectVisibility();
    }

    public void UpdateRoomObjectVisibility()
    {
        if (roomObjects != null)
        {
            foreach (RoomObject roomObject in roomObjects)
            {
                if (roomObject != null)
                {
                    roomObject.UpdateVisibility();
                }
            }
        }
    }
}
