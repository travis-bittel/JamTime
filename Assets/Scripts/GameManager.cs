using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton Code
	private static GameManager _instance;

	public static GameManager Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Debug.LogError("Attempted to Instantiate multiple GameManager in one scene!");
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void OnDestroy()
	{
		if (this == _instance) { _instance = null; }
	}
	#endregion

	private VisionMode _currentVisionMode;

	public Room currentRoom;

	public VisionMode CurrentVisionMode
    {
		get { return _currentVisionMode; }
		set
        {
			_currentVisionMode = value;
			if (currentRoom == null)
            {
				Debug.LogError("GameManager's currentRoom was null!");
            } else
            {
				currentRoom.UpdateRoomObjectVisibility();

			}
        }
    }

    private void Start()
    {
		CurrentVisionMode = VisionMode.DEFAULT;
    }
}
