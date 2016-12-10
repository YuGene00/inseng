using UnityEngine;
using System.Collections;

public class NumCount : MonoBehaviour 
{
	public int num = 0;
	private Sprite[] spriteNum = null;
	public SpriteRenderer numberRender = null; 

	void Start()
	{
		spriteNum = Resources.LoadAll<Sprite>("Images/Number/Num");
	}

	void OnEnable()
	{
		GameController.SpecialEvent += TimerStart;
	}

	void OnDisable()
	{
		GameController.SpecialEvent -= TimerStart;
	}


	void TimerStart()
	{
		numberRender.gameObject.SetActive (true);
	}

	void TimerEnd()
	{
		numberRender.gameObject.SetActive (false);
	}

	
	void Update () 
	{
		float tmp = GameController.GetInstance().GetTime();

		tmp = Mathf.Clamp(5-tmp,0,5);

		numberRender.sprite = spriteNum[((int)tmp)%10];

		if (tmp<=0.0f)
			TimerEnd();
	}
}