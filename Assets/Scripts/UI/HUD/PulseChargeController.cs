using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using TMPro;

public class PulseChargeController : MonoBehaviour
{
    [Header("Referenzen")]
    public ProgressBar charge1;
    public ProgressBar charge2;
    public ProgressBar charge3;
    public ProgressBar charge4;
    public ProgressBar charge5;
    public GameObject chargesLeft;
    public GameObject allCharges;
    
    [Header("Values")]
    public int initAllCharges = 5;

    void Awake()
    {
        allCharges.GetComponent<TextMeshProUGUI>().text = initAllCharges.ToString();
        setChargesLeft(initAllCharges);
    }

    public void chargeUsed (int chargesUsed) {
        int chargesLeft = getChargesLeft() - chargesUsed;
        if (chargesLeft < 0) return;
        setChargesLeft(chargesLeft);
    }

    public void setChargesLeft (int charges) {
        chargesLeft.GetComponent<TextMeshProUGUI>().text = charges.ToString();
    }

    public int getChargesLeft () {
        return int.Parse(chargesLeft.GetComponent<TextMeshProUGUI>().text);
    }
}
