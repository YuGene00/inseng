using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BackMover : MonoBehaviour 
{
	public float speed = 1.0f;
	public Transform backGroup = null;

	//private GameController.JOBSTAGE nowStage = GameController.JOBSTAGE.CHILD_STAGE;

	private float length = 1280.0f;
	private Sprite[] spriteGroup = null;

	private int imgIndex = 0;
	private int backIndex = 0;

	public bool lastImage = false;

	void Start()
	{
		ChangeStage(GameController.JOBSTAGE.CHILD_STAGE);

		for (int i=0;i<backGroup.childCount;i++)
		{
			ChangeImage (i, i);

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
		//nowStage = stage;

		string stageName = stage.ToString().Split('_')[0];

		spriteGroup = LoadSprite (stageName);
	}

	//last Image Event
	void LastImage()
	{
		lastImage = true;
	}

	Sprite[] LoadSprite(string name)
	{
		return Resources.LoadAll<Sprite>("Images/"+name);
	}

	void Update()
	{
		//move
		MoveBack();
	}

	void MoveBack()
	{
		int backCount = backGroup.childCount;
		float len = backGroup.GetChild (backIndex).GetComponent<Renderer> ().bounds.size.y;

		for(int i=0;i<backCount;i++)
			backGroup.GetChild(i).Translate(new Vector3(0.0f,-speed*Time.deltaTime, 0.0f));

		if(backGroup.GetChild(backIndex).position.y < -(len*1.5f))
		{
			int preIndex;

			if (backIndex.Equals (0))
				preIndex = 3;
			else
				preIndex = backIndex-1;


			Transform preTran = backGroup.GetChild (preIndex);
			length = preTran.GetComponent<Renderer> ().bounds.size.y;

			backGroup.GetChild (backIndex).position = new Vector3 (0.0f,preTran.position.y+length,0.0f);

			if (!imgIndex.Equals (0))
			{
				//Stage Change Check
				if (lastImage)
					imgIndex = spriteGroup.Length-1;
				else
					imgIndex = Random.Range (1,spriteGroup.Length-2);

				ChangeImage (backIndex,imgIndex);
			}
			else
				ChangeImage (backIndex,imgIndex++);

			backIndex = (backIndex+1)%backCount;

			if(lastImage)
				StageChange(1);
		}
	}

	void ChangeImage(int bIndex, int iIndex)
	{
		backGroup.GetChild (bIndex).GetComponent<SpriteRenderer> ().sprite = spriteGroup [iIndex];
	}

	void StageChange(int nextStage)
	{
		lastImage = false;

		//spriteGroup = LoadSprite (nextStage);

		imgIndex = 0;
	}
}