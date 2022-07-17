using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using TMPro;

public class QuestNotificationController : MonoBehaviour
{
    public GameObject quest;
    public TextMeshProUGUI textComponent;

    void Start() {
        quest.gameObject.SetActive(false);
    }

    public void showQuest (string questText) {
        quest.gameObject.SetActive(true);
        textComponent.text = questText;
    }

    public void hideQuest () {
        quest.gameObject.SetActive(false);
    }
}
