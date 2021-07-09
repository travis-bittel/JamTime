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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpoonBehaviour s = collision.GetComponent<SpoonBehaviour>();
        if (s) {
            s.inJar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpoonBehaviour s = collision.GetComponent<SpoonBehaviour>();
        if (s)
        {
            s.inJar = false;
        }
    }
}
