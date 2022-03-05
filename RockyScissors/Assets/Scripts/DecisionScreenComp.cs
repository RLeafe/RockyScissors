using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionScreenComp : MonoBehaviour
{
    public ScoreComp scoreComp;
    public CheckButtons checkButtons;

    public TMP_Text draw;

    public Image[] YourHands = new Image[3];
    public TMP_Text[] YourDecision = new TMP_Text[2];

    public Image[] OppsHands = new Image[3];
    public TMP_Text[] OppsDecision = new TMP_Text[2];

    public Canvas winningCanvas;
    public TMP_Text winnerName;
    public TMP_Text finalWin;

    [HideInInspector] public bool resetGame = false;

    private int yourNumChoice;
    private int oppNumChoice;

    private float onScreen = 1f;
    private float decideWinNum = 2f;
    private float onReset = 4f;

    private float winPercent;

    [HideInInspector] public bool displayWinner = false;
    private bool BestOfThree = false, BestOfFive = false;
    private int BestOf;
    private int yourCounter, oppsCounter;

    // Start is called before the first frame update
    void Start()
    {
        scoreComp = ScoreComp.FindObjectOfType<ScoreComp>();
        checkButtons.GetComponent<CheckButtons>();
    }

    public void GameLogic()
    {
        if (yourNumChoice == oppNumChoice) { draw.enabled = true; }

        switch (yourNumChoice)
        {
            case 0:
                if (oppNumChoice == 1) { YourDecision[1].enabled = true; OppsDecision[0].enabled = true; scoreComp.oppsScore += 1; scoreComp.backgroundSlider.value += winPercent; if (BestOfThree) { BestOf -= 1; oppsCounter += 1; } } 
                else if (oppNumChoice == 2) { YourDecision[0].enabled = true; OppsDecision[1].enabled = true; scoreComp.yourScore += 1; scoreComp.backgroundSlider.value -= winPercent; if (BestOfThree) { BestOf -= 1; yourCounter += 1; } }
                break;
            case 1:
                if (oppNumChoice == 0) { YourDecision[0].enabled = true; OppsDecision[1].enabled = true; scoreComp.yourScore += 1; scoreComp.backgroundSlider.value -= winPercent; if (BestOfThree) { BestOf -= 1; yourCounter += 1; } } 
                else if (oppNumChoice == 2) { YourDecision[1].enabled = true; OppsDecision[0].enabled = true; scoreComp.oppsScore += 1; scoreComp.backgroundSlider.value += winPercent; if (BestOfThree) { BestOf -= 1; oppsCounter += 1; } }
                break;
            case 2:
                if (oppNumChoice == 0) { YourDecision[1].enabled = true; OppsDecision[0].enabled = true; scoreComp.oppsScore += 1; scoreComp.backgroundSlider.value += winPercent; if (BestOfThree) { BestOf -= 1; oppsCounter += 1; } } 
                else if (oppNumChoice == 1) { YourDecision[0].enabled = true; OppsDecision[1].enabled = true; scoreComp.yourScore += 1; scoreComp.backgroundSlider.value -= winPercent; if (BestOfThree) { BestOf -= 1; yourCounter += 1; } }
                break;
        }

        if (displayWinner) { 
            if (BestOfThree && BestOf <= 0) 
            {
                if (yourCounter > oppsCounter) 
                {
                    winningCanvas.enabled = true;
                    winnerName.text = scoreComp.YourName.text;
                }else
                {
                    winningCanvas.enabled = true;
                    winnerName.text = "Bot";
                }
            }

            if (BestOfFive && BestOf <= 0)
            {
                if (yourCounter > oppsCounter)
                {
                    winningCanvas.enabled = true;
                    winnerName.text = scoreComp.YourName.text;
                }
                else
                {
                    winningCanvas.enabled = true;
                    winnerName.text = "Bot";
                }
            }

            if (!BestOfFive && !BestOfThree) 
            {
                if (scoreComp.backgroundSlider.value <= 1)
                {
                    winningCanvas.enabled = true;
                    winnerName.text = "Bot";
                }
                else if (scoreComp.backgroundSlider.value >= 99)
                {
                    winningCanvas.enabled = true;
                    winnerName.text = scoreComp.YourName.text;
                }
            }
        }

        StartCoroutine(ResetGame());
    }

    public void OnGameModeSelect()
    {
        if (!BestOfFive)
        {
            winPercent = 17f;
        }else if (BestOfFive)
        {
            winPercent = 10f;
        }
    }

    public void OnBestOfThree()
    {
        BestOfThree = true;
        BestOf = 3;
    }

    public void OnBestOfFivee()
    {
        BestOfFive = true;
        BestOf = 5;
    }

    private void YourChoice()
    {
        switch (checkButtons.buttName)
        {
            case "RockButt":
                yourNumChoice = 0;
                YourHands[0].enabled = true;
                break;
            case "PaperButt":
                yourNumChoice = 1;
                YourHands[1].enabled = true;
                break;
            case "ScissorsButt":
                yourNumChoice = 2;
                YourHands[2].enabled = true;
                break;
        }
    }

    private void OppsChoice()
    {
        switch (oppNumChoice)
        {
            case 0:
                OppsHands[0].enabled = true;
                break;
            case 1:
                OppsHands[1].enabled = true;
                break;
            case 2:
                OppsHands[2].enabled = true;
                break;
        }
    }

    public IEnumerator DecideWinner()
    {
        yield return new WaitForSeconds(decideWinNum);

        GameLogic();
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(onReset);
        resetGame = true;
        
        for(int i=0; i<YourHands.Length; i++)
        {
            YourHands[i].enabled = false;
            OppsHands[i].enabled = false;
        }

        for (int x = 0; x < YourDecision.Length; x++)
        {
            YourDecision[x].enabled = false;
            OppsDecision[x].enabled = false;
        }

        if (draw.enabled) { draw.enabled = false; }

        gameObject.SetActive(false);
        
    }

    public IEnumerator OnDecidingScreenStart()
    {
        oppNumChoice = Random.Range(0, 2);

        yield return new WaitForSeconds(onScreen);
        
        YourChoice();
        OppsChoice();
    }

    public void OnGameReset()
    {
        winningCanvas.enabled = false;
        displayWinner = false;
        BestOfThree = false;
        BestOfFive = false;
        yourCounter = 0;
        oppsCounter = 0;

        scoreComp.backgroundSlider.value = 50;
        scoreComp.yourScore = 0;
        scoreComp.oppsScore = 0;
        scoreComp.PlayerName.text = " ";
    }
}
