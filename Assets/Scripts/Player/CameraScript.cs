using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Room startingRoom;
    /*
     * Helps with aspect ratio calculations.
     * Not sure why this is this number.
     */
    private float dividerConst = 2.0f; 

    private Vector3 pointNow;
    private Vector3 pointNext;

    private float aspectRatio;
    // Start is called before the first frame update
    void Start()
    {
        aspectRatio = (float)Screen.width / (float)Screen.height;
        resizeCamera(startingRoom);
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
        Debug.Log(newOrthoSize.ToString());
    }
}
