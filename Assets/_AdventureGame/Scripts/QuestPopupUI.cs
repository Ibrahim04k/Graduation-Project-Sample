using TMPro;
using UnityEngine;

public class QuestPopupUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI nameTextMesh;


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        QuestSystem.Instance.OnQuestAdded += QuestSystem_OnQuestAdded;
        QuestSystem.Instance.OnQuestCompleted += QuestSystem_OnQuestCompleted;

        animator.Update(100f);
    }

    private void QuestSystem_OnQuestCompleted(object sender, QuestSystem.OnQuestEventArgs e) {
        titleTextMesh.text = "QUEST COMPLETED";
        nameTextMesh.text = e.questSO.text;
        PlaySpawnAnimation();
    }

    private void QuestSystem_OnQuestAdded(object sender, QuestSystem.OnQuestEventArgs e) {
        titleTextMesh.text = "QUEST ADDED";
        nameTextMesh.text = e.questSO.text;
        PlaySpawnAnimation();
    }

    private void PlaySpawnAnimation() {
        animator.SetTrigger("Spawn");
    }


}