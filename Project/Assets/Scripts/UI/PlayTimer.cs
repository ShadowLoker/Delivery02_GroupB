using UnityEngine;
using UnityEngine.UI;

public class PlayTimer : MonoBehaviour
{
    private static float totalTime;
    public Text scoretext;
    public GameObject scoreTextObj;

    void Start()
    {
        LoadTime();
        scoretext = scoreTextObj.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        scoretext.text = "Temps jugat " + totalTime.ToString("f1") + " Segons";

    }

    public static void SaveTime()
    {
        PlayerPrefs.SetFloat("Time", totalTime);
    }

    public static void LoadTime()
    {
        totalTime = PlayerPrefs.GetFloat("Time",0f);
    }

    public static void ResetTime()
    {
        totalTime = 0;
        SaveTime();
    }
}
