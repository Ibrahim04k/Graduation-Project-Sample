using UnityEngine;

public class AdventureNPC : MonoBehaviour, IInteractable, IDialogueCharacter {


    public enum NPCType {
        FindCheese,
        GreatDay,
    }

    [SerializeField] private GameObject dialogueCameraGameObject;
    [SerializeField] private QuestSO questSO;
    [SerializeField] private NPCType npcType;


    private bool lookAtPlayer;


    private void Update() {
        if (lookAtPlayer) {
            Vector3 lookDir = (AdventurePlayer.Instance.transform.position - transform.position).normalized;
            lookDir.y = 0;
            float rotationSpeed = 1000f;
            transform.forward = Vector3.Slerp(transform.forward, lookDir, Time.deltaTime * rotationSpeed);
        }
    }

    public string GetInteractText() {
        return "Talk";
    }

    public void Interact(AdventurePlayer adventurePlayer) {
        switch (npcType) {
            case NPCType.GreatDay:
                lookAtPlayer = true;
                DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
                    DialogueSystem.DialogueSingle[] {
                        new DialogueSystem.DialogueSingle {
                                character = 0,
                                text = "What a great day!",
                        },
                        new DialogueSystem.DialogueSingle {
                                character = 0,
                                text = "Are you enjoying today?",
                        },
                        new DialogueSystem.DialogueSingle {
                                character = 1,
                                text = "Yeah sure",
                        },
                        new DialogueSystem.DialogueSingle {
                                character = 0,
                                text = "Well keep enjoying it!",
                        },
                    },
                    () => {
                        lookAtPlayer = false;
                    }
                );
                break;
            case NPCType.FindCheese:
                lookAtPlayer = true;
                if (QuestSystem.Instance.HasCompletedQuest(questSO)) {
                    DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
                        DialogueSystem.DialogueSingle[] {
                                new DialogueSystem.DialogueSingle {
                                     character = 0,
                                     text = "Thanks for your help in finding my cheese!",
                                },
                        },
                        () => {
                            lookAtPlayer = false;
                        }
                    );
                    break;
                }

                if (QuestSystem.Instance.HasQuest(questSO)) {
                    // Quest already started
                    // Did player complete it?
                    if (Inventory.Instance.HasItem(AdventureGameAssets.Instance.items.cheese)) {
                        // Success!
                        DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
                            DialogueSystem.DialogueSingle[] {
                                new DialogueSystem.DialogueSingle {
                                     character = 1,
                                     text = "I found your cheese!",
                                },
                                new DialogueSystem.DialogueSingle{
                                     character = 0,
                                     text = "Oh wow thanks so much! I was really hungry!",
                                },
                            },
                            () => {
                                Inventory.Instance.RemoveItem(AdventureGameAssets.Instance.items.cheese);
                                QuestSystem.Instance.CompleteQuest(questSO);
                                lookAtPlayer = false;
                            }
                        );
                    } else {
                        DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
                            DialogueSystem.DialogueSingle[] {
                                new DialogueSystem.DialogueSingle {
                                     character = 0,
                                     text = "Have you found my cheese?",
                                },
                                new DialogueSystem.DialogueSingle{
                                     character = 1,
                                     text = "Not yet, I'll be back...",
                                },
                            },
                            () => {
                                lookAtPlayer = false;
                            }
                        );
                    }
                    break;
                }

                // Doesn't have this quest yet, start it
                DialogueSystem.Instance.StartDialogue(this, adventurePlayer, new
                    DialogueSystem.DialogueSingle[] {
                        new DialogueSystem.DialogueSingle {
                                character = 0,
                                text = "Hi there!",
                        },
                        new DialogueSystem.DialogueSingle{
                                character = 0,
                                text = "I need your help!",
                        },
                        new DialogueSystem.DialogueSingle{
                                character = 0,
                                text = "My cheese is missing!",
                        },
                        new DialogueSystem.DialogueSingle{
                                character = 1,
                                text = "What kind of cheese?",
                        },
                        new DialogueSystem.DialogueSingle{
                                character = 0,
                                text = "It's a cheese wheel, you can't miss it",
                        },
                        new DialogueSystem.DialogueSingle{
                                character = 1,
                                text = "Ok I'll be on the look out for some cheese",
                        },
                        },
                    () => {
                        QuestSystem.Instance.AddQuest(questSO);
                        lookAtPlayer = false;
                    }
                );
                break;
        }
    }

    public Transform GetTransform() => transform;

    public GameObject GetVirtualCameraGameObject() => dialogueCameraGameObject;


}