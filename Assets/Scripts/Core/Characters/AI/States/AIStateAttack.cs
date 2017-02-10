using System;
using System.Linq;
using UnityEngine;

using Core.Map;
using Core.Characters.Player;
using Core.Map.Pathfinding;
using Core.Characters.Player.Demand;
using Core.Interactivity.AI;
using Random = UnityEngine.Random;


namespace Core.Characters.AI
{
	public class AIStateAttack:AIStateBase
	{
		private Node _currentDestination;
		private Node _previousDestination;
		private Core.Characters.Player.PlayerBehaviour _player;
	    private GuardBrains _guardBrains;
		private bool _attacks;
		private AudioClip _sound;


		public AIStateAttack(ArtificialIntelligence brains) : base(brains)
		{
			State = EAIState.Attack;
		    _guardBrains = (GuardBrains) brains;
            _guardBrains.RanAway += GuardBrainsOnRanAway;
		}

	    private void GuardBrainsOnRanAway()
	    {
	        _pendingState = EAIState.Alert;
	        _currentCondition = AIStateCondition.Done;
	    }

	    public override void OnLeave()
		{
            _masterBrain.MovableObject.MovementSpeed *= 0.5f;
            if (PlayerBehaviour.CurrentPlayer != null)
			{
				PlayerQuirks.Attacked = false;
			}
		}

		public override void OnEnter()
		{
			base.OnEnter();
			PlayerQuirks.Attacked = true;
            var _guardBrains = (GuardBrains)_masterBrain;
            _masterBrain.StatusText.text = _guardBrains.AttackStrings[Random.Range(0, _guardBrains.WanderingStrings.Length)];
            _sound = ((GuardBrains)_masterBrain).AngerSound;
			AudioSource.PlayClipAtPoint(_sound, _masterBrain.transform.position, 1f);
			_player = PlayerBehaviour.CurrentPlayer;
		    _masterBrain.MovableObject.MovementSpeed *= 2f;
			_player.GetComponent<StressAffector>().DemandTickTime *= 0.8f;
		}

		public override void UpdateState()
		{
			base.UpdateState();
			if(IsPlayerReachable())
			{
				MoveToPlayer();
			}
			else
			{
				_currentCondition = AIStateCondition.Done;
				_pendingState = EAIState.Wandering;
			}
		}

		#region PRIVATE

		private void MoveToPlayer()
		{
			var suitableAttackPosition = _map.GetNodeByPosition(_player.transform.position);
		    if (suitableAttackPosition.CurrentCellType == ECellType.Blocked)
		    {
		        suitableAttackPosition =
		            _map.GetNeighbours(suitableAttackPosition).First(i => i.CurrentCellType == ECellType.Walkable);
		    } 
			_masterBrain.MovableObject.BeginMovementByPath(Pathfinder.FindPathToDestination(
				_map,
				_masterBrain.MovableObject.CurrentNode.GridPosition,
				suitableAttackPosition.GridPosition));
		}

		private bool IsPlayerReachable()
		{
			return _map.GetNodeByPosition(_player.transform.position) != null && Vector3.Distance(_masterBrain.transform.position, _player.transform.position) < 20f;
		} 

		#endregion
	}
}