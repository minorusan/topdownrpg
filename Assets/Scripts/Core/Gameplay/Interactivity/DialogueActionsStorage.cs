using System;
using System.Collections.Generic;

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
        }

        public static Action GetDialogueCompletion(string dialogueId)
        {
            Action ret;
            _actions.TryGetValue(dialogueId, out ret);
            return ret;
        }
    }
}

