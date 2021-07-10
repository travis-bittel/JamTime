using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class SpoonListener : MonoBehaviour
{
    public UnityEvent onEatJam;
    // the minimum amount of jam to trigger jam eating event
    [Range(0, 1)]
    public float jamAmount;

    // Start is called before the first frame update
    void Start()
    {
        if (onEatJam == null)
        {
            throw new UnityException("You must assign onEatJam. Take a look at inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // handle spoon collisions
    protected bool HandleSpoonCollision(Collider2D collision)
    {
        SpoonBehaviour s = collision.GetComponent<SpoonBehaviour>();
        if (s)
        {
            Debug.Log(this.name + " is eating jam");
            if (s.jam > jamAmount) {
                Debug.Log("\tjam ate...");
                onEatJam.Invoke();
                s.jam = 0;
                return true;
            }
            else
            {
                Debug.Log("\tthere isn't enough jam...");
            }
        }
        return false;
    }
}
