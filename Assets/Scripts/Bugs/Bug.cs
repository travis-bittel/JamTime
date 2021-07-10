using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public Transform target;
    public Transform home;
    public float speed;
    public BugStrat strategy;

	#region Creators
    public Bug(Transform target)
    {
        this.target = target;
        this.strategy = null;
    }
    public Bug(Transform target, BugStrat strategy)
	{
        this.target = target;
        this.strategy = strategy;
    }
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        target = Player.Instance.transform;
        if (strategy == null)
		{
            this.strategy = new TakeJamBugStrat(home);
        }
    }

    // Update is called once per frame
    void Update()
    {
        step();
    }

    private void step()
	{
        Vector3 movement = (target.position - transform.position);
        movement = movement.normalized * speed;
        this.transform.position += movement;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject other = collision.gameObject;

		if (other.CompareTag("Player"))
		{
            strategy.OnPlayerContact(this);
		}
        else if (other.GetComponent<Hive>() != null) // Is a hive.
        {
            strategy.OnHiveContact(this);
        }
    }
}
