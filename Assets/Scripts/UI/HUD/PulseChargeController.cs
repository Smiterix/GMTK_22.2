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
    float fullChargeValue = 15f;
    float emptyChargeValue = 0f;

    void Start ()
    {
        // Progress Bar Values
        charge1.currentPercent = fullChargeValue;
        charge2.currentPercent = fullChargeValue;
        charge3.currentPercent = fullChargeValue;
        charge4.currentPercent = fullChargeValue;
        charge5.currentPercent = fullChargeValue;

        // Text
        allCharges.GetComponent<TextMeshProUGUI>().text = initAllCharges.ToString();
        setTextChargesLeft(initAllCharges);
    }

    public void chargeUsed (int chargesLeft) {
        if (chargesLeft < 5) charge5.currentPercent = emptyChargeValue;
        else charge5.currentPercent = fullChargeValue;

        if (chargesLeft < 4) charge4.currentPercent = emptyChargeValue;
        else charge4.currentPercent = fullChargeValue;
        
        if (chargesLeft < 3) charge3.currentPercent = emptyChargeValue;
        else charge3.currentPercent = fullChargeValue;

        if (chargesLeft < 2) charge2.currentPercent = emptyChargeValue;
        else charge2.currentPercent = fullChargeValue;

        if (chargesLeft < 1) charge1.currentPercent = emptyChargeValue;
        else charge1.currentPercent = fullChargeValue;

        setTextChargesLeft(chargesLeft);
    }

    public void setTextChargesLeft (int charges) {
        chargesLeft.GetComponent<TextMeshProUGUI>().text = charges.ToString();
    }

    public int getTextChargesLeft () {
        return int.Parse(chargesLeft.GetComponent<TextMeshProUGUI>().text);
    }
}
