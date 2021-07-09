using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	public PlayerFacing direction = PlayerFacing.DOWN;
	public PlayerState state = PlayerState.IDLE;
}
