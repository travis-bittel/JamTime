using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerTrigger : MonoBehaviour
{
	public delegate void TriggerEventHandler(Player player);

	public event TriggerEventHandler TriggerPublish;

	private void OnTriggerFire(Player player)
	{
		TriggerEventHandler triggerPublish = TriggerPublish;
		triggerPublish(player);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// If colliding with another room.
		GameObject other = collision.gameObject;
		if (other.CompareTag("Player"))
		{
			Player player = other.GetComponent<Player>();
			OnTriggerFire(player);
		}
	}
}
