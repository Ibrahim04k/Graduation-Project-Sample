using TMPro;
using UnityEngine;

public class DialogueSystemUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI textMesh;


    private void Start() {
        DialogueSystem.Instance.OnDialogueSingleChanged += DialogueSystem_OnDialogueSingleChanged;

        Hide();
    }

    private void DialogueSystem_OnDialogueSingleChanged(object sender, DialogueSystem.OnDialogueSingleChangedEventArgs e) {
        if (e.dialogueSingle != null) {
            Show(e.dialogueSingle.text);
        } else {
            Hide();
        }
    }

    private void Show(string text) {
        textMesh.text = text;
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}