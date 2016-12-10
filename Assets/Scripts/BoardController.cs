using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour 
{
	public SpriteRenderer spriteRenderer = null;
	private Animator boardAnimator = null;
	private Sprite[] boardSprite = null;

	void Start () 
	{
		boardAnimator = GetComponent<Animator>();
		boardSprite = Resources.LoadAll<Sprite>("Images/Board");
	}

	void OnEnable()
	{
		GameController.SpecialEvent += Move1;
		GameController.SectionEvent += Move2;
	}

	void OnDisable()
	{
		GameController.SpecialEvent -= Move1;
		GameController.SectionEvent -= Move2;
	}

	void Move1()
	{
		spriteRenderer.sprite = boardSprite [0];
		boardAnimator.SetTrigger ("Move");
	}

	void Move2()
	{
		spriteRenderer.sprite = boardSprite [1];
		boardAnimator.SetTrigger ("Move");
	}
}