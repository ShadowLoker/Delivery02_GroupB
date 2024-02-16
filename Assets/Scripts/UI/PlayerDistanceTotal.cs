using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDistanceTotal: MonoBehaviour
{
    private Vector3 antPos;
    private static float totalDistance;
    public Text scoreText;
    public GameObject scoreTextObj;

     void Start()
    {
        LoadDistance();
        scoreText = scoreTextObj.GetComponent<Text>();
        antPos = transform.position;
    }

    void FixedUpdate()
    {
        float CalcDistance = Vector3.Distance(transform.position, antPos);
        totalDistance += CalcDistance;
        //update new pos for new frame
        antPos = transform.position;
        scoreText.text = "Distancia recorreguda "+totalDistance.ToString("f1")+" Metres";

    }

    public static  void SaveDistance()
    {
        PlayerPrefs.SetFloat("Distance", totalDistance);
    }

    public static void LoadDistance()
    {
        totalDistance = PlayerPrefs.GetFloat("Distance",0f);
    }

    public static void ResetDistance()
    {
        totalDistance = 0;
        SaveDistance();
    }
   

}
