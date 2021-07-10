using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision icon");
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
