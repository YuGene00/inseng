public class MissionManager {

    //struct
    struct MissionInfo {
        string name;
        string detail;
        int rewardLife;
        public MissionInfo(string name, string detail, int rewardLife) {
            this.name = name;
            this.detail = detail;
            this.rewardLife = rewardLife;
        }
    }

    //variable
    int currentMission;
    public int CurrentMission {
        get {
            return currentMission;
        }
    }
    MissionInfo[][] missionInfoForSpecial;
    MissionInfo[][] missionInfoForBranch;

    public MissionManager() {
        CreateMissionList();
    }

    void CreateMissionList() {
        missionInfoForSpecial = new MissionInfo[(int)EventManager.EventTypeForSpecial.END][];
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.CHILD] = new MissionInfo[] {
            new MissionInfo ("혼자서도 잘해요", "심부름에 성공하자", 1),
            new MissionInfo ("깨끗이 씻어요", "깔끔하게 씻자", 1),
            new MissionInfo ("친구와 놀았어요", "놀이터에서 친구들과 함께 뛰어놀자", 1)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.STUDENT] = new MissionInfo[] {
            new MissionInfo ("사춘기가 왔어요", "내 마음대로 살테다", -1),
            new MissionInfo ("꿈을 찾아 떠나요", "이 세상은 틀렸어. 난 나만의 꿈을 찾겠어!", 1),
            new MissionInfo ("모범생이 될래요", "착실하게 공부하자", 1)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.UNIVERSITY] = new MissionInfo[] {
            new MissionInfo ("죽음의 팀플", "팀플이 시작됩니다", 1),
            new MissionInfo ("벚꽃의 꽃말은 중간고사", "교수님으로부터 살아남으세요", -1),
            new MissionInfo ("장학금을 받아요", "학자금 대출에서 벗어나세요", 2)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.UNEMPLOYED] = new MissionInfo[] {
            new MissionInfo ("돈을 법시다", "아르바이트를 구했어요", 1),
            new MissionInfo ("돈을 탕진합시다", "인생은 한 방!", -1),
            new MissionInfo ("집이 좋아", "집에만 머무릅시다", 2)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.WORKER] = new MissionInfo[] {
            new MissionInfo ("서류 통과", "상사의 눈에 들어라", 2),
            new MissionInfo ("이직하자!", "이직에 성공해라", 3),
            new MissionInfo ("상사가 화났다", "상사로부터 도망쳐라", -1)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.CHICKEN] = new MissionInfo[] {
            new MissionInfo ("치킨을 튀기자", "치킨을 튀겨서 팔자", 1),
            new MissionInfo ("고객을 만족시켜라", "화난 고객을 달래주자", 1),
            new MissionInfo ("사기를 조심해라", "사기꾼을 조심하자", -1)
        };
        missionInfoForSpecial[(int)EventManager.EventTypeForSpecial.SENIOR] = new MissionInfo[] {
            new MissionInfo ("휴식", "휴식을 취하자", 1),
            new MissionInfo ("연금", "안정적인 노후생활", 1),
            new MissionInfo ("장례식", "장례식 참석", -99)
        };

        missionInfoForBranch = new MissionInfo[(int)EventManager.EventTypeForBranch.END][];
        missionInfoForBranch[(int)EventManager.EventTypeForBranch.CSAT] = new MissionInfo[] {
            new MissionInfo ("수능 보기", "", 1),
        };
    }
}

public abstract class MissionSelector {

    //variable
    protected Mission[] missionList;
    int missionNo;
    public int MissionNo {
        get {
            return missionNo;
        }
    }

    public MissionSelector() {

    }

    protected abstract int CreateMissionList();
}

public class ChildMissionSelector : MissionSelector {

    protected override int CreateMissionList() {
        Mission[] tempList = {
            new ConstantMission("혼자서도 잘해요", "심부름에 성공하세요."),
            new ConstantMission("깨끗이 씻어요", "깔끔하게 씻어요."),
            new ConstantMission("친구와 놀았어요", "놀이터에서 친구들과 함께 뛰어 놀아요.")
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class StudentMissionSelector : MissionSelector {

    protected override int CreateMissionList() {
        Mission[] tempList = {
            new ConstantMission("사춘기가 왔어요", "내 마음대로 살 거예요. 다 비켜!"),
            new ConstantMission("꿈을 찾아 떠나요", "이 세상은 틀렸어! 나만의 꿈을 찾도록 해요."),
            new ConstantMission("모범생이 될래요", "착실하게 공부할래요.")
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class UniversityMissionSelector : MissionSelector {

    protected override int CreateMissionList() {
        Mission[] tempList = {
            new ConstantMission("죽음의 팀플", "팀플이 시작됩니다."),
            new ConstantMission("벚꽃의 꽃말은 중간고사", "교수님으로부터 살아 남으세요."),
            new ConstantMission("장학금을 받아요", "학자금 대출에서 벗어 나세요.")
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class UnemployedMissionSelector : MissionSelector {

    protected override int CreateMissionList() {
        Mission[] tempList = {
            new ConstantMission("돈을 법시다", "아르바이트를 구했어요."),
            new ConstantMission("돈을 탕진합시다", "인생은 한 방! 신나게 놀아요."),
            new ConstantMission("집이 좋아", "집이 최고예요. 방 구석에서 놀아요.")
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public abstract class Mission {

    //variable
    string name;
    public string GetName {
        get {
            return name;
        }
    }
    string detail;
    public string GetDetail {
        get {
            return detail;
        }
    }

    public Mission(string name, string detail) {
        this.name = name;
        this.detail = detail;
    }

    public abstract void GiveRewardByCondition();
}

public class ConstantMission : Mission {

    //const
    const int sucCondition = 15;

    //variable
    int rewardLife;
    public int SetRewardLife {
        set {
            rewardLife = value;
        }
    }

    public ConstantMission(string name, string detail)
        : base(name, detail) {

    }

    public override void GiveRewardByCondition() {
        
    }
}