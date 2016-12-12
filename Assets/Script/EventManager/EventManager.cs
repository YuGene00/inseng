using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    //singleton
    public static EventManager instacne = null;

    //enum
    public enum EventType {
        NORMAL, SPECIAL, BRANCH
    }
    
    public enum EnemyType {
        Spine
    }

    public enum NormalType {
        Star
    }

    public enum SpecialType {
        CHILD, STUDENT, UNIVERSITY, UNEMPLOYED, WORKER, CHICKEN, SENIOR, END
    }

    public enum BranchType {
        CSAT, JOBHUNT, DARWINISM, MARRIAGE, END
    }

    public struct EventData {
        public EventType type;
        public int detail;
    }

    //variable
    EnemyType currentEnemy;
    public EnemyType GetCurrentEnemy {
        get {
            return currentEnemy;
        }
    }
    EventData currentEvent;
    public EventData GetCurrentEvent {
        get {
            return currentEvent;
        }
    }
    public delegate void CallForEvent();
    event CallForEvent EventForEnemyStart;
    event CallForEvent EventForEnemyEnd;
    event CallForEvent EventForNormalStart;
    event CallForEvent EventForNormalEnd;
    event CallForEvent EventForSpecialStart;
    event CallForEvent EventForSpecialEnd;
    event CallForEvent EventForBranchStart;
    event CallForEvent EventForBranchEnd;

    void Awake() {
        instacne = this;
    }

    IEnumerator Start() {
        yield return null;
    }

    void EnemyStart(EnemyType type = EnemyType.Spine) {
        currentEnemy = type;
        EventForEnemyStart();
    }

    void EnemyEnd() {
        EventForEnemyEnd();
    }

    void NormalStart(NormalType detail = NormalType.Star) {
        currentEvent.type = EventType.NORMAL;
        currentEvent.detail = (int)detail;
        EventForNormalStart();
    }
    
    void NormalEnd() {
        EventForNormalEnd();
    }

    void SpecialStart(SpecialType detail) {
        currentEvent.type = EventType.SPECIAL;
        currentEvent.detail = (int)detail;
        EventForSpecialStart();
    }

    void SpecialEnd() {
        EventForSpecialEnd();
    }

    void BranchStart(BranchType detail) {
        currentEvent.type = EventType.BRANCH;
        currentEvent.detail = (int)detail;
        EventForBranchStart();
    }

    void BranchEnd() {
        EventForBranchEnd();
    }

    public void AddEventToTypeForStart(CallForEvent eventFunc, EventType type) {
        switch(type) {
            case EventType.NORMAL:
                EventForNormalStart += eventFunc;
                break;
            case EventType.SPECIAL:
                EventForSpecialStart += eventFunc;
                break;
            case EventType.BRANCH:
                EventForBranchStart += eventFunc;
                break;
        }
    }

    public void AddEventToTypeForEnd(CallForEvent eventFunc, EventType type) {
        switch (type) {
            case EventType.NORMAL:
                EventForNormalEnd += eventFunc;
                break;
            case EventType.SPECIAL:
                EventForSpecialEnd += eventFunc;
                break;
            case EventType.BRANCH:
                EventForBranchEnd += eventFunc;
                break;
        }
    }
}