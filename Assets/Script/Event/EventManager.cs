using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    //singleton
    public static EventManager instacne = null;

    //enum
    public enum EventType {
        ENEMY, NORMAL, SPECIAL, BRANCH
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

    public struct EventInfo {
        public EventType type;
        public int detail;
    }

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
    MissionManager mission = new MissionManager();
    public MissionManager Mission {
        get {
            return mission;
        }
    }
    public delegate void CallForEvent();
    event CallForEvent StartEventForEnemy;
    event CallForEvent EndEventForEnemy;
    event CallForEvent StartEventForNormal;
    event CallForEvent EndEventForNormal;
    event CallForEvent StartEventForSpecial;
    event CallForEvent EndEventForSpecial;
    event CallForEvent StartEventForBranch;
    event CallForEvent EndEventForBranch;

    void Awake() {
        instacne = this;
    }

    IEnumerator Start() {
        yield return null;
    }

    void EventForEnemyStart(EventTypeForEnemy type = EventTypeForEnemy.SPINE) {
        currentEventForEnemy = type;
        StartEventForEnemy();
    }

    void EventForEnemyEnd() {
        EndEventForEnemy();
    }

    void EventForNormalStart(EventTypeForNormal detail = EventTypeForNormal.STAR) {
        currentEvent.type = EventType.NORMAL;
        currentEvent.detail = (int)detail;
        StartEventForNormal();
    }
    
    void EventForNormalEnd() {
        EndEventForNormal();
    }

    void EventForSpecialStart(EventTypeForSpecial detail) {
        currentEvent.type = EventType.SPECIAL;
        currentEvent.detail = (int)detail;
        StartEventForSpecial();
    }

    void EventForSpecialEnd() {
        EndEventForSpecial();
    }

    void EventForBranchStart(EventTypeForBranch detail) {
        currentEvent.type = EventType.BRANCH;
        currentEvent.detail = (int)detail;
        StartEventForBranch();
    }

    void EventForBranchEnd() {
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
}