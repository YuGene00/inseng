using UnityEngine;
using System.Collections;

public class BackMover : MonoBehaviour 
{
    //singleton
    public static BackMover instance = null;

    //variable
	float speed = 800.0f;
    public float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }
	public Transform backGroup = null;

	private float length = 1280.0f;
	private Sprite[][] spriteGroup = new Sprite[(int)StageManager.StageType.END][];

	private int imgIndex = 0;
	private int backIndex = 0;

    void Awake() {
        instance = this;
        InitSpriteGroup();
    }

    void Start()
	{
        BindFuncToEvent();

        for (int i=0;i<backGroup.childCount;i++)
		{
			if (i.Equals (0))
				ChangeImage (i, i);
			else
				ChangeImage(i,Random.Range (1,spriteGroup[(int)StageManager.instance.CurrentStage].Length-2));

			length = backGroup.GetChild (i).GetComponent<Renderer> ().bounds.size.y;
			backGroup.GetChild (i).position = new Vector3 (0.0f,i*length,0.0f);
		}

		imgIndex = backGroup.childCount-1;
        StartCoroutine("RunBackMover");
    }

    void InitSpriteGroup() {
        for (int i = 0; i < (int)StageManager.StageType.END; ++i) {
            spriteGroup[i] = Resources.LoadAll<Sprite>("Image/Backgrounds/" + ((StageManager.StageType)i).ToString().ToUpper());
        }
    }

    void BindFuncToEvent() {
        StageManager.instance.AddFuncToEventForStart(ChangeStage);
    }

    void ChangeStage()
	{
		imgIndex = 0;
	}

    IEnumerator RunBackMover() {
        while(true) {
            MoveBack();
            yield return null;
        }
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
				imgIndex = RndNum(1,spriteGroup[(int)StageManager.instance.CurrentStage].Length-1,imgIndex);

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
        Sprite sprite = spriteGroup[(int)StageManager.instance.CurrentStage][iIndex];

        backGroup.GetChild (bIndex).GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	int RndNum(int min, int max, int pre)
	{
		int rnd = Random.Range (min,max);

		while(rnd.Equals(pre))
			rnd = Random.Range (min,max);

		return rnd;
	}
}