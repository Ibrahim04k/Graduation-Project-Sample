using UnityEngine;

public class ObjectInspectDialogue : MonoBehaviour, IDialogueCharacter, IInteractable {


    [SerializeField] private GameObject dialogueCameraGameObject;


    public string GetInteractText() {
        return "Inspect";
    }

    public Transform GetTransform() => transform;

    public GameObject GetVirtualCameraGameObject() => dialogueCameraGameObject;

    public void Interact(AdventurePlayer adventurePlayer) {
        DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
            DialogueSystem.DialogueSingle[] {
            new DialogueSystem.DialogueSingle {
                    character = 0,
                    text = "It's a fascinating statue",
            },
            new DialogueSystem.DialogueSingle{
                    character = 0,
                    text = "Very well made, lots of detail",
            },
            },
            () => {
            }
        );
    }

}