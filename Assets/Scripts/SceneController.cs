using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public delegate void ResetSceneInputDelegate();
    public static ResetSceneInputDelegate OnResetSceneInput;
   

    public void Awake()
    {

        SceneController.OnResetSceneInput += ReloadScene;
    }
    public void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        try
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void LoadScene(String sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void ReloadScene()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void Exit()
    {
        //reset distance
        Debug.Log("Out");
        Application.Quit();
    }

}
