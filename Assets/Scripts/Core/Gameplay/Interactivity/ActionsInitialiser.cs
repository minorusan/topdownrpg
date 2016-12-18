using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Core.Inventory;
using Core.Utilities;


namespace Core.Gameplay.Interactivity
{
	public static class ActionsInitialiser
	{
		private static Dictionary<string, ActionBase> _actions = new Dictionary<string, ActionBase> ();

		public static ActionBase GetActionByID (string actionID)
		{
			return _actions [actionID];
		}

		#region Configuration

		static ActionsInitialiser ()
		{
			InitialiseActions ();
		}

		private static void InitialiseActions ()
		{
			InitOpenDoorAction ();
		}

		private static void InitOpenDoorAction ()
		{
			ActionRequirement openActionRequirement = (GameObject owner) =>
			{
				var doorway = owner.GetComponent <LockedDoorway> ();

				if (PlayerInventory.Instance.GetItems ().Any (item => item.ItemID == doorway.ItemToUnlock))
				{
					return true;
				}
				return false;
			};

			InteractiveAction action = (GameObject obj) =>
			{
				var doorway = obj.GetComponent <LockedDoorway> ();
				PlayerInventory.Instance.RemoveItemFromInventory (doorway.ItemToUnlock);
				ProcessBarController.StartProcessWithCompletion (3f, doorway.Action.ActionImage, () =>
				                                                 doorway.gameObject.SetActive (false), Color.yellow);
			};

			var openDoorAction = new ActionBase (LockedDoorway.kOpenDoorActionId, openActionRequirement, action);
			_actions.Add (LockedDoorway.kOpenDoorActionId, openDoorAction);
		}

		#endregion
	}
}

