using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TakeJamBugStrat : BugStrat
{
	public Transform home;
	public TakeJamBugStrat(Transform home)
	{
		this.home = home;
	}

	public void OnPlayerContact(Bug self) {
		Player.Instance.heldJamColor = VisionMode.DEFAULT;
		self.target = this.home;
	}

	public void OnHiveContact(Bug self)
	{
		// If your target was the hive, that means the bug was goin' home.
		if (self.target == this.home)
		{
			UnityEngine.Object.Destroy(self.gameObject);
		}
	}
}
