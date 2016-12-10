using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour 
{
	private Animator boardAnimator = null;

	void Start () 
	{
		boardAnimator = GetComponent<Animator>();
	}

	void OnEnable()
	{
		GameController.SpecialEvent += Move;
	}

	void OnDisable()
	{
		GameController.SpecialEvent -= Move;
	}

	void Move()
	{
		boardAnimator.SetTrigger ("Move");
	}

	void Update () 
	{
	
	}
}