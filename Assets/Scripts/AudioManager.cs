using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static List<EnemyAI> enemies = new List<EnemyAI>();

    public GameObject Base;
    public GameObject Beat;
    public GameObject Drums;

    private int state = 0;

    private void Update()
    {
        state = 0;
        foreach (EnemyAI enemy in enemies)
        {
            if(enemy.detectionState == FoV.PlayerDetectionState.PartiallyDetected)
            {
                state = 1;
                break;
            }
            if(enemy.detectionState == FoV.PlayerDetectionState.FullyDetected)
            {
                state = 2;
                break;
            }

        }

        switch(state)
        {
            case 2:
                Base.GetComponent<AudioSource>().volume = 0;
                Beat.GetComponent<AudioSource>().volume = 0;
                Drums.GetComponent<AudioSource>().volume = 1;
                break;
            case 1:
                Base.GetComponent<AudioSource>().volume = 0;
                Beat.GetComponent<AudioSource>().volume = 1;
                Drums.GetComponent<AudioSource>().volume = 1;
                break;
            case 0:
                Base.GetComponent<AudioSource>().volume = 1;
                Beat.GetComponent<AudioSource>().volume = 1;
                Drums.GetComponent<AudioSource>().volume = 1;
                break;
        }

    }

    public static void AddEnemy(EnemyAI enemy)
    {
        enemies.Add(enemy);
    }
}
