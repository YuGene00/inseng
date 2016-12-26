using UnityEngine;
using System.Collections;

public abstract class Stage : MonoBehaviour {

    //const
    const float normalTime = 20f;

    //variable
    protected static bool stageRunning;
    public static bool StageRunning {
        get {
            return stageRunning;
        }
        set {
            stageRunning = value;
        }
    }
    protected static bool DidJobHunt;
    float remainTime;
    protected WaitForSeconds waitForNormal;

    void Awake() {
        InitVariable();
    }

    void InitVariable() {
        stageRunning = false;
        DidJobHunt = false;
        waitForNormal = new WaitForSeconds(normalTime);
    }

    void Start() {
        BindFuncToEvent();
    }

    void BindFuncToEvent() {
        EventManager.instacne.AddFucToEventForDie(StopStage);
    }

    void StopStage() {
        StopAllCoroutines();
    }

	public void StartStage() {
        if(!stageRunning) {
            stageRunning = true;
            StartCoroutine("StageContent");
        }
    }

    protected abstract IEnumerator StageContent();

    protected WaitForSeconds WaitAndDisplayTimePerUnit(float time, float unit) {
        remainTime = time;
        StopCoroutine("CountDown");
        StartCoroutine("CountDown", unit);
        return new WaitForSeconds(time);
    }

    IEnumerator CountDown(float unit) {
        WaitForSeconds waitUnit = new WaitForSeconds(unit);
        UIManager.instance.SetRemainTimeActive(true);
        while (remainTime > 0f) {
            UIManager.instance.SetRemainTime(remainTime);
            remainTime -= unit;
            yield return waitUnit;
        }
        UIManager.instance.SetRemainTimeActive(false);
    }
}

public class ChildStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHILD);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHILD);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();

        StageManager.instance.CurrentStage = StageManager.StageType.STUDENT;

        stageRunning = false;
    }
}

public class StudentStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.STUDENT);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.STUDENT);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.CSAT);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class UniversityStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNIVERSITY);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNIVERSITY);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.JOBHUNT);
        DidJobHunt = true;
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class UnemployedStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNEMPLOYED);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNEMPLOYED);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();

        if(DidJobHunt) {
            EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.DARWINISM);
            DidJobHunt = false;
        } else {
            EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.JOBHUNT);
            DidJobHunt = true;
        }
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class WorkerStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.WORKER);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.WORKER);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.MARRIAGE);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class ChickenStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHICKEN);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHICKEN);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return waitForNormal;
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.MARRIAGE);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class SeniorStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return WaitAndDisplayTimePerUnit(2f, 0.1f);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.SENIOR);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return WaitAndDisplayTimePerUnit(2f, 0.1f);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.SENIOR);
        yield return WaitAndDisplayTimePerUnit(8f, 0.1f);
        EventManager.instacne.EventForSpecialEnd();

        stageRunning = false;
    }
}