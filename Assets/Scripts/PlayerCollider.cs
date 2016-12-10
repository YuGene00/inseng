using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{
	public delegate void ObjectColliderHandler();
	public event ObjectColliderHandler CollidedEvent;

	void OnTriggerEnter2D(Collider2D collided)
	{
		if (collided.CompareTag ("Item"))
		{
			if (CollidedEvent != null)
				CollidedEvent ();
		}
	}
}