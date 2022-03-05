using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuComp : MonoBehaviour
{
    public Canvas playerConnection;
    public Canvas SelectionMode;

    private string buttName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonSelected()
    {
        buttName = EventSystem.current.currentSelectedGameObject.name;

        if (buttName != "")
        {
            if (buttName == "VsBotButt") 
            {
                SelectionMode.enabled = true;
            }else if (buttName == "VsFriendButt")
            {
                playerConnection.enabled = true;
            }

            //Debug.Log(buttName);
        }
    }
}
