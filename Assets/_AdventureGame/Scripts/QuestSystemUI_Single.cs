using TMPro;
using UnityEngine;

public class QuestSystemUI_Single : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI textMesh;


    public void Setup(QuestSO questSO) {
        textMesh.text = questSO.text;
    }



}