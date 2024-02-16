using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    public GameObject endTime;
    public GameObject endDistance;
    public GameObject newBest;
    public Text endTimeText;
    public Text endDistanceText;

    private void Start()
    {
        bool NewBestTime = PlayerPrefs.GetFloat("Time", 0f) < PlayerPrefs.GetFloat("BestTime", 0f);
        bool NewBestDistance = PlayerPrefs.GetFloat("Distance", 0f) < PlayerPrefs.GetFloat("BestDistance", 0f);
        if (NewBestTime||NewBestDistance)
        {
            if(NewBestTime)
            {
                PlayerPrefs.SetFloat("BestTime", PlayerPrefs.GetFloat("Time", 0f));
            }
            
            if(NewBestDistance)
            {
                PlayerPrefs.SetFloat("BestDistance", PlayerPrefs.GetFloat("Distance", 0f));
            }
            newBest.SetActive(true);
        }else
        {
            newBest.SetActive(false);
        }
        endTimeText = endTime.GetComponent<Text>();
        endDistanceText = endDistance.GetComponent<Text>();
        endTimeText.text = "Time: "+PlayerPrefs.GetFloat("Time",0f)+" Best Time: "+PlayerPrefs.GetFloat("BestTime",0f);
        endDistanceText.text = "Distance: "+PlayerPrefs.GetFloat("Distance",0f)+" Best Distance: "+PlayerPrefs.GetFloat("BestDistance",0f);
    }
}
