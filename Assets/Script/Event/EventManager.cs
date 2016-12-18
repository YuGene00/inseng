using UnityEngine;
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
    public delegate void CallForEvent();
    event CallForEvent StartEventForEnemy = delegate { };
    event CallForEvent EndEventForEnemy = delegate { };
    event CallForEvent StartEventForNormal = delegate { };
    event CallForEvent EndEventForNormal = delegate { };
    event CallForEvent StartEventForSpecial = delegate { };
    event CallForEvent EndEventForSpecial = delegate { };
    event CallForEvent StartEventForBranch = delegate { };
    event CallForEvent EndEventForBranch = delegate { };

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
}