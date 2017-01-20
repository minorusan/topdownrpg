using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Core.Inventory;
using Core.Utilities;
using Core.Characters.Player;
using Core.Utilities.UI;
using System;
using UI;
using Core.Map;

namespace Core.Gameplay.Interactivity
{
	public static class ActionsInitialiser
	{
		private static Dictionary<string, ActionBase> _actions = new Dictionary<string, ActionBase>();

		public static ActionBase GetActionByID(string actionID)
		{
			return _actions[actionID];
		}

		#region Configuration

		static ActionsInitialiser()
		{
			InitialiseActions();
		}

		private static void InitialiseActions()
		{
			InitOpenDoorAction();
			InitHideAction();
			InitDialogueAction();
            InitVendorAction();
            InitContainerAction();
		}

        private static void InitVendorAction()
        {

            InteractiveAction action = (GameObject obj) =>
            {
                var trigger = obj.GetComponent<VendorTrigger>();

                var vendor = VendorsStorage.GetVendor(trigger.VendorID);
                TradeController.ShowTradeForVendor(vendor.Vendorid);
            };

            var tradeAction = new ActionBase("action.id.trade", (GameObject owner)=> { return true; }, action);
            _actions.Add("action.id.trade", tradeAction);
        }

        private static void InitContainerAction()
        {

            InteractiveAction action = (GameObject obj) =>
            {
                ProcessBarController.StartProcessWithCompletion(3f, Resources.Load<Sprite>("Sprites/Actions/action.id.container"), () =>
                                                                 {
                                                                     var container = obj.GetComponent<Container>();
                                                                     ContainerUI.ShowForContainer(container);
                                                                 }, Color.green);
                
            };

            var containerAction = new ActionBase("action.id.container", (GameObject owner) => { return true; }, action);
            _actions.Add("action.id.container", containerAction);
        }

        private static void InitDialogueAction()
		{
			InteractiveAction action = (GameObject obj) =>
			{
				var trigger = obj.GetComponent <DialogTrigger>();

				var dialogue = DialogueStorage.GetDialogueByID(trigger.DialogueID);
				DialogueDisplayer.ShowDialogue(dialogue);
			};

			var dialogueAction = new ActionBase("action.id.dialogue", (GameObject owner) => { return true; }, action);
			_actions.Add("action.id.dialogue", dialogueAction);
		}

		private static void InitHideAction()
		{
			ActionRequirement openActionRequirement = (GameObject owner) =>
			{
				return !PlayerQuirks.Attacked;
			};

			InteractiveAction action = (GameObject obj) =>
			{
				var doorway = obj.GetComponent <Hideout>();

				ProcessBarController.StartProcessWithCompletion(1f, doorway.Action.ActionImage, () =>
				{
					PlayerBehaviour.CurrentPlayer.Renderer.enabled = false;
					PlayerQuirks.Hidden = true;
				}, Color.grey);
			};

			var openDoorAction = new ActionBase(Hideout.kHideAction, openActionRequirement, action);
			_actions.Add(Hideout.kHideAction, openDoorAction);
		}

		private static void InitOpenDoorAction()
		{
			ActionRequirement openActionRequirement = (GameObject owner) =>
			{
				var doorway = owner.GetComponent <LockedDoorway>();

				if(PlayerInventory.Instance.GetItems().Any(item => item.ItemID == doorway.ItemToUnlock))
				{
					return true;
				}
				return false;
			};

			InteractiveAction action = (GameObject obj) =>
			{
				var doorway = obj.GetComponent <LockedDoorway>();
				PlayerInventory.Instance.RemoveItemFromInventory(doorway.ItemToUnlock);
				ProcessBarController.StartProcessWithCompletion(3f, doorway.Action.ActionImage, () =>
				                                                 doorway.gameObject.SetActive(false), Color.yellow);
			};

			var openDoorAction = new ActionBase(LockedDoorway.kOpenDoorActionId, openActionRequirement, action);
			_actions.Add(LockedDoorway.kOpenDoorActionId, openDoorAction);
		}

		#endregion
	}
}

