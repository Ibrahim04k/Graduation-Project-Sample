using TMPro;
using UnityEngine;

public class InteractableUI : MonoBehaviour {



    [SerializeField] private TextMeshProUGUI interactTextMesh;


    private void Start() {
        AdventurePlayer.Instance.OnSelectedInteractionChanged += AdventurePlayer_OnSelectedInteractionChanged;

        Hide();
    }

    private void AdventurePlayer_OnSelectedInteractionChanged(object sender, AdventurePlayer.OnSelectedInteractionChangedEventArgs e) {
        if (e.interactable == null) {
            Hide();
        } else {
            Show(e.interactable);
        }
    }

    private void Show(IInteractable interactable) {
        interactTextMesh.text = interactable.GetInteractText();
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}