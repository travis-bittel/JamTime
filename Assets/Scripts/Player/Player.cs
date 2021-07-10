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

public enum VisionMode
{
	DEFAULT,
	RED,
	YELLOW,
	BLUE
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

	public PlayerFacing direction;
	public PlayerState state = PlayerState.IDLE;
	public float speedScalar;
	public Vector3 velocity;
	public VisionMode heldJamColor; // The color of the jam the player is currently carrying

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
		direction = GetFacingDirectionFromVelocity();
	}
	public void OnInteract()
    {

    }

	public void OnCollisionEnter2D(Collision2D collision)
	{
		// If colliding with another room.
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			GameManager.Instance.queuedRoom = other.GetComponent<Room>();
		}
	}

	public void OnCollisionExit2D(Collision2D collision)
	{
		GameManager gm = GameManager.Instance;
		// If colliding with another room...
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			gm.changeRooms(gm.queuedRoom);
		}
	}

	public void OnToggleVisionMode()
    {
		if (GameManager.Instance.CurrentVisionMode != heldJamColor)
        {
			GameManager.Instance.CurrentVisionMode = heldJamColor;
		} else
        {
			GameManager.Instance.CurrentVisionMode = VisionMode.DEFAULT;
		}
    }

	private PlayerFacing GetFacingDirectionFromVelocity()
    {
		if (velocity.x > 0)
        {
			return PlayerFacing.RIGHT;
        } else if (velocity.x < 0)
        {
			return PlayerFacing.LEFT;
		} else if (velocity.y > 0)
        {
			return PlayerFacing.UP;
		} else
        {
			return PlayerFacing.DOWN;
		}
    }
}
