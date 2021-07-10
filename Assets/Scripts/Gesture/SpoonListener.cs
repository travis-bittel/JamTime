using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class SpoonListener : MonoBehaviour
{
    Collider2D col;
    public UnityEvent onSpoonCollision;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spoon"))
        {
            Debug.Log("colliding with spoon");
            onSpoonCollision.Invoke();
        }
    }
}