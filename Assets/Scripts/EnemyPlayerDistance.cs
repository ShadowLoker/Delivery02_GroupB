using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDistanceTotal: MonoBehaviour
{
    private Vector3 AntPos;
    private float TotalDistance;
    public Text Scoretext;
    public GameObject ScoreTextObj;

     void Start()
    {
        Scoretext = ScoreTextObj.GetComponent<Text>();
        AntPos = transform.position;
    }

    void Update()
    {
        float CalcDistance = Vector3.Distance(transform.position, AntPos);
        TotalDistance += CalcDistance;
        //update new pos for new frame
        AntPos = transform.position;
        Scoretext.text = "Distancia recorreguda "+TotalDistance.ToString("f1")+" Metres";

    }
   

}
