using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpoonBehaviour : MonoBehaviour
{
    public Vector3 mPos; // position of the mouse under world coordinate

    const int NUM_DIR = 40;
    List<Vector3> dirLog; // log of the last NUM_DIR mouse deltas

    public float avg_spd = 0.0f;
    public Vector3 avg_dir = Vector3.zero;

    public SpriteRenderer rend;

    float zInit = 10;

    private static SpoonBehaviour _instance;
    public static SpoonBehaviour Instance
    {
        get { return _instance; }
    }

    public float jam_spillage;
    public float _jam = 0;
    // the current amount of jam in the spoon
    public float jam
    {
        set
        {
            _jam = Mathf.Min(1, value);
            if (jamRend != null)
            {
                jamRend.enabled = _jam > 0;
                jamRend.transform.localScale = Vector3.one * _jam;
            }
            if (_jam == 0) {
                jh = null;
            }
        }
        get { return _jam; }
    }

    public Animator anim;

    SpriteRenderer jamRend;
    Rigidbody2D rig;
    GameObject jamAnchor;
    Color jColL, jColD;
    // CapsuleCollider2D spoonBounds;

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
    bool _inJar = false;
    public bool inJar
    {
        get
        {
            return _inJar;
        }
        set
        {
            _inJar = value;
            if (value) {
                onEnterJam();
            }
            else
            {
                onExitJam();
            }
        }
    }

    // connect to other game components
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

        jamAnchor = transform.GetChild(0).gameObject;
        jamRend = jamAnchor.transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // spoonBounds = GetComponent<CapsuleCollider2D>();

        rig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        dirLog = new List<Vector3>();
        dirLog.Add(Vector3.zero);
    }

    int measureSeg = 1; // frames per measurement of mouse
    // Update is called once per frame
    JamHistory jh = null;

    int jColUpdateSeg = 3;
    void Update()
    {
        // calculate mouse position per frame
        Vector3 mPosT = mPos;
        mPos = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Pointer.current.position.x.ReadValue(),
                Pointer.current.position.y.ReadValue(),
                zInit
                )
            );
        Vector3 dm = mPos - mPosT;

        // spoon follow mouse
        transform.position = mPos;

        // update cumulative mouse stats
        if (Time.frameCount % measureSeg == 0)
        {
            dirLog.Add(dm / Time.deltaTime);
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
            getJamInJar(dm);
        }// spill jam
        else if (jam > 0)
        {
            if ((dm.magnitude) / Time.deltaTime > jam_spillage)
            {
                spillJam(dm / Time.deltaTime);
            }
            /*
            jamRend.gameObject.transform.localPosition = new Vector3(
                   Mathf.Sin(Time.time *12) * Mathf.Max(Mathf.Abs(avg_spd / jam_spillage), 1) * Mathf.Abs(avg_dir.x),
                   Mathf.Abs(Mathf.Sin(Time.time * 10f) * Mathf.Max(Mathf.Abs(avg_spd / jam_spillage), 1) * Mathf.Abs(avg_dir.y)),
                   0.0f
                   ) * 0.03f * jam;
            */
        }

        //randomly update jam color inside spoon for "glittering" effect
        if (jamRend.enabled)
        {
            if (Time.frameCount % jColUpdateSeg == 0)
            {
                if (Random.value < (0.005f * avg_spd))
                {
                    // glitter
                    jamRend.color = jColL;
                }
                else
                {
                    jamRend.color = jColD;
                }
            }
            
        }
    }

    public void getJamInJar(Vector3 dMPos)
    {
        if (jh != null) {
            jh.update(dMPos);
            jam = jh.netDown * -0.33f;
        }
        else
        {
            jh = new JamHistory();
        }
    }

    public void onEnterJam()
    {
        jam = 0.00001f;
        jh = new JamHistory();
        jh.jType = Player.Instance.heldJamColor;
        rend.color = new Color(1f, 1f, 1f, 0.3f);

        jColL = JarBehaviour.instance.jColL;
        jColD = JarBehaviour.instance.jColD;

        anim.SetBool("scooping", true);
    }

    public void onExitJam()
    {
        anim.SetBool("scooping", false);
        rend.color = Color.white;
    }

    // clone current jam object and toss it out along mouse trail
    void spillJam(Vector3 dMPos)
    {
        Debug.Log("oops, jam spilled...");
        GameObject newJam = Instantiate(jamAnchor);

        Rigidbody2D jamRig = newJam.AddComponent<Rigidbody2D>();
        jamRig.transform.position = transform.position;
        jamRig.gravityScale = 2f;
        jamRig.position = transform.position;
        jamRig.velocity = Vector3.zero;
        jamRig.drag = 0.2f;

        // preserve size of the new jam object
        newJam.transform.localScale = Vector3.one * transform.localScale.x * jamAnchor.transform.localScale.x;
        newJam.transform.GetChild(0).transform.localPosition = Vector3.zero;

        jam = 0;
    }

    public IEnumerator ScoopGestureFinish(float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool("scooping", false);
    }
}

class JamHistory
{
    public float netDown = 0.2f;
    public VisionMode jType = VisionMode.DEFAULT;

    // return if currently in downstroke
    public bool update(Vector3 dMPos)
    {
        float dy = dMPos.y;
        float dx = dMPos.x;
        // player can scoop down and left
        if (dy < -0.1f) {
            netDown += dy;
        }
        if (dx < -0.1f)
        {
            netDown += dx;
        }
        return dy < 0;
    }
}