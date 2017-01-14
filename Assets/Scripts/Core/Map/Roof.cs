using UnityEngine;


namespace Core.Map
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class Roof : MonoBehaviour
	{
		private SpriteRenderer _selfRenderer;

	    #region Monobehaviour

	    private void Start()
	    {
	        _selfRenderer = GetComponent<SpriteRenderer>();
	    }

	    private void OnTriggerEnter2D(Collider2D collision)
	    {
	        if(collision.tag == "Player")
	        {
	            _selfRenderer.enabled = false;
	        }
	    }

	    private void OnTriggerExit2D(Collider2D collision)
	    {
	        if(collision.tag == "Player")
	        {
	            _selfRenderer.enabled = true;
	        }
	    }

	    private void OnTriggerStay2D(Collider2D collision)
	    {
	        if(collision.tag == "Player" && _selfRenderer.enabled)
	        {
	            _selfRenderer.enabled = false;
	        }
	    }

	    #endregion
	}
}

