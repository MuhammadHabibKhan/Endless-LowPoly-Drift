using System.Collections;
using UnityEngine;
using UnityEngine.UI; // If using Text
using TMPro; // If using TextMeshPro

public class CountdownController : MonoBehaviour
{
    public TextMeshProUGUI countdownText; 
    public GameObject playerCar;
    public float countdownTime = 1.0f; // Time interval between numbers

    void Start()
    {
        // Disable player control at the start
        playerCar.GetComponent<CarController>().start = 0;

        // Start the countdown coroutine
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // Display "3"
        countdownText.text = "3";
        yield return new WaitForSeconds(countdownTime);

        // Display "2"
        countdownText.text = "2";
        yield return new WaitForSeconds(countdownTime);

        // Display "1"
        countdownText.text = "1";
        yield return new WaitForSeconds(countdownTime);

        // Display "Start"
        countdownText.text = "Start";
        yield return new WaitForSeconds(0.5f); // Optional small delay for "Start"

        yield return new WaitForEndOfFrame(); // Ensures smooth frame timing before enabling controls

        // Hide the countdown text after starting
        countdownText.gameObject.SetActive(false);

        // Enable player control
        playerCar.GetComponent<CarController>().start = 1;
    }

}
