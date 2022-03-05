using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreComp : MonoBehaviour
{
    public Slider backgroundSlider;

    public TMP_Text YourScore;
    public TMP_Text OppScore;

    public TMP_Text YourName;
    public TMP_Text OppName;

    public TMP_Text PlayerName;

    [HideInInspector] public int yourScore = 0;
    [HideInInspector] public int oppsScore = 0;

    void Start()
    {
        backgroundSlider.value = 50f;

        if (PlayerName.text == "")
        {
            YourName.text = "player";
        }
        else
        {
            YourName.text = PlayerName.text;
        }

        

        OppName.text = "Bot";
    }

    // Update is called once per frame
    void Update()
    {
        YourScore.text = yourScore.ToString();
        OppScore.text = oppsScore.ToString();
    }
}
