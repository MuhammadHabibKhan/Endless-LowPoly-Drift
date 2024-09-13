using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleNeonLight : MonoBehaviour
{
    bool up = true;
    bool down = false;
    float G_VAL = 0.3f;
    public Light directionalLight;

    void changeColor()
    {
        Color currentColor = directionalLight.color;

        currentColor.g = (G_VAL / 255f);
        currentColor.r = 228f/255f;
        currentColor.b = 1f;

        directionalLight.color = currentColor;

        if (G_VAL <= 0.3f)
        {
            up = true;
            down = false;
        }
        if (G_VAL >= 1f)
        {
            down = true;
            up = false;
        }

        if (up) G_VAL += 0.1f;
        else if (down) G_VAL -= 0.1f;
    }

    void Update()
    {
        changeColor();
    }
}
