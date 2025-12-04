using System;
using UnityEngine;
using static DialogueSystem;

public class DialogueSystem : MonoBehaviour {


    public static DialogueSystem Instance { get; private set; }


    public event EventHandler<OnDialogueSingleChangedEventArgs> OnDialogueSingleChanged;
    public class OnDialogueSingleChangedEventArgs : EventArgs {
        public DialogueSingle dialogueSingle;
    }


    private IDialogueCharacter dialogueCharacter0;
    private IDialogueCharacter dialogueCharacter1;
    private DialogueSingle[] dialogueSingleArray;
    private int selectedDialogue;
    private Action onDialogueCompleted;


    public class DialogueSingle {
        public int character;
        public string text;
    }


    private void Awake() {
        Instance = this;
    }

    public void StartDialogue(IDialogueCharacter dialogueCharacter0, IDialogueCharacter dialogueCharacter1, DialogueSingle[] dialogueSingleArray, Action onDialogueCompleted) {
        this.dialogueCharacter0 = dialogueCharacter0;
        this.dialogueCharacter1 = dialogueCharacter1;
        this.dialogueSingleArray = dialogueSingleArray;
        this.onDialogueCompleted = onDialogueCompleted;

        selectedDialogue = -1;
        Next();
    }


    public void Next() {
        dialogueCharacter0.GetVirtualCameraGameObject().SetActive(false);
        dialogueCharacter1.GetVirtualCameraGameObject().SetActive(false);

        selectedDialogue++;
        if (selectedDialogue < dialogueSingleArray.Length) {
            DialogueSingle dialogueSingle = dialogueSingleArray[selectedDialogue];

            if (dialogueSingle.character == 0) {
                dialogueCharacter0.GetVirtualCameraGameObject().SetActive(true);
            }
            if (dialogueSingle.character == 1) {
                dialogueCharacter1.GetVirtualCameraGameObject().SetActive(true);
            }

            OnDialogueSingleChanged?.Invoke(this, new OnDialogueSingleChangedEventArgs {
                dialogueSingle = dialogueSingle,
            });
        } else {
            // No more dialogue
            onDialogueCompleted();
            OnDialogueSingleChanged?.Invoke(this, new OnDialogueSingleChangedEventArgs {
                dialogueSingle = null,
            });
        }
    }

    public bool IsDialogueActive() {
        return dialogueSingleArray != null && selectedDialogue < dialogueSingleArray.Length;
    }
    
}