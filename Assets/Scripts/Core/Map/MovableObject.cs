using UnityEngine;

using System.Linq;
using System.Collections;
using Core.Map;
using Core.Map.Pathfinding;


namespace Core.Map
{
    [RequireComponent(typeof(Animator))]
    public class MovableObject : MonoBehaviour
    {
        #region PRIVATE

        private Path _currentPath = new Path();
        private EMovableObjectState _currentState = EMovableObjectState.Standing;
        private Node _myPosition;
        private Animator _animator;

        #endregion

        public Color DebugColor;
        public float MovementSpeed;
        public MapController Map;

        #region Properties

        public Node CurrentNode
        {
            get
            {
                return _myPosition;
            }
            set
            {
                Debug.Assert(value != null, this.name + " attempted to obtain null position.");
                _myPosition = value;
            }
        }

        public bool ReachedDestination
        {
            get
            {
                return _currentPath.Empty;
            }
        }

        public Path CurrentPath
        {
            get
            {
                return _currentPath;
            }
        }

        public Animator SelfAnimator
        {
            get
            {
                return _animator;
            }
        }

        #endregion

        #region Monobehaviour

        private void Start()
        {
            var maps = FindObjectsOfType<MapController>();
            for (int i = 0; i < maps.Length; i++)
            {
                var playerNode = maps[i].GetNodeByPosition(transform.position);
                if (playerNode != null && (Map == null || Map != maps[i]))
                {
                    Map = maps[i];
                    _myPosition = Map.GetNodeByPosition(transform.position);
                    return;
                }
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_currentPath.Empty)
            {
                if (_currentPath.Nodes[0].CurrentCellType == ECellType.Busy)
                {
                    BeginMovementByPath(Pathfinder.FindPathToDestination(Map, CurrentNode.GridPosition, _currentPath.Nodes.Last().GridPosition));
                    if (!_currentPath.Empty)
                    {
                        MoveToTarget(_currentPath.Nodes[0].Position);
                    }
                    else
                    {
                        ToggleAnimationState(EMovableObjectState.Standing);
                    }
                }
                else
                {
                    MoveToTarget(_currentPath.Nodes[0].Position);
                }

                DrawDebugPath();
            }
        }

        private void OnDisable()
        {
            _currentPath.Nodes.Clear();
        }

        #endregion

        public void BeginMovementByPath(Path path)
        {
            _currentPath.Nodes.Clear();
            _currentPath = path;
            ToggleAnimationState(EMovableObjectState.Walking);
        }

        #region Internal

        private void MoveToTarget(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, MovementSpeed);
            CheckIfDestinationIsReached();
        }

        private void CheckIfDestinationIsReached()
        {
            if (Vector3.Distance(_currentPath.Nodes[0].Position, this.transform.position) < 0.1f)
            {
                _myPosition = _currentPath.Nodes[0];
                _currentPath.Nodes.Remove(_currentPath.Nodes[0]);
            }

            if (_currentPath.Empty)
            {
                ToggleAnimationState(EMovableObjectState.Standing);
            }
        }

        private void DrawDebugPath()
        {
            if (_currentPath.Empty)
            {
                return;
            }

            var startDraw = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            var endFirst = new Vector3(_currentPath.Nodes[0].Position.x, _currentPath.Nodes[0].Position.y, _currentPath.Nodes[0].Position.z + 1);
            Debug.DrawLine(startDraw, endFirst, DebugColor);
            for (int i = 0; i < _currentPath.Nodes.Count - 1; i++)
            {
                var start = new Vector3(_currentPath.Nodes[i].Position.x, _currentPath.Nodes[i].Position.y, _currentPath.Nodes[i].Position.z + 1);
                var end = new Vector3(_currentPath.Nodes[i + 1].Position.x, _currentPath.Nodes[i + 1].Position.y, _currentPath.Nodes[i + 1].Position.z + 1);

                Debug.DrawLine(start, end, DebugColor);
            }
        }

        protected virtual void ToggleAnimationState(EMovableObjectState state)
        {
            switch (state)
            {
                case EMovableObjectState.Standing:
                    {
                        SelfAnimator.SetBool("Walk", false);
                        _currentState = EMovableObjectState.Standing;
                        break;
                    }
                case EMovableObjectState.Walking:
                    {
                        SelfAnimator.SetBool("Walk", true);
                        _currentState = EMovableObjectState.Walking;
                        break;
                    }
                default:
                    break;
            }

        }

        #endregion
    }
}

