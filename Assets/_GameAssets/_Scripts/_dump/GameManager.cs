using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    bool isPause = false;

    public Text progress_text;

    public static GameManager instance;

    public GameObject keyObject;
    public GameObject doorObject;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelText("please kill the enemy");
    }

    internal void LevelText(string v)
    {
        progress_text.text = v;
    }

    public void PauseGameBtn()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void EnableKey()
    {
        keyObject.SetActive(true);
    }


}
