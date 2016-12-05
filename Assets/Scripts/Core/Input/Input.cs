using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Core.Characters;

namespace Core.Input
{
    public class Input : MonoBehaviour
    {
        private Vector3 screenPoint;
        private Vector3 initialPosition;
        private Vector3 offset;
        private Vector3 direction;
        private Rigidbody2D targetRB;

        public float speed = 20.0f;
        public float maxSpeed;

        public Player InputTarget;

        // Use this for initialization
        void Start()
        {
            targetRB = InputTarget.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            targetRB.velocity = Vector2.ClampMagnitude(targetRB.velocity, maxSpeed);
            if (!float.IsNaN(direction.x) || !float.IsNaN(direction.y))
            {
                targetRB.AddForce(direction * speed, ForceMode2D.Impulse);
                InputTarget.Moves = true;
            }

            if (targetRB.velocity == Vector2.zero)
            {
                InputTarget.Moves = false;
            }
        }

        public void OnMouseDrag()
        {
            targetRB.velocity = Vector2.zero;
            InputTarget.Moves = false;
            Vector3 cursorPoint = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, 0);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            Vector3 heading = cursorPosition - initialPosition;
            direction = heading / heading.magnitude;     // heading magnitude = distance 
            direction.x = Mathf.Clamp(direction.x, -1f, 1f);
            direction.y = Mathf.Clamp(direction.y, -1f, 1f);
            //Do what you want.
            //if you want to drag object on only swipe gesture comment below. Otherwise:
            initialPosition = cursorPosition;
        }
    }
}

