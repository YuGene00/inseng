using UnityEngine;
using System.Text;

public abstract class SpriteSelector {

    //enum
    public enum SpriteType {
        NORMAL, HAPPY, SAD, DROP, END
    }

    //variable
    protected Sprite[] spriteList = new Sprite[(int)SpriteType.END];
    protected static StringBuilder strBuilder = new StringBuilder();
    protected static string[] typeString = {
        "Normal", "Happy", "Sad", "Drop"
    };


    public SpriteSelector() {
        CreateSpriteList();
    }

    protected abstract void CreateSpriteList();

    protected Sprite LoadSpriteWithPathAndName(string path, string name) {
        strBuilder.Length = 0;
        strBuilder.Append("Image/Character/").Append(path).Append("/").Append(name);
        return Resources.Load<Sprite>(strBuilder.ToString());
    }

    public SpriteRenderer SetRendererWithType(SpriteRenderer renderer, SpriteType spriteType) {
        renderer.sprite = spriteList[(int)spriteType];
        return renderer;
    }
}

public class ChildSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Child";

        for(int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class StudentSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Student";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class UniversitySpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "University";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class UnemployedSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Unemployed";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class WorkerSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Worker";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class ChickenSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Chicken";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}

public class SeniorSpriteSelector : SpriteSelector {

    protected override void CreateSpriteList() {
        string path = "Senior";

        for (int i = 0; i < (int)SpriteType.END; ++i) {
            spriteList[i] = LoadSpriteWithPathAndName(path, typeString[i]);
        }
    }
}