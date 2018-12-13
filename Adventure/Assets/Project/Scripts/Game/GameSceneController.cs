using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour {

    [Header("Game")]
    public Player player;

    [Header("UI")]
    public GameObject[] hearts;
    public Text BombText;
    public Text ArrowText;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {   
           for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < player.health);
            }
            BombText.text = "Bombs: " + player.bombAmount;
            ArrowText.text = "Arrows: " + player.arrowAmount;

        } else
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(false);
            }
            BombText.text = "Bombs: 0";
            ArrowText.text = "Arrows: 0";
        }
    }
}
