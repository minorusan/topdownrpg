using UnityEngine;

using Core.Pathfinding;
using Core.Map;
using Core.Map.Pathfinding;
using Core.Characters.Player;
using UnityEngine.UI;


namespace Core.Characters.AI
{
    public class AIStateWandering:AIStateBase
    {
        private float _previousMoveSpeed;
        private float _timeIdle = 3f;
        private float _searchDistance;
        private float _suspention;
        private Image _suspentionBar;
        private Core.Characters.Player.Player _player;

        public AIStateWandering(ArtificialIntelligence brains, float searchDistance, Image suspentionBar)
            : base(brains)
        {
            _searchDistance = searchDistance;
            State = EAIState.Wandering;
            _player = GameObject.FindObjectOfType<Core.Characters.Player.Player>();
            _suspentionBar = suspentionBar;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _suspention = 0f;
            _masterBrain.StatusText.text = "ZZZZzz....";
            _previousMoveSpeed = _masterBrain.MovableObject.MovementSpeed;
            _masterBrain.MovableObject.MovementSpeed *= 0.4f;

            _masterBrain.MovableObject.DebugColor = Color.green;
        }

        public override void OnLeave()
        {
            _masterBrain.MovableObject.MovementSpeed = _previousMoveSpeed;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (Vector3.Distance(_masterBrain.transform.position, _player.transform.position) < _searchDistance)
            {
                _currentCondition = AIStateCondition.Done;
                _pendingState = EAIState.Attack;
            }

            if (_suspention > 1)
            {
                _currentCondition = AIStateCondition.Done;
                _pendingState = EAIState.Alert;
            }

            if (_timeIdle <= 0)
            {
                FindNewpath();
                _timeIdle = 3f;

            }
            else
            {
                CheckNoise();
                _timeIdle -= Time.deltaTime;
                _suspention -= _suspention > 0.1f ? 0.01f : 0;
            }
        }

        private void CheckNoise()
        {
            var noise = _player.Noise;
            _suspention += noise;
            _suspentionBar.fillAmount = _suspention;
        }

        private void FindNewpath()
        {
            var possibleLocations = _map.GetNeighbours(_masterBrain.MovableObject.CurrentNode);
            if (possibleLocations != null && possibleLocations.Length > 1)
            {
                var destination = possibleLocations[Random.Range(0, possibleLocations.Length - 1)];
                if (destination != null)
                {
                    _masterBrain.MovableObject.BeginMovementByPath(Pathfinder.FindPathToDestination(
                            _map,
                            _masterBrain.MovableObject.CurrentNode.GridPosition,
                            destination.GridPosition));
                    
                }
            }
        }
    }
}

