using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Room startingRoom;
    public int screenDepth = -10; 
    private float dividerConst = 2.0f; 

    private float aspectRatio;
    // Start is called before the first frame update
    void Start()
    {
        aspectRatio = (float)Screen.width / (float)Screen.height;
        resizeCamera(startingRoom);
        transform.position = startingRoom.transform.position;
        transform.position = new Vector3(startingRoom.transform.position.x, startingRoom.transform.position.y, screenDepth); // -1 to allow the camera to see the room
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Resizes the camera to ensure that it contains the entire room.
     * */
    public void resizeCamera(Room newRoom)
	{
        float roomRatio = newRoom.width / newRoom.height;
        float newOrthoSize;
        if (aspectRatio >= roomRatio)
        {
            newOrthoSize = newRoom.height / dividerConst;
        }
        else // If room is wider than aspect ratio allows, increase camera field to include full width.
        {
            newOrthoSize = (roomRatio / aspectRatio) * newRoom.height / dividerConst;
        }
        Camera.main.orthographicSize = newOrthoSize;
    }

    public void moveToPosition(Vector3 position)
	{
        position.z = screenDepth;
        this.transform.position = position;
	}
}
