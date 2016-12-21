﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters.Player;
using UnityEngine.UI;


namespace Core.Gameplay.Interactivity
{
	[RequireComponent (typeof(DialogTrigger))]
	public class QuestNPC : MonoBehaviour
	{
		private Quest _quest;
		public string QuestId;

		private void Start ()
		{
			_quest = QuestStorage.GetQuestById (QuestId);
		}

		private void OnTriggerEnter2D (Collider2D trigger)
		{
			if (trigger.tag == PlayerBehaviour.kPlayerTag && trigger.isTrigger)
			{
				var satisfied = _quest.IsRequirementSatisfied (gameObject);
				if (!_quest.Completed && satisfied)
				{
					GetComponent<DialogTrigger> ().enabled = false;
					_quest.PerformAction (gameObject);
					_quest.Completed = true;
				}
			}
		}
	}

}
