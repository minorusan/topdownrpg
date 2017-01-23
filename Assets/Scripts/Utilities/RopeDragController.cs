using System.Collections;
using System.Collections.Generic;
using Core.Characters.Player;
using UnityEngine;

namespace Core.Gameplay.Interactivity
{
    public class RopeDragController : MonoBehaviour
    {
        private static RopeDragController _instance;
        private Rigidbody2D _lastJoint;
        private GameObject _current;
        private HingeJoint2D _firstJoint2D;

        private void Awake()
        {
            _instance = this;
            _firstJoint2D = transform.GetChild(0).GetComponent<HingeJoint2D>();
            _lastJoint = transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D>();
            gameObject.SetActive(false);
        }

        public static void Bind(GameObject obj)
        {
            _instance._current = obj;
            obj.GetComponent<DraggableObject>().State = EDragState.Dragging;
            PlayerBehaviour.CurrentPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            _instance.transform.position = PlayerBehaviour.CurrentPlayer.transform.position;
            _instance.gameObject.SetActive(true);
            _instance._firstJoint2D.connectedAnchor = Vector2.zero;
            
            obj.GetComponent<HingeJoint2D>().connectedBody = _instance._lastJoint;
            obj.GetComponent<HingeJoint2D>().connectedAnchor = Vector2.zero;
          _instance.Invoke("UpdateBodies", 0.1f);
            
        }

        private void UpdateBodies()
        {
            PlayerBehaviour.CurrentPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
            _current.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        public static void Unbind(GameObject obj)
        {
            obj.GetComponent<DraggableObject>().State = EDragState.Idle;
            obj.GetComponent<Rigidbody2D>().isKinematic = true;
            _instance.gameObject.SetActive(false);
            obj.GetComponent<HingeJoint2D>().connectedBody = null;
        }
    }
}

