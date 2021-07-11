using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : SpoonListener
{
    public Transform target;
    public Transform home;
    public float speed;
    public BugStrat strategy;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

	// Start is called before the first frame update
	void Start()
    {
        this.target = Player.Instance.transform;
        if (strategy == null)
		{
            this.strategy = new TakeJamBugStrat(home);
        }

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (movement.x >= 0)
        {
            spriteRenderer.flipY = true;
        } else
        {
            spriteRenderer.flipY = false;
        }
        this.transform.position += movement;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject other = collision.gameObject;

		if (other.CompareTag("Player"))
		{
            strategy.OnPlayerContact(this);
		}
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
        GameObject other = collision.gameObject;

        if (other.GetComponent<Hive>() != null) // Is a hive.
        {
            strategy.OnHiveContact(this);
        }
    }
}
