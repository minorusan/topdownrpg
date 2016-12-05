using UnityEngine;
using System.Collections;

namespace Core.Map
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class NonWalkable : MonoBehaviour
    {
        #region PRIVATE

        private Bounds _bounds;

        #endregion

        public Bounds Bounds
        {
            get
            {
                return _bounds;
            }
        }

        void Awake()
        {
            _bounds = GetComponent<BoxCollider2D>().bounds;
        }
    }
}

