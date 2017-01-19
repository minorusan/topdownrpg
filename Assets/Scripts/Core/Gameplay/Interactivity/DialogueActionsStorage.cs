using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay.Interactivity
{
    public static class DialogueActionsStorage
    {
        private static Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        static DialogueActionsStorage()
        {
            InitDialogueActions();
        }

        private static void InitDialogueActions()
        {
            _actions.Add("dialogue.id.childgroup_one", () =>
            {
                QuestController.StartQuest("quest.id.gettovault");
            });

            _actions.Add("dialogue.id.kidstart", () =>
            {
                QuestController.StartQuest("quest.id.getbear");
            });

            _actions.Add("dialogue.id.scholar", () =>
            {
                QuestController.StartQuest("quest.id.getnails");
            });

            _actions.Add("dialogue.id.kidend", () =>
            {
                var scholarMad = GameObject.Find("ScholarMad");
                var scholar = GameObject.Find("Scholar");
                scholar.transform.position = scholarMad.transform.position;
                scholarMad.SetActive(false);
            });

            _actions.Add("dialogue.id.madscholar", () =>
            {
                var kidbusy = GameObject.Find("KidBusy");
                var kid = GameObject.Find("Kid");
                kid.transform.position = kidbusy.transform.position;
                kidbusy.SetActive(false);
            });
        }

        public static Action GetDialogueCompletion(string dialogueId)
        {
            Action ret;
            _actions.TryGetValue(dialogueId, out ret);
            return ret;
        }
    }
}

