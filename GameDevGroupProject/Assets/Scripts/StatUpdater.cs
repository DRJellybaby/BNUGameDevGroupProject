using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpdater : MonoBehaviour
{
    PlayerStats player;
    PlayerControler playerControler;
    public GameObject health;
    public GameObject stamina;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        playerControler = GameObject.FindWithTag("Player").GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        temp = new Vector3(player.stamina / player.maxStamina, 1, 1);
        stamina.GetComponent<RectTransform>().localScale =  temp;
        temp = new Vector3(playerControler.playerHealth / 200, 1, 1);
        health.GetComponent<RectTransform>().localScale = temp;
    }
}
