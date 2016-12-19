using UnityEngine;

public class SpriteManager {

    //variable
    SpriteSelector[] spriteSelector = new SpriteSelector[(int)StageManager.StageType.END];
    
    public SpriteManager() {
        SetSpriteSelector();
    }

    void SetSpriteSelector() {
        spriteSelector[(int)StageManager.StageType.CHILD] = new ChildSpriteSelector();
        spriteSelector[(int)StageManager.StageType.STUDENT] = new StudentSpriteSelector();
        spriteSelector[(int)StageManager.StageType.UNIVERSITY] = new UniversitySpriteSelector();
        spriteSelector[(int)StageManager.StageType.UNEMPLOYED] = new UnemployedSpriteSelector();
        spriteSelector[(int)StageManager.StageType.WORKER] = new WorkerSpriteSelector();
        spriteSelector[(int)StageManager.StageType.CHICKEN] = new ChickenSpriteSelector();
        spriteSelector[(int)StageManager.StageType.SENIOR] = new SeniorSpriteSelector();
    }

    public void SetRendererWithType(SpriteRenderer renderer, SpriteSelector.SpriteType type) {
        spriteSelector[(int)StageManager.instance.CurrentStage].SetRendererWithType(renderer, type);
    }
}
