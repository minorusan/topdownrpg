using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters.Player;

namespace Core.Gameplay.Interactivity
{
    public enum EDragState
    {
        Dragging,
        Idle
    }
    public class DraggableObject : MonoBehaviour
    {
        private ActionBase _dragActionBase;
        private ActionBase _undragActionBase;
        public EDragState State = EDragState.Idle;
      
        void Start()
        {
            _dragActionBase = ActionsInitialiser.GetActionByID("action.id.drag");
            _undragActionBase = ActionsInitialiser.GetActionByID("action.id.undrag");
        }

        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if (trigger.tag == PlayerBehaviour.kPlayerTag && trigger.isTrigger)
            {
                ActionPerformer.Instance.SetAction(State == EDragState.Idle ? _dragActionBase : _undragActionBase, gameObject);
            }
           
        }

        private void OnTriggerExit2D(Collider2D trigger)
        {
            if (trigger.tag == PlayerBehaviour.kPlayerTag)
            {
                ActionPerformer.Instance.SetAction(null);
            }
        }
    }

}

