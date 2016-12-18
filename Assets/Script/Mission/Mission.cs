public class Mission {

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
    ConditionChecker conditionChecker;
    EffectorManager effectorManager = new EffectorManager(5);

    public Mission(string name, string detail, ConditionChecker conditionChecker) {
        this.name = name;
        this.detail = detail;
        this.conditionChecker = conditionChecker;
    }

    public Mission AddEffectorAndReturnMission(Effector effector) {
        effectorManager.AddEffector(effector, this);
        return this;
    }

    public void RemoveEffector(System.Type type, int value) {
        effectorManager.RemoveEffector(type, value);
    }

    public void GiveRewardIfSuccess() {
        if (conditionChecker.IsConditionSatisfied()) {
            effectorManager.RunAllEffector();
        }
    }
}