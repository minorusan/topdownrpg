﻿using UnityEngine;
using Core.Characters.Player.Demand;
using DynamicLight2D;


namespace Core.Characters.Player
{
    [RequireComponent(typeof(StressAffector))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerBehaviour : MonoBehaviour
    {
        public static string kPlayerTag = "Player";
        public static float BaseMovementSpeed = 10f;
        public AudioClip StepSound;
        private DynamicLight _light;
        #region Private

        private Animator _animator;
        private SpriteRenderer _renderer;
        private static PlayerBehaviour _player;
        private StressAffector _stress;
        private DeathController _death;
        private bool _moves;
        #endregion

        [Header("Movement")]
        public float MovementSpeed;
        public float Noise;

        public SpriteRenderer Renderer
        {
            get { return _renderer ?? (_renderer = GetComponent<SpriteRenderer>()); }
        }

        public static PlayerBehaviour CurrentPlayer
        {
            get { return _player; }
        }

        public bool Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                if (_moves == value) return;
                _moves = value;
                _animator.SetBool("Walk", _moves);
            }
        }

        public void Step()
        {
            Noise += (float)_stress.DemandState / 100;
            AudioSource.PlayClipAtPoint(StepSound, transform.position, Noise);
        }

        private void Awake()
        {
            _light = GetComponentInChildren<DynamicLight>();
            TurnOnLight(false);
            BaseMovementSpeed = MovementSpeed;
            Debug.LogWarning("Tutorial::Removing player prefs!");
            PlayerPrefs.DeleteAll();
        }

        private void Start()
        {
            _stress = GetComponent<StressAffector>();
            _animator = GetComponent<Animator>();
            _death = GetComponent<DeathController>();
        }

        private void OnEnable()
        {
           _player = this;
        }

        private void LateUpdate()
        {
            Noise = Mathf.MoveTowards(Noise, 0, 0.2f);
        }

        public static void SetPlayerPosition(Vector3 position)
        {
            var player = FindObjectOfType<PlayerBehaviour>();
            player.transform.position = position;
        }

        public void Kill()
        {
            _death.Kill();
        }

        public void TurnOnLight(bool b)
        {
            _light.gameObject.SetActive(b);
        }
    }
}