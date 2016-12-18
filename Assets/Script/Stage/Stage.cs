using UnityEngine;
using System.Collections;

public abstract class Stage : MonoBehaviour {

    //variable
    protected static bool stageRunning = false;
    public static bool StageRunning {
        get {
            return stageRunning;
        }
        set {
            stageRunning = value;
        }
    }
    protected bool DidJobHunt = false;

	public void StartStage() {
        if(!stageRunning) {
            stageRunning = true;
            StartCoroutine("StageContent");
        }
    }

    protected abstract IEnumerator StageContent();
}

public class ChildStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHILD);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHILD);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();

        stageRunning = false;
    }
}

public class StudentStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.STUDENT);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.STUDENT);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.CSAT);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class UniversityStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNIVERSITY);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNIVERSITY);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.JOBHUNT);
        DidJobHunt = true;
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class UnemployedStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNEMPLOYED);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.UNEMPLOYED);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();

        if(DidJobHunt) {
            EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.DARWINISM);
            DidJobHunt = false;
        } else {
            EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.JOBHUNT);
            DidJobHunt = true;
        }
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class WorkerStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.WORKER);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.WORKER);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.MARRIAGE);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class ChickenStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHICKEN);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.CHICKEN);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForBranchStart(EventManager.EventTypeForBranch.MARRIAGE);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForBranchEnd();

        stageRunning = false;
    }
}

public class SeniorStage : Stage {

    protected override IEnumerator StageContent() {
        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.SENIOR);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();
        EventManager.instacne.EventForSpecialStart(EventManager.EventTypeForSpecial.SENIOR);
        yield return new WaitForSeconds(5);
        EventManager.instacne.EventForSpecialEnd();

        EventManager.instacne.EventForNormalStart();
        yield return new WaitForSeconds(20);
        EventManager.instacne.EventForNormalEnd();

        stageRunning = false;
    }
}