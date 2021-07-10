using System;
using System.Collections.Generic;
using System.Collections;
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
	PURPLE
}

public class Player : SpoonListener
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
	private VisionMode _heldJamColor; // The color of the jam the player is currently carrying
	public VisionMode heldJamColor
    {
		get
        {
			return _heldJamColor;
        }
		set
        {
			_heldJamColor = value;
			JarBehaviour.instance.pickUp(value);
        }
    }

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	// The object the player will interact with if they press the Interact Key.
	// Based on interactionPriority of the object if multiple are within interaction range.
	private InteractableObject currentInteractableObject;

	public bool canMove;

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

		if (rb == null)
        {
			rb = GetComponent<Rigidbody2D>();
		}

		if (TextManager.Instance != null)
        {
			TextManager.Instance.DisplayFixedText(Color.white, "Use Enter to dismiss text.", "Use WASD to move.");
		}
	}

	// Update is called once per frame
	void Update()
	{
		// End of frame:
		if (canMove)
        {
			Vector2 newPosition = new Vector2(transform.position.x + velocity.x * speedScalar, transform.position.y + velocity.y * speedScalar);
			rb.MovePosition(newPosition);
		}
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
		if (currentInteractableObject != null)
        {
			currentInteractableObject.OnInteract();
        }
	}

	public void OnAdvanceText()
    {
		TextManager.Instance.NextSentence();
    }

/*	public void OnCollisionEnter2D(Collision2D collision)
	{
		// If colliding with another room.
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			GameManager.Instance.queuedRoom = other.GetComponent<Room>();
		}
	}*/

/*	public void OnCollisionExit2D(Collision2D collision)
	{
		GameManager gm = GameManager.Instance;
		// If colliding with another room...
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			Room nextRoom = other.GetComponent<Room>();
			if (nextRoom == gm.queuedRoom)
			{
				gm.roomChangeEnd(nextRoom);
			}
			gm.changeRooms();
		}
	}*/

	public void OnToggleVisionModeOn()
    {
		GameManager.Instance.CurrentVisionMode = heldJamColor;
		heldJamColor = VisionMode.DEFAULT; // Remove jam jar whenever the player uses any amount of jam
		StartCoroutine(OnToggleVisionModeOff());
	}

	IEnumerator OnToggleVisionModeOff()
    {
		Debug.Log("jam vision effect will last for " + (SpoonBehaviour.Instance.jam * 5) + " seconds");
		yield return new WaitForSeconds(SpoonBehaviour.Instance.jam * 5);
		GameManager.Instance.CurrentVisionMode = VisionMode.DEFAULT;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
		// for spoon collision
		if (HandleSpoonCollision(collision)) {
			return;
        }
		////

		InteractableObject obj = collision.gameObject.GetComponent<InteractableObject>();
		if (obj != null)
        {
			if (currentInteractableObject == null || obj.interactionPriority > currentInteractableObject.interactionPriority)
            {
				currentInteractableObject = obj;

				if (obj is JamJar)
                {
					heldJamColor = ((JamJar)obj).type;
                }
			}
		}

		// If colliding with another room.
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			GameManager.Instance.queuedRoom = other.GetComponent<Room>();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		InteractableObject obj = collision.gameObject.GetComponent<InteractableObject>();
		if (currentInteractableObject == obj)
		{
			currentInteractableObject = null;
		}

		GameManager gm = GameManager.Instance;
		// If colliding with another room...
		GameObject other = collision.gameObject;
		if (other.CompareTag("Room"))
		{
			Room nextRoom = other.GetComponent<Room>();
			if (nextRoom == gm.queuedRoom)
			{
				gm.roomChangeEnd(nextRoom);
			}
			gm.changeRooms();
		}
	}
}
