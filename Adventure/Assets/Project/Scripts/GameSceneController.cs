using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour {

    [Header("Game")]
    public Player player;

    [Header("UI")]
    public Text HealthText;
    public Text BombText;
    public Text ArrowText;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {   
            HealthText.text = "Health: " + player.health;
            BombText.text = "Bombs: " + player.bombAmount;
            ArrowText.text = "Arrows: " + player.arrowAmount;

        } else
        {
            HealthText.text = "Health: 0";
            BombText.text = "Bombs: 0";
            ArrowText.text = "Arrows: 0";
        }
    }
}
