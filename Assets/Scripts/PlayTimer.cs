using UnityEngine;
using UnityEngine.UI;

public class PlayTimer : MonoBehaviour
{
    private float TotalTime;
    public Text Scoretext;
    public GameObject ScoreTextObj;

    void Start()
    {
        Scoretext = ScoreTextObj.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        TotalTime += Time.deltaTime;
        Scoretext.text = "Temps jugat " + TotalTime.ToString("f1") + " Segons";

    }
}
