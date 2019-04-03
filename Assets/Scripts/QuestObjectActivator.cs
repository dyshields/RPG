using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    // Purpose of this script is to activate / deactivate certain game objects depending on the quest

    public GameObject objectToActivate;
    public string questToCheck;
    public bool activeIfComplete;
    private bool initialCheckDone;


    void Start()
    {
        
    }

    //  Quest check should be done on Start, but it's possible that this script loads before the dependent Game Manager and Quest Manager scripts.
    void Update()
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance.CheckIfComplete(questToCheck))
            objectToActivate.SetActive(activeIfComplete);
        {

        }
    }
}
