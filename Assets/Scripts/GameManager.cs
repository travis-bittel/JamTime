using System;
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
	public Room queuedRoom;

	private Vector3 cameraStart;
	private Vector3 cameraEnd;

	//Variables used to interpolate camera position.
	private bool movingCamera;

	public float transitionTime = 2.0f;
	private float currentTransitionTime;
	public VisionMode CurrentVisionMode
	{
		get { return _currentVisionMode; }
		set
		{
			_currentVisionMode = value;
			if (currentRoom == null)
			{
				Debug.LogError("GameManager's currentRoom was null!");
			}
			else
			{
				currentRoom.UpdateRoomObjectVisibility();

			}
		}
	}

	private void Start()
	{
		CurrentVisionMode = VisionMode.DEFAULT;
		movingCamera = false;
	}

	private void Update()
	{
		if (movingCamera)
		{
			roomChangeStep();
		}
	}

	public void changeRooms(Room nextRoom)
	{
		Debug.Log("Room Change Requested");
		if (movingCamera)
		{
			Debug.Log("Finalizing last transition.");
			roomChangeEnd(queuedRoom);
		}
		roomChangeStart(nextRoom);
	}

	private void roomChangeStart(Room nextRoom)
	{
		movingCamera = true;
		currentTransitionTime = 0.0f;
		cameraStart = Camera.main.transform.position;
		cameraEnd = nextRoom.transform.position;
		Debug.Log("Going from" + cameraStart.ToString() + " to " + cameraEnd.ToString());
	}

	private void roomChangeStep()
	{
		currentTransitionTime = Mathf.Min(transitionTime, currentTransitionTime + Time.deltaTime);
		// Interpolate camera view.
		float progress = currentTransitionTime / transitionTime;
		Vector3 interpolation = cameraStart * (1 - progress) +
			cameraEnd * progress;

		// End Room Change.
		if (currentTransitionTime >= transitionTime)
		{
			roomChangeEnd(queuedRoom);
		} 
		else
		{
			Camera.main.transform.position = interpolation;
		}
	}

	private void roomChangeEnd(Room nextRoom)
	{
		movingCamera = false;
		Debug.Log("Room Transition Complete.");
		CameraScript cs = Camera.main.GetComponent<CameraScript>();
		cs.resizeCamera(nextRoom);
		currentRoom = nextRoom;
		Camera.main.transform.position = nextRoom.transform.position;
	}
}