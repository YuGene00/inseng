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

    public abstract Effector CopyWithoutParent();

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

    public override Effector CopyWithoutParent() {
        return new DestroyItemEffector(value);
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

    public override Effector CopyWithoutParent() {
        return new DamageEffector(value);
    }

    public override void OperateEffect() {
        Player.instance.Damaged(value);
    }
}

public class ScoreEffector : Effector {

    public ScoreEffector(int value)
        : base(value) {
        
    }

    public override Effector CopyWithoutParent() {
        return new ScoreEffector(value);
    }

    public override void OperateEffect() {
        ScoreManager.instance.AddScore(value);
    }
}

public class GainEffector : Effector {

    public GainEffector(int value)
        : base(value) {
        
    }

    public override Effector CopyWithoutParent() {
        return new GainEffector(value);
    }

    public override void OperateEffect() {
        MissionManager.instacne.AddMissionItemNo(value);
    }
}

public class LifeEffector : Effector {

    public LifeEffector(int value)
        : base(value) {

    }

    public override Effector CopyWithoutParent() {
        return new LifeEffector(value);
    }

    public override void OperateEffect() {
        Player.instance.AddLife(value);
    }
}

public class EndEffector : Effector {

    public EndEffector(int value)
        : base(value) {

    }

    public override Effector CopyWithoutParent() {
        return new EndEffector(value);
    }

    public override void OperateEffect() {
        Player.instance.Damaged(99);
    }
}

public class SetStageEffector : Effector {

    public SetStageEffector(int value)
        : base(value) {

    }

    public override Effector CopyWithoutParent() {
        return new SetStageEffector(value);
    }

    public override void OperateEffect() {
        StageManager.instance.CurrentStage = (StageManager.StageType)value;
    }
}