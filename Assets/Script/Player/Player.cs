using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
    //singleton
    public static Player instance = null;

    //caching
    Transform trans;

    //variable
    Move move;

    //inspector
    public GameObject EndCanvas;
    public Text highScoreText;
    public Text currentScoreText;
    public ItemManager itemManager;

	public SpriteRenderer playerSpriter = null;
	public SpriteRenderer tmpSpriter = null;

	public BoxCollider2D playerCollider = null;

	private Animator playerAnimator = null;

	private Sprite[] normalGroup = null;
	private Sprite[] sadGroup = null;
	private Sprite[] smileGroup = null;
	private Sprite[] dropGroup = null;

    public Ballon ballon;

	public int Life = 5;

	public bool isMuJuck = false;

    void Awake() 
	{
        instance = this;

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
			
		if (GameController.GetInstance().EatItemNumber <= -5) 
		{
			Life--;
			LifeChecker ();
			playerSpriter.sprite = FindSprite (stageName,sadGroup);
		} 

		else if (GameController.GetInstance().EatItemNumber >= 5) 
		{
			Life++;
			playerSpriter.sprite = FindSprite (stageName,smileGroup);
		}
	}

	void PlayerSectionEnd()
	{
		string stageName = GameController.GetInstance ().nowStage.ToString ();

		if (GameController.GetInstance().EatItemNumber < 8) 
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
            Result();
		}
	}

    void Result() {
        Time.timeScale = 0f;
        ballon.PlaySound();
        itemManager.StopManager();
        EndCanvas.SetActive(true);
        int currentScore = GameController.GetInstance().score;
        if (DataSender.highScore < currentScore) {
            DataSender.highScore = currentScore;
        }
        highScoreText.text = DataSender.highScore.ToString();
        currentScoreText.text = currentScore.ToString();
    }

    public void Reload() {
        GameController.DeleteInstance();
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Title() {
        GameController.DeleteInstance();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
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
