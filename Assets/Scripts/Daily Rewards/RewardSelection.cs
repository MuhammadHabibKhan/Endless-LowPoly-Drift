using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class RewardSelection : MonoBehaviour
{
    // array to store reward amount for each day
    int[] RewardAmount = { 5, 10, 20, 50, 50, 75, 100 };

    private System.DateTime currentDateTime; // holds current datetime | updated every frame
    private System.DateTime nextCollectionDeadline; // assigned when reward collected 
    private System.DateTime lastCollectionDateTime; // to reset progress 

    private int currentDay = 0; // initializing current day as day 0
    private int currentReward;
    private System.TimeSpan endOfDay;

    private ServerTime serverScript;
    [SerializeField] private Button collectButton; // to hold the reference of collect reward button
    System.DateTime Dummy = DateTime.Parse("1/1/0001 12:00:00 AM"); // since currentDateTime is non-nullable, we test against this dummy value that it holds to wait until first value is fetched

    Color newColor;

    void Start()
    {
        getServerTime();
        nextCollectionDeadline = currentDateTime;
    }

    //void Update()
    //{
    //    getServerTime();
    //    ResetProgress();
    //    ReEnableCollectButton();
    //}

    public void RunRewardSystem()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Rewards);

        getServerTime();
        ResetProgress();
        ReEnableCollectButton();
    }

    void getServerTime()
    {
        serverScript = GetComponent<ServerTime>();

        if (serverScript != null)
        {
            StartCoroutine(serverScript.GetTimeZoneFromAPI());
            string dateInString = serverScript.response.datetime;
            Debug.Log(dateInString);
            if (serverScript.initialReq) { currentDateTime = DateTime.ParseExact(dateInString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffffffK", CultureInfo.InvariantCulture); }
        }
        else
        {
            Debug.Log("Server Time script missing");
        }
    }

    // function to collect reward on button press | Time measured from previous reward collection 
    public void CollectReward()
    {
        if (currentDateTime != Dummy)
        {
            try
            {
                if (currentDay != 0)
                {
                    System.TimeSpan timeLeft = nextCollectionDeadline - currentDateTime;

                    if (timeLeft.TotalSeconds > 0)
                    {
                        lastCollectionDateTime = currentDateTime;
                        currentReward = RewardAmount[currentDay];
                        collectButton.interactable = false;
                        CalculateNextDeadline();

                        currentDay = (currentDay + 1) % 7;

                        Debug.Log("Current Day: " + currentDay + " Reward: " + currentReward + " Time: " + currentDateTime.ToString() + " Deadline: " + nextCollectionDeadline.ToString() + " End Of Day: " + endOfDay.TotalSeconds);
                    }
                }
                else
                {
                    currentReward = RewardAmount[0]; // if Day 0, next collection deadline does not exist hence directly assign 0th element
                    currentDay = (currentDay + 1) % 7;
                    collectButton.interactable = false;
                    lastCollectionDateTime = currentDateTime;
                    CalculateNextDeadline();

                    Debug.Log("last:" + lastCollectionDateTime.ToString() + " current: " + currentDateTime.ToString());
                    Debug.Log("Current Day: " + currentDay + " Reward: " + currentReward + " Time: " + currentDateTime.ToString() + " Deadline: " + nextCollectionDeadline.ToString() + " End Of Day: " + endOfDay.TotalHours);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
        else
        {
            Debug.Log("Wait for Initial Time Fetch " + currentDateTime);
        }
    }

    void ReEnableCollectButton()
    {
        if (currentDay != 0 || currentReward == 100) // if reward collected for the first time, last collection time does not exist | need to check if end of week since day resets to 0 here as well
        {
            //endOfDay = lastCollectionDateTime.Date.Add(new System.TimeSpan(lastCollectionDateTime.Hour, lastCollectionDateTime.Minute, lastCollectionDateTime.Second + 5)) - currentDateTime;
            endOfDay = lastCollectionDateTime.Date.Add(new System.TimeSpan(24, 0, 0)) - currentDateTime;

            if (endOfDay.TotalSeconds <= 0) // if past 12am at night
            {
                collectButton.interactable = true;
            }
        }
    }

    void CalculateNextDeadline()
    {
        System.TimeSpan timeTillEndOfDay = currentDateTime.Date.Add(new System.TimeSpan(24, 0, 0)) - currentDateTime;
        //System.TimeSpan timeTillEndOfDay = currentDateTime.Date.Add(new System.TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second + 15)) - currentDateTime;
        nextCollectionDeadline = currentDateTime + timeTillEndOfDay;
        nextCollectionDeadline = nextCollectionDeadline.AddDays(1);
    }

    // Reset the cumulative reward progress if reward not collected before deadline
    void ResetProgress()
    {
        if (currentDay != 0)
        {
            System.TimeSpan timeLeft = nextCollectionDeadline - currentDateTime; // as nextCollectionDeadline is updated as soon as reward is collected

            if (timeLeft.TotalSeconds <= 0)
            {
                collectButton.interactable = true;
                currentDay = 0;
            }
        }
    }
}