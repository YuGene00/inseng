using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BackMover : MonoBehaviour 
{
	private float speed = 1.0f;
	public Transform backGroup = null;

	private float length = 1280.0f;
	private Sprite[] spriteGroup = null;

	private int imgIndex = 0;
	private int backIndex = 0;

	void Start()
	{
		speed = GameController.GetInstance ().gameSpeed;


		ChangeStage(GameController.GetInstance().nowStage);

		for (int i=0;i<backGroup.childCount;i++)
		{
			if (i.Equals (0))
				ChangeImage (i, i);
			else
				ChangeImage(i,Random.Range (1,spriteGroup.Length-2));

			length = backGroup.GetChild (i).GetComponent<Renderer> ().bounds.size.y;
			backGroup.GetChild (i).position = new Vector3 (0.0f,i*length,0.0f);
		}

		imgIndex = backGroup.childCount-1;
	}

	void OnEnable()
	{
		GameController.ChangeStageEvnet += ChangeStage;
	}

	void OnDisable()
	{
		GameController.ChangeStageEvnet -= ChangeStage;
	}

	void ChangeStage(GameController.JOBSTAGE stage)
	{
		string stageName = stage.ToString().Split('_')[0];

		spriteGroup = LoadSprite (stageName);

		imgIndex = 0;
	}

	Sprite[] LoadSprite(string name)
	{
		return Resources.LoadAll<Sprite>("Images/Backgrounds/"+name);
	}

	void Update()
	{
		//move
		MoveBack();
	}

	void MoveBack()
	{
		int backCount = backGroup.childCount;

		for(int i=0;i<backCount;i++)
			backGroup.GetChild(i).Translate(new Vector3(0.0f,-speed*Time.deltaTime, 0.0f));

		int proIndex;

		if (backIndex.Equals (backGroup.childCount - 1))
			proIndex = 0;
		else
			proIndex = backIndex+1;

		float len = backGroup.GetChild (proIndex).GetComponent<Renderer> ().bounds.size.y;

		if(backGroup.GetChild(proIndex).position.y < -len+200.0f)
		{
			int preIndex;

			if (backIndex.Equals (0))
				preIndex = backGroup.childCount-1;
			else
				preIndex = backIndex-1;

			Transform preTran = backGroup.GetChild (preIndex);
			length = preTran.GetComponent<Renderer> ().bounds.size.y;

			if (!imgIndex.Equals (0))
			{
				//Stage Change Check
				imgIndex = RndNum(1,spriteGroup.Length-1,imgIndex);

				ChangeImage (backIndex,imgIndex);
			}
			else
				ChangeImage (backIndex,imgIndex++);

			len = backGroup.GetChild (backIndex).GetComponent<Renderer> ().bounds.size.y;

			backGroup.GetChild (backIndex).position = new Vector3 (0.0f,preTran.position.y+length/2.0f+len/2.0f,0.0f);

			backIndex = (backIndex+1)%backCount;
		}
	}

	void ChangeImage(int bIndex, int iIndex)
	{
		backGroup.GetChild (bIndex).GetComponent<SpriteRenderer> ().sprite = spriteGroup [iIndex];
	}

	int RndNum(int min, int max, int pre)
	{
		int rnd = Random.Range (min,max);

		while(rnd.Equals(pre))
			rnd = Random.Range (min,max);

		return rnd;
	}
}