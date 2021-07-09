using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerFacing
{
	UP,
	LEFT,
	RIGHT,
	DOWN
}

public enum PlayerState
{
	WALK,
	IDLE,
	DIE,
	OTHER
}

public class Player : MonoBehaviour
{
	#region Singleton Code
	private static Player _instance;

	public static Player Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Debug.LogError("Attempted to Instantiate multiple Players in one scene!");
			Destroy(this.gameObject);
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

	public PlayerFacing direction;
	public PlayerState state = PlayerState.IDLE;
	public float speedScalar;
	public Vector3 velocity;

	// Start is called before the first frame update
	void Start()
	{
		if (speedScalar == 0)
        {
			Debug.LogWarning("Speed was set to 0, defaulting to 0.05");
        }
		direction = PlayerFacing.DOWN;
		state = PlayerState.IDLE;
		velocity = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{

		// End of frame:
		transform.position += velocity * speedScalar;
	}

	// Happens onPress and Release.
	public void OnMove(InputValue value)
	{
		Vector2 moveDir = value.Get<Vector2>();
		velocity = new Vector3(moveDir.x, moveDir.y, 0);
	}
	public void OnInteract()
    {

    }
	public void OnToggleVisionMode()
    {

    }
}
