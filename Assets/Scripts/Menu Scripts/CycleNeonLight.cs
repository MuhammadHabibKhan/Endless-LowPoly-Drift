using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CycleNeonLight : MonoBehaviour
{
    public Light directionalLight;
    
    Color greenShade;
    Color originalColor;
    float lerpDuration = 2f; // time to complete one cycle

    private void Start()
    {
        originalColor = directionalLight.color;
        greenShade = new Color(directionalLight.color.r, 1f, directionalLight.color.b, directionalLight.color.a);
    }

    void Update()
    {
        // ping pong oscillates value b/w 0 and 2nd param | 1st param is any increasing value 
        float t = Mathf.PingPong(Time.time / lerpDuration, 1f);
        directionalLight.color = Color.Lerp(greenShade, originalColor, t);
    }
}
