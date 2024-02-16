using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   

    public void Awake()
    {

        PlayerMovement.OnResetSceneInput += ReloadScene;
    }

    public void OnDestroy()
    {
        PlayerMovement.OnResetSceneInput -= ReloadScene;
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
    public void LoadScene(int SceneIndex)
    {
        try
        {
            SceneManager.LoadScene(SceneIndex);

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void Win()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
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
