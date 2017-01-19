using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Utilities.UI;
using Core.Inventory;


namespace Core.Gameplay.Interactivity
{
	public delegate void QuestCompletedHandler (Quest sender);

	public class Quest
	{
		private string _questId;
		private bool _completed;
		private string _description;
		private readonly ActionRequirement _requirement;
		private readonly InteractiveAction _completion;

		public event QuestCompletedHandler QuestCompleted;

		public bool Completed
		{
			get
			{
				return _completed;
			}

			set
			{
				_completed = value;
				if (_completed && QuestCompleted != null)
				{
					QuestCompleted (this);
				}
			}
		}

		public string ID
		{
			get
			{
				return _questId;
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
		}

		public void PerformAction (GameObject obj)
		{
			_completion (obj);
		}

		public bool IsRequirementSatisfied (GameObject owner)
		{
			return _requirement (owner);
		}

		public Quest (string questId, string description, ActionRequirement requirement, InteractiveAction completion)
		{
			_questId = questId;
			_description = description;
			_requirement = requirement;
			_completion = completion;
		}
	}

	public static class QuestStorage
	{
		private static Dictionary<string, Quest> _quests = new Dictionary<string, Quest> ();

		static QuestStorage ()
		{
			InitQuests ();
		}

		private static void InitQuests ()
		{
			ActionRequirement req = (GameObject owner) =>
			{
				var containsitem = PlayerInventory.Instance.GetItems ().Count (i => i.ItemID == "genericitem.id.toybear") > 0;
				return containsitem;
			};

			InteractiveAction action = (GameObject obj) =>
			{
				var dialogue = DialogueStorage.GetDialogueByID ("dialogue.id.kidend");
				PlayerInventory.Instance.RemoveItemFromInventory ("genericitem.id.toybear");

				DialogueDisplayer.ShowDialogue (dialogue);
			};

			var gettovault = new Quest ("quest.id.gettovault", "Find your way to vault", null, null);
            var getbear = new Quest("quest.id.getbear", "Find some toy for a kid", req, action);
            var getNails = new Quest("quest.id.getnails", "Find some toy for a kid", req, action);

            _quests.Add (gettovault.ID, gettovault);
            _quests.Add(getbear.ID, getbear);
        }

		public static Quest GetQuestById (string questID)
		{
			return _quests [questID];
		}

		public static List<Quest> GetQuests ()
		{
			return _quests.Values.ToList ();
		}
	}
}

