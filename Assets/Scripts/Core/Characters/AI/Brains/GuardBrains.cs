using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Core.Characters.AI;


namespace Core.Interactivity.AI
{
    public class GuardBrains:ArtificialIntelligence
    {
        public float SearchDistance = 6f;
        public float AlertTime = 5f;
        public Image SuspentionBar;

        private void OnDrawGizmos()
        {
            var color = Color.white;
            var radius = SearchDistance;
            #if !UNITY_EDITOR
            if (_currentState != null && _currentState.State == EAIState.Wandering)
            {
            color = Color.green;
            }
            else
            {
            color = Color.yellow;
            radius = SearchDistance * 2f;
            }
            #endif


            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        #region ArtificialIntelligence

        protected override void InitStates()
        {
            _availiableStates.Add(EAIState.Wandering, new AIStateWandering(this, SearchDistance, SuspentionBar));
            _availiableStates.Add(EAIState.Alert, new AIStateAlert(this, SearchDistance * 2, AlertTime));
            _availiableStates.Add(EAIState.Attack, new AIStateAttack(this));
            BaseState = EAIState.Wandering;
        }

        #endregion
    }
}

