using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class HealthbarController : MonoBehaviour
{
    public ProgressBar pbar;

    void Start() {
        setHealth(pbar.maxValue);
    }
    
    public float getCurrentHealth () {
        return pbar.currentPercent;
    }

    public void setHealth (float health) {
        pbar.currentPercent = health;
    }
}
