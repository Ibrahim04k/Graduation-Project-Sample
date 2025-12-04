using UnityEngine;

public class QuestSystemUI : MonoBehaviour {


    [SerializeField] private Transform container;
    [SerializeField] private Transform questSingleUIPrefab;


    private void Start() {
        QuestSystem.Instance.OnQuestListChanged += QuestSystem_OnQuestListChanged;

        UpdateVisual();
    }

    private void QuestSystem_OnQuestListChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }

        foreach (QuestSO questSO in QuestSystem.Instance.GetActiveQuestList()) {
            Transform questSingleTransform = Instantiate(questSingleUIPrefab, container);
            questSingleTransform.GetComponent<QuestSystemUI_Single>().Setup(questSO);
        }
    }

}