using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckButtons : MonoBehaviour
{
    public DecisionScreenComp decisionScreen;
    public CountdownComp countdownComp;

    public Button rockButt;
    public Button paperButt;
    public Button scissorsButt;


    public string buttName = "";

    // Start is called before the first frame update
    void Start()
    {
        decisionScreen.GetComponent<DecisionScreenComp>();
        countdownComp.GetComponent<CountdownComp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (decisionScreen.resetGame)
        {
            buttName = "";
            countdownComp.timer = 4f;
            decisionScreen.resetGame = false;
        }
    }

    public void OnButtonSelected()
    {
        buttName = EventSystem.current.currentSelectedGameObject.name;

        if (buttName != "")
        {
            if (!countdownComp.startTimer)
            {
                countdownComp.startTimer = true;
            }
            //Debug.Log(buttName);
        }
    }

}
