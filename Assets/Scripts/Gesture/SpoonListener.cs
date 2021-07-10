using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
//using FMOD;

abstract public class SpoonListener : MonoBehaviour
{
    public UnityEvent onEatJam;
    // the minimum amount of jam to trigger jam eating event
    [Range(0, 1)]
    public float jamAmount;
    [FMODUnity.EventRef]
    public string success_sfx, failure_sfx;

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
                Player.Instance.heldJamColor = VisionMode.DEFAULT;

                s.jam = 0;
                s.anim.SetBool("scooping", true);
                s.StartCoroutine(s.ScoopGestureFinish(1.5f));

                // play sfx
                FMODUnity.RuntimeManager.PlayOneShot(success_sfx, transform.position);

                return true;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(failure_sfx, transform.position);
                Debug.Log("\tthere isn't enough jam...");
            }
        }
        return false;
    }
}
