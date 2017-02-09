
using UnityEngine;

namespace Utils
{
	[RequireComponent (typeof(Collider2D))]
	public class NoiseEffect : MonoBehaviour
	{
	    private _2dxFX_Blur[] _blurredObjects;
	    private Renderer _renderer;

	    private void Start()
	    {
	        _renderer = GetComponent<Renderer>();
	        _blurredObjects = GetComponentsInChildren<_2dxFX_Blur>();
	    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _renderer.enabled = false;
                foreach (var blur in _blurredObjects)
                {
                    blur.enabled = false;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _renderer.enabled = true;
                foreach (var blur in _blurredObjects)
                {
                    blur.enabled = true;
                }
            }
        }
    }
}

