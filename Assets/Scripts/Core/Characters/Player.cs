using UnityEngine;
using System.Collections;

namespace Core.Characters
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        private Animator _animator;
        private bool _moves;

        public bool Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                if (_moves != value)
                {
                    _moves = value;
                    _animator.SetBool("Walk", _moves);
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }
}