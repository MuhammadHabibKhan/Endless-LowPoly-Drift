using UnityEngine;

public class SkyBoxTransition : MonoBehaviour
{
    public Material skyboxMaterial;      // Reference to the skybox material
    public float dayNightDuration = 120f; // Duration of one full day-night cycle in seconds
    public float minExposure = 0.1f;     // Minimum exposure for the night (almost black)
    public float maxExposure = 1.3f;     // Maximum exposure for the day (bright)

    private float cycleTime = 0f;        // Time accumulator for day-night cycle

    void Start()
    {
        // Set the skybox material in RenderSettings
        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.skybox.SetFloat("_Exposure", maxExposure); // Start fully bright (day)
        InvokeRepeating("UpdateEnv", 0.5f, 0.5f);
    }

    void Update()
    {
        // Calculate normalized cycle time (0 to 1) based on dayNightDuration
        cycleTime += Time.deltaTime / dayNightDuration;

        // Use PingPong to smoothly transition between day and night
        float exposureValue = Mathf.Lerp(minExposure, maxExposure, Mathf.PingPong(cycleTime * 2, 1));

        // Set the exposure value to simulate the sky dimming and brightening
        skyboxMaterial.SetFloat("_Exposure", exposureValue);

        // Reset cycle time to prevent overflow
        if (cycleTime >= 1f)
        {
            cycleTime = 0f;
        }
    }

    void UpdateEnv()
    {
        // Update global lighting to reflect the new skybox exposure
        DynamicGI.UpdateEnvironment();
    }
}
