using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents a room in the game. Holds a list of the objects inside of the room.
public class Room : MonoBehaviour
{
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
