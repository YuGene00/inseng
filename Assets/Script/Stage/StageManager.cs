using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

    //singleton
    public static StageManager instacne = null;

    //enum
    public enum StageType {
        CHILD, STUDENT, UNIVERSITY, UNEMPLOYED, WORKER, CHICKEN, SENIOR, END
    }

    //variable
    Stage[] stageList = new Stage[(int)StageType.END];
    StageType currentStage;
    public StageType CurrentStage {
        get {
            return currentStage;
        }
        set {
            currentStage = value;
        }
    }
    public delegate void CallForEvent();
    event CallForEvent StartStageEvent = delegate { };
    event CallForEvent EndStageEvent = delegate { };

    void Awake() {
        instacne = this;
        SetStage();
    }

    void Start() {
        StartCoroutine("RunStage");
    }

    void SetStage() {
        GameObject obj = gameObject;
        stageList[(int)StageType.CHILD] = obj.AddComponent<ChildStage>();
        stageList[(int)StageType.STUDENT] = obj.AddComponent<StudentStage>();
        stageList[(int)StageType.UNIVERSITY] = obj.AddComponent<UniversityStage>();
        stageList[(int)StageType.UNEMPLOYED] = obj.AddComponent<UnemployedStage>();
        stageList[(int)StageType.WORKER] = obj.AddComponent<WorkerStage>();
        stageList[(int)StageType.CHICKEN] = obj.AddComponent<ChickenStage>();
        stageList[(int)StageType.SENIOR] = obj.AddComponent<SeniorStage>();
    }

    IEnumerator RunStage() {
        WaitWhile WaitWhileStageRunningTrue = new WaitWhile(() => (Stage.StageRunning));
        yield return null;
        EventManager.instacne.EventForEnemyStart();
        while (true) {
            StartStageEvent();
            stageList[(int)currentStage].StartStage();
            yield return WaitWhileStageRunningTrue;
            EndStageEvent();
        }
    }

    public void AddFuncToEventForStart(CallForEvent eventFunc) {
        StartStageEvent += eventFunc;
    }

    public void AddFuncToEventForEnd(CallForEvent eventFunc) {
        EndStageEvent += eventFunc;
    }
}