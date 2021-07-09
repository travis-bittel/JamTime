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
	public PlayerFacing direction;
	public PlayerState state = PlayerState.IDLE;
	public float SpeedScalar = 0.04f;
	public Vector3 speed;

	// Start is called before the first frame update
	void Start()
	{
		direction = PlayerFacing.DOWN;
		state = PlayerState.IDLE;
		speed = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{

		// End of frame:
		transform.position += speed * SpeedScalar;
	}

	// Happens onPress and Release.
	public void OnMove(InputValue value)
	{
		Debug.Log("Henlo");
		Vector2 moveDir = value.Get<Vector2>();
		speed = new Vector3(moveDir.x, moveDir.y, 0);
	}
}
