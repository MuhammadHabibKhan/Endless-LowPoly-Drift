using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardSelection : MonoBehaviour
{
    public int currentDay;
    public int currentReward;
    private TimeSpan endOfDay;
    
    public static RewardSelection instance;

    int[] RewardAmount = { 5, 10, 20, 50, 50, 75, 100 }; // array to store reward amount for each day

    [SerializeField] public DateTime currentDateTime = DateTime.Now;
    [SerializeField] public DateTime nextCollectionDeadline;
    [SerializeField] public DateTime lastCollectionDateTime;

    [SerializeField] private Button collectButton;
    private bool buttonState;

    private ServerTime serverScript;
    // since currentDateTime is non-nullable, we test against this dummy value that it holds to wait until first value is fetched
    DateTime Dummy = DateTime.Parse("1/1/0001 12:00:00 AM");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe when destroyed
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Fetching values from PlayerPrefs

        currentDay = PlayerPrefs.GetInt("CurrentDay", 0);
        try
        {
            nextCollectionDeadline = DateTime.Parse(PlayerPrefs.GetString("NextCollectionDeadline"));
            lastCollectionDateTime = DateTime.Parse(PlayerPrefs.GetString("LastCollectionDateTime"));
        }
        catch
        {
            nextCollectionDeadline = DateTime.MinValue;
            lastCollectionDateTime = DateTime.MinValue;
        }
        buttonState = (PlayerPrefs.GetInt("ButtonState") != 0);

        Button[] ButtonList = FindObjectsOfType<Button>();

        for (int i = 0; i < ButtonList.Length; i++)
        {
            Button button = ButtonList[i];
            if (button.name == "CollectButton")
            {
                collectButton = button;
                collectButton.onClick.AddListener(CollectReward);
                collectButton.interactable = buttonState;
            }
        }
    }
    void Start()
    {
        currentDateTime = DateTime.Now;
        //getServerTime();

        if (nextCollectionDeadline == DateTime.MinValue)
        {
            Debug.Log("null next");
            nextCollectionDeadline = currentDateTime;
        }

        InvokeRepeating("RunRewardSystem", 0f, 1f);
    }

    public void RunRewardSystem()
    {
        currentDateTime = DateTime.Now;
        //getServerTime();
        DeactivateAllSquares();
        ResetProgress();
        ReEnableCollectButton();
        ActivateGreenSquare();
    }

    private void ActivateGreenSquare()
    {
        GameObject[] greenSquares = GameObject.FindGameObjectsWithTag("GreenSquare");

        for (int i = 0; i < greenSquares.Length; i++)
        {
            if (greenSquares[i].name == (currentDay + 1).ToString())
            {
                Image image = greenSquares[i].GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }
        }
    }

    public void DeactivateAllSquares()
    {
        GameObject[] greenSquares = GameObject.FindGameObjectsWithTag("GreenSquare");

        for (int i = 0; i < greenSquares.Length; i++)
        {
            Image image = greenSquares[i].GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
    }

    void getServerTime()
    {
        serverScript = GetComponent<ServerTime>();

        if (serverScript != null)
        {
            serverScript.GetServerTime();
        }
        else
        {
            Debug.Log("Server Time script missing");
        }
    }

    //function to collect reward on button press | Time measured from previous reward collection
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
                        buttonState = false;
                        PlayerPrefs.SetInt("ButtonState", 0);
                        CalculateNextDeadline();

                        currentDay = (currentDay + 1) % 7;
                        PlayerPrefs.SetInt("CurrentDay", currentDay);

                        Debug.Log("Current Day: " + currentDay + " Reward: " + currentReward + " Time: " + currentDateTime.ToString() + " Deadline: " + nextCollectionDeadline.ToString() + " End Of Day: " + endOfDay.TotalSeconds);
                    }
                }
                else
                {
                    currentReward = RewardAmount[0]; // if Day 0, next collection deadline does not exist hence directly assign 0th element
                    currentDay = (currentDay + 1) % 7;
                    PlayerPrefs.SetInt("CurrentDay", currentDay);
                    collectButton.interactable = false;
                    buttonState = false;
                    PlayerPrefs.SetInt("ButtonState", 0);
                    lastCollectionDateTime = currentDateTime;
                    CalculateNextDeadline();

                    Debug.Log("last:" + lastCollectionDateTime.ToString() + " current: " + currentDateTime.ToString());
                    Debug.Log("Current Day: " + currentDay + " Reward: " + currentReward + " Time: " + currentDateTime.ToString() + " Deadline: " + nextCollectionDeadline.ToString() + " End Of Day: " + endOfDay.TotalHours);
                }
                PlayerPrefs.SetString("LastCollectionDateTime", lastCollectionDateTime.ToString());
                AudioManager.instance.PlaySFX("reward");
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
        //endOfDay = lastCollectionDateTime.Date.Add(new System.TimeSpan(lastCollectionDateTime.Hour, lastCollectionDateTime.Minute, lastCollectionDateTime.Second + 15)) - currentDateTime;
        endOfDay = lastCollectionDateTime.Date.Add(new System.TimeSpan(24, 0, 0)) - currentDateTime;

        if (endOfDay.TotalSeconds <= 0) // if past 12am at night
        {
            collectButton.interactable = true;
            buttonState = true;
            PlayerPrefs.SetInt("ButtonState", 1);
        }
    }

    void CalculateNextDeadline()
    {
        //System.TimeSpan timeTillEndOfDay = currentDateTime.Date.Add(new System.TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second + 25)) - currentDateTime;

        System.TimeSpan timeTillEndOfDay = currentDateTime.Date.Add(new System.TimeSpan(24, 0, 0)) - currentDateTime;
        nextCollectionDeadline = currentDateTime + timeTillEndOfDay;
        PlayerPrefs.SetString("NextCollectionDeadline", nextCollectionDeadline.ToString());
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
                buttonState = true;
                PlayerPrefs.SetInt("ButtonState", 1);
                currentDay = 0;
                PlayerPrefs.SetInt("CurrentDay", currentDay);
            }
        }
    }
}