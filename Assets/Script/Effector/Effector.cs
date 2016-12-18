public abstract class Effector {

    //variable
    protected int value;
    protected object parent;
    public object Parent {
        set {
            parent = value;
        }
    }

    public Effector(int value) {
        this.value = value;
    }
    public abstract void OperateEffect();

    public bool IsSameEffectorWithTypeAndValue(System.Type type, int value) {
        return (this.GetType() == type) && (this.value == value);
    }
}

public class DestroyItemEffector : Effector {

    //variable
    int effectNo = 0;

    public DestroyItemEffector(int value)
        : base(value) {
        
    }

    public override void OperateEffect() {
        if (value <= ++effectNo) {
            DestroyItem();
        }
    }

    void DestroyItem() {
        effectNo = 0;
        (parent as Item).DestroyItem();
    }
}

public class DamageEffector : Effector {

    public DamageEffector(int value)
        : base(value) {
        
    }

    public override void OperateEffect() {
        Player.instance.Damaged(value);
    }
}

public class ScoreEffector : Effector {

    public ScoreEffector(int value)
        : base(value) {
        
    }

    public override void OperateEffect() {
        ScoreManager.instance.AddScore(value);
    }
}

public class GainEffector : Effector {

    public GainEffector(int value)
        : base(value) {
        
    }

    public override void OperateEffect() {
        MissionManager.instacne.AddMissionItemNo(value);
    }
}

public class LifeEffector : Effector {

    public LifeEffector(int value)
        : base(value) {

    }

    public override void OperateEffect() {
        if(value < 0) {
            Player.instance.Damaged(value);
        } else {
            Player.instance.AddLife(value);
        }
    }
}

public class EndEffector : Effector {

    public EndEffector(int value)
        : base(value) {

    }

    public override void OperateEffect() {
        
    }
}

public class SetStageEffector : Effector {

    public SetStageEffector(int value)
        : base(value) {

    }

    public override void OperateEffect() {
        StageManager.instacne.CurrentStage = (StageManager.StageType)value;
    }
}