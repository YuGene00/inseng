using System.Collections.Generic;

public class EffectorManager {

    //variable
    List<Effector> effectList;

    public EffectorManager(int effectInitNo = 5) {
        effectList = new List<Effector>(effectInitNo);
    }

    public void RunAllEffector() {
        for (int i = 0; i < effectList.Count; ++i) {
            effectList[i].OperateEffect();
        }
    }

    public void AddEffector(Effector effector, object parent) {
        effector.Parent = parent;
        effectList.Add(effector);
    }

    public void RemoveEffector(System.Type type, int value) {
        for (int i = 0; i < effectList.Count; ++i) {
            if (effectList[i].IsSameEffectorWithTypeAndValue(type, value)) {
                effectList.RemoveAt(i);
                break;
            }
        }
    }
}