﻿using UnityEngine;
using Core.Characters.Player;


namespace Core.Gameplay.Interactivity
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class DialogTrigger : MonoBehaviour
	{
		private ActionBase _dialogueAction;

		public string DialogueID;

		void Start ()
		{
			_dialogueAction = ActionsInitialiser.GetActionByID ("action.id.dialogue");
		}

		private void OnTriggerEnter2D (Collider2D trigger)
		{
			if (trigger.tag == PlayerBehaviour.kPlayerTag && trigger.isTrigger)
			{
				ActionPerformer.Instance.SetAction (_dialogueAction, gameObject);
			}
		}

		private void OnTriggerExit2D (Collider2D trigger)
		{
			if (trigger.tag == PlayerBehaviour.kPlayerTag)
			{
				ActionPerformer.Instance.SetAction (null);
			}
		}
	}
}

