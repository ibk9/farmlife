using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Wheat()
    {
        SceneManager.LoadScene(1);
    }
    public void OliveTree()
    {
        SceneManager.LoadScene(2);
    }
    public void CowBreeding()
    {
        SceneManager.LoadScene(3);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
