using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    //singleton
    public static Player instance = null;

    //caching
    Transform trans;
    Animator playerAnimator;

    //inspector
    public float immortalTime = 1f;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer tmpSpriteRenderer;
    public BoxCollider2D playerCollider;

    public GameObject EndCanvas;
    public Text highScoreText;
    public Text currentScoreText;

    //variable
    Move move;
    Life life;
    SpriteManager spriteManager;

    void Awake() 
	{
        instance = this;
        Caching();
        Initialize();
    }

    void Initialize() {
        InitMove();
        InitLife();
        InitSpriteManager();
    }

    void InitMove() {
        move = new Move();
        move.SetMovableArea(new Vector2(-360f, -640f), new Vector2(360f, 128f));
    }

    void InitLife() {
        life = gameObject.AddComponent<Life>();
    }

    void InitSpriteManager() {
        spriteManager = new SpriteManager();
    }

    void Caching() {
        trans = transform;
        playerAnimator = GetComponent<Animator>();
    }

    void Start() {
        BindFuncToEvent();
    }

    void BindFuncToEvent() {
        StageManager.instacne.AddFuncToEventForStart(ChangeSprite);
    }

    void ChangeSprite() {
        spriteManager.SetRendererWithType(tmpSpriteRenderer, SpriteSelector.SpriteType.NORMAL);
        playerAnimator.SetTrigger("Change");
    }

    public void ChangeComplete() {
        playerSpriteRenderer.sprite = tmpSpriteRenderer.sprite;
        tmpSpriteRenderer.gameObject.SetActive(false);

        Vector2 size = playerSpriteRenderer.sprite.bounds.size;
        playerCollider.size = size;
    }

    public Vector2 GetPosition() {
        return trans.position;
    }

    public Transform Move(Vector2 dest) {
        return move.MoveTransToDestInArea(trans, dest);
    }

    public void AddLife(int value) {
        life.AddLife(value);
    }

    public void Damaged(int value) {
        life.Damaged(value);
    }

    void Result() {
        Time.timeScale = 0f;
        //ballon.PlaySound();
        EndCanvas.SetActive(true);
        //int currentScore = ScoreManager.instance.sco
        //if (DataSender.highScore < currentScore) {
            //DataSender.highScore = currentScore;
        //}
        //highScoreText.text = DataSender.highScore.ToString();
        //currentScoreText.text = currentScore.ToString();
    }

    public void Reload() {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Title() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
