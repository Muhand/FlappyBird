using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private const string HIGH_SCORE_NAME = "High Score";
    private const string SELECTED_BIRD_NAME = "Selected Bird";
    private const string GREEN_BIRD_NAME = "Green Bird";
    private const string RED_BIRD_NAME = "Red Bird";

    void Awake () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();

        //PlayerPrefs.DeleteAll();
	}

	void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void IsTheGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE_NAME, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD_NAME, 0);
            PlayerPrefs.SetInt(GREEN_BIRD_NAME, 1);
            PlayerPrefs.SetInt(RED_BIRD_NAME, 1);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_NAME, score);
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_NAME);
    }

    public void SetSelectedBird(int selectedBird)
    {
        PlayerPrefs.SetInt(SELECTED_BIRD_NAME, selectedBird);
    }
    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD_NAME);
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(GREEN_BIRD_NAME, 1);
    }
    public int IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_BIRD_NAME);
    }
    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD_NAME, 1);
    }
    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD_NAME);
    }

}
