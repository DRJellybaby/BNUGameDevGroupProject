using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void Leveltwo()
    {
        SceneManager.LoadScene(2);
    }
}
