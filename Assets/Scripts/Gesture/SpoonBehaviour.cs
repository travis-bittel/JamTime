using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpoonBehaviour : MonoBehaviour
{
    public Vector3 mPos; // position of the mouse under world coordinate

    const int NUM_DIR = 50;
    List<Vector3> dirLog; // log of the last NUM_DIR mouse deltas

    public float avg_spd = 0.0f;
    public Vector3 avg_dir = Vector3.zero;

    public SpriteRenderer rend;

    float zInit = 10;

    float _jam;
    // the current amount of jam in the spoon
    float jam
    {
        set
        {
            _jam = value;
            if (jamRend != null)
            {
                jamRend.enabled = value > 0;
            } 
        }
        get { return _jam; }
    }

    SpriteRenderer jamRend;

    /*
    // singleton setup for spoon object
    static SpoonBehaviour _instance;
    public static SpoonBehaviour instance
    {
        get { return _instance; }
    }
    
    private void Awake()
    {
        _instance = this;
    }
     */

    // whether the spoon is currently in a jar
    public bool inJar = false;

    // Start is called before the first frame update
    void Start()
    {
        dirLog = new List<Vector3>();
        dirLog.Add(Vector3.zero);

        rend = GetComponent<SpriteRenderer>();

        jamRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        jam = 0;
    }

    int measureSeg = 10; // frames per measurement of mouse
    // Update is called once per frame
    void Update()
    {
        // calculate mouse position per frame
        Vector3 mPosT = mPos;
        mPos = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Mouse.current.position.x.ReadValue(),
                Mouse.current.position.y.ReadValue(),
                zInit
                )
            );

        // spoon follow mouse
        transform.position = mPos;

        // update cumulative mouse stats
        if (Time.frameCount % measureSeg == 0)
        {
            dirLog.Add((mPos - mPosT) / Time.deltaTime);
            if (dirLog.Count > NUM_DIR)
            {
                dirLog.RemoveAt(0);
            }
            Vector3 sDir = Vector3.zero;
            float sSpd = 0.0f;
            foreach (Vector3 mt in dirLog)
            {
                sDir += mt;
                sSpd += mt.magnitude;
            }
            if (dirLog.Count > 0)
            {
                avg_dir = sDir.normalized;
                avg_spd = sSpd / dirLog.Count;
            }
            else
            {
                avg_dir = Vector3.zero;
                avg_spd = 0.0f;
            }
        }

        if (inJar)
        {
            jam = 1;
        }
        else if (eat())
        {
            jam = 0;
        }
        // spill jam
        else if (jam > 0)
        {
            if (avg_spd > 5)
            {
                spillJam();
            }
        }
    }

    // detect eating gesture
    bool eat()
    {
        return true;
    }

    // clone current jam object and toss it out along mouse trail
    void spillJam()
    {
        Debug.Log("oops, jam spilled...");
        GameObject newJam = Instantiate(jamRend.gameObject);

        Rigidbody2D jamRig = newJam.AddComponent <Rigidbody2D>();
        jamRig.gravityScale = 0.5f;
        jamRig.position = jamRend.gameObject.transform.position;
        jamRig.velocity = avg_dir * avg_spd;
    }
}
