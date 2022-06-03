using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    //when player enters portal, change scene
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(1);
    }
}
