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
                SwapNPCPosition("ScholarMad", "Scholar");
            });

            _actions.Add("dialogue.id.scholarend", () =>
            {
                var girlposition = GameObject.Find("GirlPosition");
                var girl = GameObject.Find("LostGirl");

                SwapNPCPosition("Picker", "PickerTeach");

                girl.transform.position = girlposition.transform.position;
            });

            _actions.Add("dialogue.id.madscholar", () =>
            {
                SwapNPCPosition("KidBusy", "Kid");
            });
        }

        public static Action GetDialogueCompletion(string dialogueId)
        {
            Action ret;
            _actions.TryGetValue(dialogueId, out ret);
            return ret;
        }

        private static void SwapNPCPosition(string npc, string npcToSwap)
        {
            var npc1 = GameObject.Find(npc);
            var npc2 = GameObject.Find(npcToSwap);
            npc2.transform.position = npc1.transform.position;
            npc2.SetActive(false);
        }
    }
}

