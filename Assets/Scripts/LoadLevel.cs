using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    public  void LoadLevelScene()
    {
        SceneManager.LoadScene(1);
    }
}
