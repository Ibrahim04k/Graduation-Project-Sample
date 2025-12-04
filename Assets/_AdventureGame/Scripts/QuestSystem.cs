using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour {


    public static QuestSystem Instance { get; private set; }


    public event EventHandler OnQuestListChanged;
    public event EventHandler<OnQuestEventArgs> OnQuestAdded;
    public event EventHandler<OnQuestEventArgs> OnQuestCompleted;
    public class OnQuestEventArgs : EventArgs {
        public QuestSO questSO;
    }



    private List<QuestSO> activeQuestList;
    private List<QuestSO> completedQuestList;


    private void Awake() {
        Instance = this;

        activeQuestList = new List<QuestSO>();
        completedQuestList = new List<QuestSO>();
    }

    public bool HasQuest(QuestSO questSO) {
        return activeQuestList.Contains(questSO);
    }

    public void AddQuest(QuestSO questSO) {
        activeQuestList.Add(questSO);

        OnQuestListChanged?.Invoke(this, EventArgs.Empty);
        OnQuestAdded?.Invoke(this, new OnQuestEventArgs {
             questSO = questSO
        });
    }

    public void CompleteQuest(QuestSO questSO) {
        activeQuestList.Remove(questSO);
        completedQuestList.Add(questSO);

        OnQuestListChanged?.Invoke(this, EventArgs.Empty);
        OnQuestCompleted?.Invoke(this, new OnQuestEventArgs {
             questSO = questSO
        });
    }

    public List<QuestSO> GetActiveQuestList() {
        return activeQuestList;
    }

    public bool HasCompletedQuest(QuestSO questSO) {
        return completedQuestList.Contains(questSO);
    }


}