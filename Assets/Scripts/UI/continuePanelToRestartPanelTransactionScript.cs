using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continuePanelToRestartPanelTransactionScript : MonoBehaviour
{
    public GameObject egg;


    public void RestartPanelActivity()
    {
        egg.GetComponent<EggController>().RestartPanelActivity();
    }
}
