using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {

    static private MissionDemolition S;

    [Header("Set in Inspector")]
    public Text uiLevel;
    public Text uiShots;
    public Text uiButton;
    public Vector3 castlePosition;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

	// Use this for initialization
	void Start () {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
	}

    void StartLevel()
    {
        if(castle != null)
        {
            Destroy(castle);
        }

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }

        castle = Instantiate(castles[level]);
        castle.transform.position = castlePosition;
        shotsTaken = 0;

        SwitchView("Show Both");

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        uiLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uiShots.text = "Shots Taken: " + shotsTaken;
    }
	


	// Update is called once per frame
	void Update () {
        UpdateGUI();

        if(mode == GameMode.playing && Goal.goalMet)
        {

            mode = GameMode.levelEnd;

            SwitchView("Show Both");

            Invoke("NextLevel", 2f);
        }

		
	}


    void NextLevel()
    {
        level++;

        if(level == levelMax)
        {
            level = 0;
        }

        StartLevel();
    }

    public void SwitchView(string newView = "")
    {
        if(newView == "")
        {
            newView = uiButton.text;
        }

        showing = newView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCamera.PointOfInterest = null;
                uiButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCamera.PointOfInterest = S.castle;
                uiButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCamera.PointOfInterest = GameObject.Find("ViewBoth");
                uiButton.text = "Show Slingshot";
                break;
        }
    }


    public static void ShotFired()
    {
        S.shotsTaken++;
    }

}
