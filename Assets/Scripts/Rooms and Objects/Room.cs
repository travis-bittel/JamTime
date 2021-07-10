using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a room in the game. Holds a list of the objects inside of the room.
public class Room : MonoBehaviour
{
    public float width = 16.0f;
    public float height = 9.0f;
    public List<RoomObject> roomObjects;

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
