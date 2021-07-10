using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public abstract class SpoonListener : MonoBehaviour
{
    public abstract void OnCollideWithSpoon(float amountOfJam);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spoon"))
        {
            OnCollideWithSpoon(collision.gameObject.GetComponent<SpoonBehaviour>().jam);
        }
        else
        {
            // OnTriggerEnter2DOverride
        }
    }
}