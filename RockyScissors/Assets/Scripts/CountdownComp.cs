using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownComp : MonoBehaviour
{
    public TMP_Text countdownTimer;
    public Canvas decidingScreen;
    public DecisionScreenComp decisionScreenComp;

    [HideInInspector] public bool startTimer = false;
    [HideInInspector] public float timer = 4f;
    [HideInInspector] public int seconds;

    private float onEnd = 1f;

    // Start is called before the first frame update
    void Start()
    {
        decisionScreenComp.GetComponent<DecisionScreenComp>();
        countdownTimer.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (startTimer) 
        {

            timer -= Time.deltaTime;
            seconds = (int)(timer % 60);

            countdownTimer.text = seconds.ToString();

            if (seconds <= 0)
            {
                countdownTimer.text = "GO";
                startTimer = false;

                StartCoroutine(waitEnd());
            }
        }
    }

    IEnumerator waitEnd()
    {
        yield return new WaitForSeconds(onEnd);

        countdownTimer.text = "";
        decidingScreen.gameObject.SetActive(true);
        decisionScreenComp.displayWinner = true;

        StartCoroutine(decisionScreenComp.OnDecidingScreenStart());
        StartCoroutine(decisionScreenComp.DecideWinner());
    }
}
