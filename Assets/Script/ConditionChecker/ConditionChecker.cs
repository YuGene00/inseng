public abstract class ConditionChecker {

    public abstract bool IsConditionSatisfied();
}

public class ItemNumberChecker : ConditionChecker {

    //enum
    public enum MeaningOfNumber {
        DO_NOT_CHECK = -1
    }

    //variable
    int minNo = -1;
    int maxNo = -1;

    public ItemNumberChecker(int minNo, int maxNo) {
        InitMinMaxNo(minNo, maxNo);
    }

    public ItemNumberChecker(MeaningOfNumber minNo, int maxNo) {
        InitMinMaxNo((int)minNo, maxNo);
    }

    public ItemNumberChecker(int minNo, MeaningOfNumber maxNo) {
        InitMinMaxNo(minNo, (int)maxNo);
    }

    void InitMinMaxNo(int minNo, int maxNo) {
        this.minNo = minNo;
        this.maxNo = maxNo;
    }

    public override bool IsConditionSatisfied() {
        if ((minNo != -1 && MissionManager.instacne.GetMissionItemNo < minNo) || (maxNo != -1 && MissionManager.instacne.GetMissionItemNo > maxNo)) {
            return false;
        }

        return true;
    }
}