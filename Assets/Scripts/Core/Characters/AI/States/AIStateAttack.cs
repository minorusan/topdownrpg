using UnityEngine;
using UnityEngine.EventSystems;

using Core.Pathfinding;
using System.Linq;

using System.Collections.Generic;
using Core.Map;
using Core.Characters.Player;
using Core.Map.Pathfinding;
using Core.Characters.Player.Demand;


namespace Core.Characters.AI
{
	public class AIStateAttack:AIStateBase
	{
		private Node _currentDestination;
		private Node _previousDestination;
		private Core.Characters.Player.PlayerBehaviour _player;
		private bool _attacks;
		private AudioClip _sound;


		public AIStateAttack (ArtificialIntelligence brains) : base (brains)
		{
			State = EAIState.Attack;
			_sound = Resources.Load <AudioClip> ("Sounds/moan");
		}

		public override void OnLeave ()
		{
			if (PlayerBehaviour.CurrentPlayer != null)
			{
				PlayerBehaviour.CurrentPlayer.Attacked = false;
			}
		}

		public override void OnEnter ()
		{
			base.OnEnter ();
			PlayerBehaviour.CurrentPlayer.Attacked = true;
			_masterBrain.StatusText.text = "Hrrr..";
			AudioSource.PlayClipAtPoint (_sound, _masterBrain.transform.position, 1f);
			_player = PlayerBehaviour.CurrentPlayer;
			_player.GetComponent<StressAffector> ().DemandTickTime *= 0.8f;
		}

		public override void UpdateState ()
		{
			base.UpdateState ();
			if (IsPlayerReachable ())
			{
				MoveToPlayer ();
			}
			else
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Wandering;
			}
		}

		#region PRIVATE

		private void MoveToPlayer ()
		{
			var suitableAttackPosition = _map.GetNodeByPosition (_player.transform.position); 
			_masterBrain.MovableObject.BeginMovementByPath (Pathfinder.FindPathToDestination (
				_map,
				_masterBrain.MovableObject.CurrentNode.GridPosition,
				suitableAttackPosition.GridPosition));
		}

		private bool IsPlayerReachable ()
		{
			return _map.GetNodeByPosition (_player.transform.position) != null;
		}

		#endregion
	}
}