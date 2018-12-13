using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSceneManager : MonoBehaviour {

	public void OnStart ()
    {
        SceneManager.LoadScene("Level 1");
    }
}
