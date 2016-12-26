using UnityEngine;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

    //singleton
    public static MissionManager instacne = null;

    //enum
    public enum MeaningOfNumber {
        NO_MISSION = -1
    }

    //struct
    public struct MissionInfo {
        public int index;
        public Mission mission;

        public MissionInfo(int index, Mission mission) {
            this.index = index;
            this.mission = mission;
        }
    }

    //variable
    List<MissionInfo> currentMission = new List<MissionInfo>();
    MissionInfo emptyMission = new MissionInfo((int)MeaningOfNumber.NO_MISSION, null);
    public MissionInfo GetCurrentMission {
        get {
            if(currentMission.Count == 0) {
                return emptyMission;
            }
            return currentMission[0];
        }
    }
    MissionSelector[] specialSelector = new MissionSelector[(int)EventManager.EventTypeForSpecial.END];
    MissionSelector[] branchSelector = new MissionSelector[(int)EventManager.EventTypeForBranch.END];
    int missionItemNo = 0;
    public int GetMissionItemNo {
        get {
            return missionItemNo;
        }
    }
    bool funeralFlag = false;

    void Awake() {
        instacne = this;
    }

    void Start() {
        Initialize();
    }

    void Initialize() {
        ResetCurrentMission();
        SetSpecialSelectors();
        SetBranchSelectors();
        BindFuncToEvent();
    }

    void ResetCurrentMission() {
        missionItemNo = 0;
        currentMission.Clear();
    }

    void SetSpecialSelectors() {
        specialSelector[(int)EventManager.EventTypeForSpecial.CHILD] = new ChildMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.STUDENT] = new StudentMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNIVERSITY] = new UniversityMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNEMPLOYED] = new UnemployedMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.WORKER] = new WorkerMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.CHICKEN] = new ChickenMissionSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.SENIOR] = new SeniorMissionSelector();
    }

    void SetBranchSelectors() {
        branchSelector[(int)EventManager.EventTypeForBranch.CSAT] = new CSATMissionSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.JOBHUNT] = new JobHuntMissionSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.DARWINISM] = new DarwinismMissionSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.MARRIAGE] = new MarriageMissionSelector();
    }

    void BindFuncToEvent() {
        EventManager.instacne.AddFuncToEventForStart(SelectSpecialMission, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForStart(DisplaySpecialMission, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForEnd(CheckMissionPerformanceAndReset, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForStart(SelectBranchMission, EventManager.EventType.BRANCH);
        EventManager.instacne.AddFuncToEventForStart(DisplayBranchMission, EventManager.EventType.BRANCH);
        EventManager.instacne.AddFuncToEventForEnd(CheckMissionPerformanceAndReset, EventManager.EventType.BRANCH);
    }

    void SelectSpecialMission() {
        if (StageManager.instance.CurrentStage == StageManager.StageType.SENIOR) {
            SelectMissionForSenior();
        }
        SelectRandomSpecial(specialSelector[EventManager.instacne.GetCurrentEvent.detail].GetMissionNo);
    }

    void SelectMissionForSenior() {
        if (!funeralFlag) {
            SelectRandomSpecial(specialSelector[EventManager.instacne.GetCurrentEvent.detail].GetMissionNo - 1);
            funeralFlag = true;
        } else {
            int funeralIndex = specialSelector[EventManager.instacne.GetCurrentEvent.detail].GetMissionNo - 1;
            AddMission(funeralIndex, specialSelector[EventManager.instacne.GetCurrentEvent.detail].SelectMission(funeralIndex));
            funeralFlag = false;
        }
    }

    void SelectRandomSpecial(int missionNo) {
        int dice = Random.Range(0, missionNo);
        AddMission(dice, specialSelector[EventManager.instacne.GetCurrentEvent.detail].SelectMission(dice));
    }

    void AddMission(int index, Mission mission) {
        MissionInfo tempMissionInfo = new MissionInfo();
        tempMissionInfo.index = index;
        tempMissionInfo.mission = mission;
        currentMission.Add(tempMissionInfo);
    }

    void SelectBranchMission() {
        for(int i = 0; i < branchSelector[EventManager.instacne.GetCurrentEvent.detail].GetMissionNo; ++i) {
            MissionInfo tempMissionInfo = new MissionInfo();
            tempMissionInfo.index = 0;
            tempMissionInfo.mission = branchSelector[EventManager.instacne.GetCurrentEvent.detail].SelectMission(i);
            currentMission.Add(tempMissionInfo);
        }
    }

    void DisplaySpecialMission() {
        UIManager.instance.SetMissionNameAndDetail(currentMission[0].mission.GetName, currentMission[0].mission.GetDetail);
        UIManager.instance.SetMissionAlarmWithType(UIManager.AlarmType.SPECIAL);
    }

    void DisplayBranchMission() {
        UIManager.instance.SetMissionNameAndDetail(currentMission[0].mission.GetName, currentMission[0].mission.GetDetail);
        UIManager.instance.SetMissionAlarmWithType(UIManager.AlarmType.BRANCH);
    }

    void CheckMissionPerformanceAndReset() {
        for(int i = 0; i < currentMission.Count; ++i) {
            currentMission[i].mission.GiveRewardIfSuccess();
        }
        ResetCurrentMission();
    }

    public void AddMissionItemNo(int value) {
        missionItemNo += value;
    }
}