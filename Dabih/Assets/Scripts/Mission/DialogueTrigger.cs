using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DialogueType
{
    missionText,
    tutorialText,
    popupText
}

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] List<DialogueType> dialoguesToDisplay;
    [SerializeField] bool triggerEnter;
    [TextArea(1, 6)]
    [SerializeField] string missionTextToDisplay;
    [TextArea(1, 6)]
    [SerializeField] string tutorialTextToDisplay;
    [SerializeField] string popupTextToDisplay;

    [SerializeField] GameEvent onMissionTextChanged;
    [SerializeField] GameEvent onTutorialTextChanged;
    [SerializeField] GameEvent onPopupTextChanged;

    public void Use()
    {
        if (triggerEnter)
        {
            return;
        }

        SetDialogue();
    }

    private void SetDialogue()
    {
        foreach (var dialogue in dialoguesToDisplay)
        {
            switch (dialogue)
            {
                case DialogueType.missionText:
                    onMissionTextChanged.Raise(this, missionTextToDisplay);
                    break;
                case DialogueType.tutorialText:
                    onTutorialTextChanged.Raise(this, tutorialTextToDisplay);
                    break;
                case DialogueType.popupText:
                    onPopupTextChanged.Raise(this, popupTextToDisplay);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerEnter)
        {
            return;
        }

        if (other.transform.root.tag == CommonStrings.playerString)
        {
            SetDialogue();
        }
    }
}
