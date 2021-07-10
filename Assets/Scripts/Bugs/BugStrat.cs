using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface BugStrat
{
	void OnPlayerContact(Bug self);
	void OnHiveContact(Bug self);
}
