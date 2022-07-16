using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class HeatbarController : MonoBehaviour
{
    public ProgressBar pbar;

    // Start is called before the first frame update
    void Start()
    {
        setHeat(0f);
    }

    public void setHeat (float heat) {
        pbar.currentPercent = heat;
    }
}
