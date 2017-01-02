using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EventManager : MonoBehaviour {

    //singleton
    public static EventManager instacne = null;

    //enum
    public enum EventType {
        ENEMY, NORMAL, SPECIAL, BRANCH, END
    }
    
    public enum EventTypeForEnemy {
        SPINE, END
    }

    public enum EventTypeForNormal {
        STAR, END
    }

    public enum EventTypeForSpecial {
        CHILD, STUDENT, UNIVERSITY, UNEMPLOYED, WORKER, CHICKEN, SENIOR, END
    }

    public enum EventTypeForBranch {
        CSAT, JOBHUNT, DARWINISM, MARRIAGE, END
    }

    //struct
    public struct EventInfo {
        public EventType type;
        public int detail;
    }

    //inspector
    public GameObject EndCanvas;
    public Text highScoreText;
    public Text currentScoreText;

    //variable
    EventTypeForEnemy currentEventForEnemy;
    public EventTypeForEnemy GetCurrentEventForEnemy {
        get {
            return currentEventForEnemy;
        }
    }
    EventInfo currentEvent;
    public EventInfo GetCurrentEvent {
        get {
            return currentEvent;
        }
    }
    public delegate void CallForEvent();
    event CallForEvent StartEventForEnemy = delegate { };
    event CallForEvent EndEventForEnemy = delegate { };
    event CallForEvent StartEventForNormal = delegate { };
    event CallForEvent EndEventForNormal = delegate { };
    event CallForEvent StartEventForSpecial = delegate { };
    event CallForEvent EndEventForSpecial = delegate { };
    event CallForEvent StartEventForBranch = delegate { };
    event CallForEvent EndEventForBranch = delegate { };
    event CallForEvent DieEvent = delegate { };

    void Awake() {
        instacne = this;
    }

    public void EventForEnemyStart(EventTypeForEnemy type = EventTypeForEnemy.SPINE) {
        currentEventForEnemy = type;
        StartEventForEnemy();
    }

    public void EventForEnemyEnd() {
        EndEventForEnemy();
    }

    public void EventForNormalStart(EventTypeForNormal detail = EventTypeForNormal.STAR) {
        currentEvent.type = EventType.NORMAL;
        currentEvent.detail = (int)detail;
        StartEventForNormal();
    }
    
    public void EventForNormalEnd() {
        EndEventForNormal();
    }

    public void EventForSpecialStart(EventTypeForSpecial detail) {
        currentEvent.type = EventType.SPECIAL;
        currentEvent.detail = (int)detail;
        StartEventForSpecial();
    }

    public void EventForSpecialEnd() {
        EndEventForSpecial();
    }

    public void EventForBranchStart(EventTypeForBranch detail) {
        currentEvent.type = EventType.BRANCH;
        currentEvent.detail = (int)detail;
        StartEventForBranch();
    }

    public void EventForBranchEnd() {
        EndEventForBranch();
    }

    public void AddFuncToEventForStart(CallForEvent eventFunc, EventType type) {
        switch(type) {
            case EventType.ENEMY:
                StartEventForEnemy += eventFunc;
                break;
            case EventType.NORMAL:
                StartEventForNormal += eventFunc;
                break;
            case EventType.SPECIAL:
                StartEventForSpecial += eventFunc;
                break;
            case EventType.BRANCH:
                StartEventForBranch += eventFunc;
                break;
        }
    }

    public void AddFuncToEventForEnd(CallForEvent eventFunc, EventType type) {
        switch (type) {
            case EventType.ENEMY:
                EndEventForEnemy += eventFunc;
                break;
            case EventType.NORMAL:
                EndEventForNormal += eventFunc;
                break;
            case EventType.SPECIAL:
                EndEventForSpecial += eventFunc;
                break;
            case EventType.BRANCH:
                EndEventForBranch += eventFunc;
                break;
        }
    }

    public void EventForDie() {
        StartCoroutine("RunDieEvent");
    }

    IEnumerator RunDieEvent() {
        DieEvent();
        PrepareDie();
        yield return new WaitForSeconds(1f);
        Rigidbody2D rigid = Player.instance.GetComponent<Rigidbody2D>();
        SetToFallingWithRigid(rigid);
        Result();
        yield return new WaitWhile(() => (Player.instance.IsInArea()));
        rigid.isKinematic = true;
        rigid.velocity = Vector2.zero;
    }

    void PrepareDie() {
        BackMover.instance.Speed = 0f;
        InputManager.instance.Active = false;
    }

    void SetToFallingWithRigid(Rigidbody2D rigid) {
        Player.instance.SetSpriteWithState(SpriteSelector.SpriteType.DROP);
        rigid.isKinematic = false;
    }

    void Result() {
        EndCanvas.SetActive(true);
        ScoreManager.instance.UpdateHighScore();
        highScoreText.text = ScoreManager.instance.GetHighScore().ToString();
        currentScoreText.text = ScoreManager.instance.GetScore.ToString();
    }

    public void AddFucToEventForDie(CallForEvent eventFunc) {
       DieEvent += eventFunc;
    }

    public void Reload() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Title() {
        SceneManager.LoadScene("Title");
    }
}