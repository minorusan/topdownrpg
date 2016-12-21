using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters.Player;


namespace Core.Gameplay.Interactivity
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class Hideout : MonoBehaviour
	{
		public const string kHideAction = "action.id.hide";
		private ActionBase _action;

		public ActionBase Action
		{
			get
			{
				return _action;
			}
		}
		// Use this for initialization
		void Start ()
		{
			_action = ActionsInitialiser.GetActionByID (kHideAction);
		}

		// Update is called once per frame
		void Update ()
		{

		}

		private void OnTriggerEnter2D (Collider2D trigger)
		{
			if (trigger.tag == PlayerBehaviour.kPlayerTag)
			{
				ActionPerformer.Instance.SetAction (_action, gameObject);
			}
		}

		private void OnTriggerExit2D (Collider2D trigger)
		{
			if (trigger.tag == PlayerBehaviour.kPlayerTag)
			{
				ActionPerformer.Instance.SetAction (null);
				PlayerBehaviour.CurrentPlayer.Renderer.enabled = true;
				PlayerBehaviour.CurrentPlayer.Hidden = false;
			}
		}
	}
}

