using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTime : MonoBehaviour
{
    private string ipApiUrl = "http://worldtimeapi.org/api/ip";
    public TimeZoneApiResponse response;
    [NonSerialized] public bool initialReq = false;

    IEnumerator GetTimeZoneFromAPI()
    {
        // 'Using' statement to get rid of request once done
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ipApiUrl))
        {
            // Send the request and wait for a response | IEnumerator + Yield is similar to async await functionality acheived as a workaround
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Parse the JSON response to get the time zone
                response = JsonUtility.FromJson<TimeZoneApiResponse>(webRequest.downloadHandler.text);
                initialReq = true;
            }
        }
    }

    public void GetServerTime()
    {
        StartCoroutine(GetTimeZoneFromAPI());
    }

}
// [Serializable] attribute is necessary for JsonUtility to work. Tells Unity that the class can be serialized (converted to and from a JSON format).
[Serializable]
public class TimeZoneApiResponse
{
    public string timezone;
    public string datetime;
    public string utc_offset;
}