using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarBehaviour : MonoBehaviour
{
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = box.bounds;
        // Debug.Log(bounds.size.x + ", " + bounds.size.y);
        // snap to lower left corner
        Vector3 ll = Camera.main.ScreenToWorldPoint(
            new Vector3(
                0,
                0,
                10
                )
            
            ) + new Vector3(
                bounds.size.x / 2.0f,
                bounds.size.x / 2.0f,
                0.0f
                );

        transform.position = ll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("collision icon");
        SpoonBehaviour s = collision.gameObject.GetComponent<SpoonBehaviour>();
        if (s)
        {
            s.inJar = true;
            s.onEnterJam();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SpoonBehaviour s = collision.gameObject.GetComponent<SpoonBehaviour>();
        if (s)
        {
            s.inJar = false;
        }
    }
}
