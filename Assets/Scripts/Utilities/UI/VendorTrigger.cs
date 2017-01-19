using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class VendorTrigger : MonoBehaviour
    {

        void Start()
        {
           
        }

        private void OnTriggerEnter2D(Collider2D trigger)
        {
            

            if (trigger.tag == PlayerBehaviour.kPlayerTag && trigger.isTrigger)
            {
                ActionPerformer.Instance.SetAction(_dialogueAction, gameObject);
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

