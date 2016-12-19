using System.Collections.Generic;

public class EffectorManager {

    //variable
    List<Effector> effectorList;
    bool IsEffectorSetted = false;
    public bool GetIsEffectorSetted {
        get {
            return IsEffectorSetted;
        }
    }

    public EffectorManager(int effectInitNo = 5) {
        effectorList = new List<Effector>(effectInitNo);
    }

    public void RunAllEffector() {
        for (int i = 0; i < effectorList.Count; ++i) {
            effectorList[i].OperateEffect();
        }
    }

    public void AddEffector(Effector effector, object parent) {
        if (!IsEffectorSetted) {
            IsEffectorSetted = true;
        }
        effector.Parent = parent;
        effectorList.Add(effector);
    }

    public void RemoveEffector(System.Type type, int value) {
        for (int i = 0; i < effectorList.Count; ++i) {
            if (effectorList[i].IsSameEffectorWithTypeAndValue(type, value)) {
                effectorList.RemoveAt(i);
                break;
            }
        }
    }

    public void CopyAllEffectorFromManagerWithParent(EffectorManager other, object parent) {
        for (int i = 0; i < other.effectorList.Count; ++i) {
            Effector effector = other.effectorList[i].CopyWithoutParent();
            AddEffector(effector, parent);
        }
    }

    public void Clear() {
        effectorList.Clear();
    }
}