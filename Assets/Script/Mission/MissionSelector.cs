public abstract class MissionSelector {

    //variable
    protected Mission[] missionList;
    int missionNo;
    public int GetMissionNo {
        get {
            return missionNo;
        }
    }

    public MissionSelector() {
        missionNo = CreateMissionListAndReturnNo();
    }

    protected abstract int CreateMissionListAndReturnNo();

    public Mission SelectMission(int missionNo) {
        return missionList[missionNo];
    }
}

public class ChildMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("혼자서도 잘해요", "심부름에 성공하세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("깨끗이 씻어요", "깔끔하게 씻어요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("친구와 놀았어요", "놀이터에서 친구들과 함께 뛰어 놀아요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class StudentMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("사춘기가 왔어요", "내 마음대로 살 거예요. 다 비켜!",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
            new Mission("꿈을 찾아 떠나요", "이 세상은 틀렸어! 나만의 꿈을 찾도록 해요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("모범생이 될래요", "착실하게 공부할래요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class UniversityMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("죽음의 팀플", "팀플이 시작됩니다.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("벚꽃의 꽃말은 중간고사", "교수님으로부터 살아 남으세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
            new Mission("장학금을 받아요", "학자금 대출에서 벗어 나세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class UnemployedMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("돈을 법시다", "아르바이트를 구했어요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("돈을 탕진합시다", "인생은 한 방! 신나게 놀아요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
            new Mission("집이 좋아", "집이 최고예요. 방 구석에서 놀아요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class WorkerMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("서류 통과", "상사에게 칭찬 받으세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
            new Mission("이직하자!", "이직에 성공하세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(3)),
            new Mission("상사가 화났다", "보고서를 받은 상사가 화가 났어요. 도망치세요!",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class ChickenMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("치킨을 튀기자", "치킨을 튀겨서 팔아보아요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("고객을 만족시켜라", "화난 고객을 달래 매출을 올리세요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("사기를 조심해라", "사기꾼을 조심하세요. 한 방에 훅 갈 수도 있어요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class SeniorMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 5;
        Mission[] tempList = {
            new Mission("휴식", "휴식을 취하도록 해요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("연금", "안정적인 노후 생활을 준비해요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("우울증", "혼자 있다보니 마음이 아파요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
            new Mission("장례식", "마지막이에요.",
                new ItemNumberChecker(0, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new EndEffector(0)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class CSATMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 8;
        Mission[] tempList = {
            new Mission("수능", "대학 합격을 위해 달려요. 실패 시 백수!",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.UNIVERSITY))
            .AddEffectorAndReturnMission(new ScoreEffector(500))
            .AddEffectorAndReturnMission(new LifeEffector(1)),
            new Mission("수능실패", "대학 합격을 위해 달려요. 실패 시 백수!",
                new ItemNumberChecker(ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK, sucCondition - 1))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.UNEMPLOYED))
            .AddEffectorAndReturnMission(new ScoreEffector(-500))
            .AddEffectorAndReturnMission(new LifeEffector(-2)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class JobHuntMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 8;
        Mission[] tempList = {
            new Mission("취업준비", "직장에 들어가기 위해 달려요. 실패 시 백수!",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.WORKER))
            .AddEffectorAndReturnMission(new ScoreEffector(500))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
            new Mission("취업실패", "직장에 들어가기 위해 달려요. 실패 시 백수!",
                new ItemNumberChecker(ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK, sucCondition - 1))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.UNEMPLOYED))
            .AddEffectorAndReturnMission(new ScoreEffector(-500))
            .AddEffectorAndReturnMission(new LifeEffector(-2)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class DarwinismMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 8;
        Mission[] tempList = {
            new Mission("사회생존", "직장에서 살아남으세요. 실패하면 취킨을 튀겨요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.WORKER))
            .AddEffectorAndReturnMission(new ScoreEffector(1000))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
            new Mission("생존실패", "직장에서 살아남으세요. 실패하면 취킨을 튀겨요.",
                new ItemNumberChecker(ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK, sucCondition - 1))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.CHICKEN))
            .AddEffectorAndReturnMission(new ScoreEffector(-700))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}

public class MarriageMissionSelector : MissionSelector {

    protected override int CreateMissionListAndReturnNo() {
        const int sucCondition = 8;
        Mission[] tempList = {
            new Mission("결혼", "운명의 상대를 만나 귀여운 아이를 가지도록 해요.",
                new ItemNumberChecker(sucCondition, ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.CHILD))
            .AddEffectorAndReturnMission(new ScoreEffector(1000))
            .AddEffectorAndReturnMission(new LifeEffector(2)),
            new Mission("솔로", "운명의 상대를 만나 귀여운 아이를 가지도록 해요.",
                new ItemNumberChecker(ItemNumberChecker.MeaningOfNumber.DO_NOT_CHECK, sucCondition - 1))
            .AddEffectorAndReturnMission(new SetStageEffector((int)StageManager.StageType.SENIOR))
            .AddEffectorAndReturnMission(new ScoreEffector(-700))
            .AddEffectorAndReturnMission(new LifeEffector(-1)),
        };
        missionList = tempList;
        return missionList.Length;
    }
}