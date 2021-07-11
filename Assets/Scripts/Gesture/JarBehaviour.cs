using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarBehaviour : MonoBehaviour
{
    //BoxCollider2D box;
    CapsuleCollider2D capsule;
    SpriteRenderer rend;

    public Sprite purple, red, yellow, empty;

    static JarBehaviour _instance;
    public static JarBehaviour instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        capsule = GetComponent<CapsuleCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // box = GetComponent<BoxCollider2D>();
        // capsule = GetComponent<CapsuleCollider2D>();
        // rend = GetComponent<SpriteRenderer>();

        // force set to default
        // pickUp(VisionMode.DEFAULT);
        rend.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = capsule.bounds;
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
                bounds.size.y / 2.0f,
                0.0f
                );

        transform.position = ll;

        //rend.color = jColL;
    }

    public void pickUp(VisionMode vm)
    {
        if (rend == null || capsule == null) { return; }
        capsule.enabled = true;
        System.Tuple<Color, Color> jCols = jamToCol(vm);
        jColL = jCols.Item1;
        jColD = jCols.Item2;
        rend.color = Color.white;
        switch (vm)
        {
            case VisionMode.PURPLE:
                rend.sprite = purple;
                break;
            case VisionMode.RED:
                rend.sprite = red;
                break;
            case VisionMode.YELLOW:
                rend.sprite = yellow;
                break;
            case VisionMode.DEFAULT:
                rend.sprite = empty;
                break;
        }
    }

    public Color jColL = Color.clear, jColD = Color.clear;

    public static System.Tuple<Color, Color> jamToCol(VisionMode vm)
    {
        switch (vm)
        {
            case VisionMode.PURPLE:
                return new System.Tuple<Color, Color>(new Color(.58f, .25f, .93f, 0.8f), new Color(.41f, .09f, .76f, 0.8f));
            case VisionMode.RED:
                return new System.Tuple<Color, Color>(new Color(1.0f, 0.19f, 0.22f, 0.8f), new Color(.74f, .07f, .09f, 0.8f));
            case VisionMode.YELLOW:
                return new System.Tuple<Color, Color>(new Color(1.0f, 0.87f, 0.05f, 0.8f), new Color(0.92f, 0.8f, 0.05f, 0.8f));
            case VisionMode.DEFAULT:
                return new System.Tuple<Color, Color>(Color.clear, Color.clear);
        }
        return new System.Tuple<Color, Color>(Color.clear, Color.clear);
    }

    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("collision icon");
        SpoonBehaviour s = collision.gameObject.GetComponent<SpoonBehaviour>();
        if (s != null && Player.Instance.heldJamColor != VisionMode.DEFAULT)
        {
            s.inJar = true;

            if (anim)
            {
                anim.SetBool("wobble", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpoonBehaviour s = collision.gameObject.GetComponent<SpoonBehaviour>();
        if (s)
        {
            s.inJar = false;
            
            if (anim)
            {
                anim.SetBool("wobble", false);
            }
        }
    }
}
