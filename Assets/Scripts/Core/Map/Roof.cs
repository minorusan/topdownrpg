using Core.Characters.Player;
using DynamicLight2D;
using UnityEngine;


namespace Core.Map
{
    [RequireComponent(typeof(Collider2D))]
    public class Roof : MonoBehaviour
    {
        private SpriteRenderer[] _selfRenderer;
        private MeshRenderer[] _lightRenderers;
        private DynamicLight[] _lights;
        private DayNight _dayNight;

        public bool DisableOnAwake = false;

        #region Monobehaviour

        private void Start()
        {
            _selfRenderer = GetComponentsInChildren<SpriteRenderer>();
            _lightRenderers = transform.parent.gameObject.GetComponentsInChildren<MeshRenderer>();
            _dayNight = FindObjectOfType<DayNight>();
            _lights = transform.parent.gameObject.GetComponentsInChildren<DynamicLight>();
            if (!DisableOnAwake)
            {
                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = false;
                }
                foreach (var spriteRenderer in _selfRenderer)
                {
                    spriteRenderer.enabled = false;
                }
            }

            foreach (var lightRenderer in _lightRenderers)
            {
                lightRenderer.enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                PlayerBehaviour.CurrentPlayer.TurnOnLight(true);
                
                foreach (var spriteRenderer in _selfRenderer)
                {
                    spriteRenderer.enabled = false;
                }
                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = true;
                }
                foreach (var lightRenderer in _lightRenderers)
                {
                    lightRenderer.enabled = true;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            PlayerBehaviour.CurrentPlayer.TurnOnLight(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                PlayerBehaviour.CurrentPlayer.TurnOnLight(false);
                foreach (var spriteRenderer in _selfRenderer)
                {
                    spriteRenderer.enabled = true;
                }
                foreach (var dynamicLight in _lights)
                {
                    dynamicLight.enabled = false;
                }
                foreach (var lightRenderer in _lightRenderers)
                {
                    lightRenderer.enabled = false;
                }
            }
        }



        #endregion
    }
}

