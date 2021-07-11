using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Vignetter : MonoBehaviour
{

    static Vignetter _instance;
    public static Vignetter Instance
    {
        get { return _instance; }
    }

    Volume vol;
    public AnimationCurve lerp;
    public AnimationCurve chromaLerp;
    Vignette v;
    ChromaticAberration ca;

    [Range(0, 1)]
    public float intensity;

    [Range(0, 3)]
    public float duration;

    private void Awake()
    {
        _instance = this;

        vol = GetComponent<Volume>();

        Vignette vT;
        if (vol.profile.TryGet<Vignette>( out vT ))
        {
            v = vT;
        }

        ChromaticAberration caT;
        if (vol.profile.TryGet<ChromaticAberration>( out caT ))
        {
            ca = caT;
        }

    }


    public void ToggleVignetteOn()
    {
        StopAllCoroutines();
        StartCoroutine(VLerp(JarBehaviour.instance.jColL));
    }

    public void ToggleVignetteOff()
    {
        StopAllCoroutines();
        StartCoroutine(VLerp(Color.clear));
    }

    IEnumerator VLerp(Color to)
    {
        float t = 0.0f;
        Color cOrig = v.color.value;
        bool clear = to.a == 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;
            v.color.value = CLerp(cOrig, to, p);
            ca.intensity.value = chromaLerp.Evaluate(p);
            v.intensity.value = intensity * (clear ? lerp.Evaluate(1 - p) : lerp.Evaluate(p));

            yield return null;
        }
    }

    Color CLerp(Color a, Color b, float t)
    {
        float t_ = lerp.Evaluate(t);
        return new Color(
            Mathf.Lerp(a.r, b.r, t_),
            Mathf.Lerp(a.g, b.g, t_),
            Mathf.Lerp(a.b, b.g, t_),
            Mathf.Lerp(a.a, b.a, t_)
            );
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
