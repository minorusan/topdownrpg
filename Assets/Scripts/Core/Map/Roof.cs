using Core.Characters.Player;
using DynamicLight2D;
using UnityEngine;


namespace Core.Map
{
    [RequireComponent(typeof(Collider2D))]
    public class Roof : MonoBehaviour
    {
        private Renderer[] _renderers;
        private DynamicLight[] _lights;
        private SpriteRenderer _selfRenderer;
        private MovableObject[] _movableObjects;
        private DayNight _dayNight;

        public bool DisableOnAwake = false;

        #region Monobehaviour

        private void Start()
        {
            _dayNight = FindObjectOfType<DayNight>();
            _selfRenderer = GetComponent<SpriteRenderer>();
            _renderers = transform.parent.parent.gameObject.GetComponentsInChildren<Renderer>();
            _lights = transform.parent.parent.gameObject.GetComponentsInChildren<DynamicLight>();
            _movableObjects = transform.parent.parent.gameObject.GetComponentsInChildren<MovableObject>();
            if (DisableOnAwake)
            {
                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = false;
                }

                foreach (var renderer in _renderers)
                {
                    renderer.enabled = false;
                }

                foreach (var obj in _movableObjects)
                {
                    obj.enabled = false;
                }
                _selfRenderer.enabled = true;
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.tag == "Player")
            {
                PlayerBehaviour.CurrentPlayer.TurnOnLight(true);

                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = true;
                }

                foreach (var renderer in _renderers)
                {
                    renderer.enabled = true;
                }

                foreach (var obj in _movableObjects)
                {
                    obj.enabled = true;
                }

                _selfRenderer.enabled = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            PlayerBehaviour.CurrentPlayer.TurnOnLight(true);
            _dayNight.Block(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _dayNight.Block(false);
                PlayerBehaviour.CurrentPlayer.TurnOnLight(false);
                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = false;
                }

                foreach (var renderer in _renderers)
                {
                    renderer.enabled = false;
                }

                foreach (var obj in _movableObjects)
                {
                    obj.enabled = false;
                }

                _selfRenderer.enabled = true;
            }
        }



        #endregion
    }
}

