using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    //caching
    Transform trans;

    //variable
    Move move;


	public SpriteRenderer playerSpriter = null;
	public SpriteRenderer tmpSpriter = null;

	public BoxCollider2D playerCollider = null;

	private Animator playerAnimator = null;

	private Sprite[] normalGroup = null;
	private Sprite[] sadGroup = null;
	private Sprite[] smileGroup = null;
	private Sprite[] dropGroup = null;

	public int coreItem = 0;
	public int passiveItem = 0;
	public int activeItem = 0;

	public int Life = 5;

	public bool isMuJuck = false;

    void Awake() 
	{
		playerAnimator = GetComponent<Animator> ();

		normalGroup = Resources.LoadAll<Sprite>("Images/Character/Normal");
		sadGroup = Resources.LoadAll<Sprite>("Images/Character/Sad");
		smileGroup = Resources.LoadAll<Sprite>("Images/Character/Smile");
		dropGroup = Resources.LoadAll<Sprite>("Images/Character/Drop");

        Caching();
        InitMove();
    }

	void OnEnable()
	{
		GameController.ChangeStageEvnet += PlayerChange;
		GameController.SpecialEndEvent += PlayerSpecialEnd;
		GameController.SectionEndEvent += PlayerSectionEnd;
		GameController.DyingSeniEvent += DyingSeni;
	}

	void OnDisable()
	{
		GameController.ChangeStageEvnet -= PlayerChange;
		GameController.SpecialEndEvent -= PlayerSpecialEnd;
		GameController.SectionEndEvent -= PlayerSectionEnd;
		GameController.DyingSeniEvent -= DyingSeni;
	}

	public void DamagedLife(int num)
	{
		if (isMuJuck)
			return;

		string stageName = GameController.GetInstance ().nowStage.ToString ();

		Life-=num;
		LifeChecker();
		playerSpriter.sprite = FindSprite (stageName,sadGroup);

		isMuJuck = true;

		StartCoroutine ("Delay");
	}

	void DyingSeni()
	{
		StartCoroutine ("Dyning");
	}

	IEnumerator Dyning()
	{
		isMuJuck = true;

		while (Life <= 0)
		{
			Life--;
			Dyning ();
			yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds (1.0f);
		isMuJuck = false;
	}

	void PlayerSpecialEnd()
	{
		string stageName = GameController.GetInstance ().nowStage.ToString ();
			
		if (coreItem <= -5) 
		{
			Life--;
			LifeChecker ();
			playerSpriter.sprite = FindSprite (stageName,sadGroup);
		} 

		else if (coreItem >= 5) 
		{
			Life++;
			playerSpriter.sprite = FindSprite (stageName,smileGroup);
		}
	}

	void PlayerSectionEnd()
	{
		string stageName = GameController.GetInstance ().nowStage.ToString ();

		if (coreItem < 8) 
		{
			Life--;
			LifeChecker ();
			playerSpriter.sprite = FindSprite (stageName,sadGroup);
			GameController.GetInstance().GlobalChange(1);
		} 
		else
		{
			Life+=2;
			playerSpriter.sprite = FindSprite (stageName,smileGroup);
			GameController.GetInstance().GlobalChange(0);
		}
	}

	public void LifeChecker()
	{
		if (Life <= 0) 
		{
			string stageName = GameController.GetInstance().nowStage.ToString();
			playerSpriter.sprite = FindSprite (stageName,dropGroup);
		}
	}

	void PlayerChange(GameController.JOBSTAGE stage)
	{
		tmpSpriter.sprite = FindSprite (stage.ToString(),normalGroup);
		playerAnimator.SetTrigger ("Change");
	}

	public void SetSprite()
	{
		playerSpriter.sprite = tmpSpriter.sprite;
		tmpSpriter.gameObject.SetActive (false);

		Vector2 size = playerSpriter.sprite.bounds.size;
		playerCollider.size = size;
	}

	Sprite FindSprite(string name, Sprite[] spriteGroup)
	{
		string stageName = name.Split('_')[0];
		int index = 0;

		for (;index < spriteGroup.Length;index++)
			if (spriteGroup [index].name.Equals (stageName))
				break;

		return spriteGroup [index];
	}


    void Caching() {
        trans = transform;
    }

    void InitMove() {
        move = new Move();
        move.SetMovableArea(new Vector2(-360f, -640f), new Vector2(360f, 128f));
    }

    public Vector2 GetPosition() {
        return trans.position;
    }

    public Transform Move(Vector2 dest) {
        return move.MoveTransToDestInArea(trans, dest);
    }
}
