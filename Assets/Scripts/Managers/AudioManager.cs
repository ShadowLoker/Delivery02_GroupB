using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static List<EnemyAI> enemies = new List<EnemyAI>();

    public GameObject rithm;
    public GameObject beat;
    public GameObject drums;

    private int state;

    private void Awake()
    {
        state = 0;
        enemies = new List<EnemyAI>();
    }
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
            case 0:
                rithm.GetComponent<AudioSource>().volume = 0;
                beat.GetComponent<AudioSource>().volume = 0;
                drums.GetComponent<AudioSource>().volume = 1;
                break;
            case 1:
                rithm.GetComponent<AudioSource>().volume = 0;
                beat.GetComponent<AudioSource>().volume = 1;
                drums.GetComponent<AudioSource>().volume = 1;
                break;
            case 2:
                rithm.GetComponent<AudioSource>().volume = 1;
                beat.GetComponent<AudioSource>().volume = 1;
                drums.GetComponent<AudioSource>().volume = 1;
                break;
        }

    }

    public static void AddEnemy(EnemyAI enemy)
    {
        enemies.Add(enemy);
    }
}
