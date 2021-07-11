using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Noiser : MonoBehaviour
{
    Light2D l;

    float int_0;
    float sInt_0;
    float r_0;

    Vector2 seed;
    void Awake()
    {
        l = GetComponent<Light2D>();
        int_0 = l.intensity;
        sInt_0 = l.shadowIntensity;
        r_0 = l.pointLightInnerRadius;

        seed = new Vector2(Random.value * 20, Random.value * 20);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    [Range(0, 1)]
    public float deviation;
    float fluc(float a, float d)
    {
        return a * (1 + deviation * d);
    }

    // Update is called once per frame
    void Update()
    {
        float n = Mathf.PerlinNoise(Time.time + seed.x, Time.time + seed.y);

        l.intensity = fluc(int_0, n);
        l.shadowIntensity = fluc(sInt_0, n);
        l.pointLightInnerRadius = fluc(r_0, n);
    }
}
