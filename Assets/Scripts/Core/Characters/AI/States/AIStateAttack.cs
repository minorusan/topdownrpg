using UnityEngine;
using UnityEngine.EventSystems;

using Core.Pathfinding;
using System.Linq;

using System.Collections.Generic;
using Core.Map;
using Core.Characters.Player;
using Core.Map.Pathfinding;


namespace Core.Characters.AI
{
    public class AIStateAttack:AIStateBase
    {
        private Node _currentDestination;
        private Node _previousDestination;
        private Core.Characters.Player.PlayerBehaviour _player;
        private bool _attacks;

        public AIStateAttack(ArtificialIntelligence brains)
            : base(brains)
        {
            State = EAIState.Attack;
        }

        public override void OnLeave()
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _masterBrain.StatusText.text = "Lemme get ya!1";


            _player = GameObject.FindObjectOfType<Core.Characters.Player.PlayerBehaviour>();
            _player.GetComponent<StressAffector>().NervesTickTime *= 0.8f;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (IsPlayerReachable())
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
            _masterBrain.MovableObject.BeginMovementByPath(Pathfinder.FindPathToDestination(
                    _map,
                    _masterBrain.MovableObject.CurrentNode.GridPosition,
                    suitableAttackPosition.GridPosition));
        }

        private bool IsPlayerReachable()
        {
            return _map.GetNodeByPosition(_player.transform.position) != null;
        }

        #endregion
    }
}