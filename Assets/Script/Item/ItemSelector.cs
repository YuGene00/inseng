using UnityEngine;
using System.Collections;
using System.Text;

public abstract class ItemSelector {

    //const
    protected const int normalScore = 10;

    //variable
    ObjectPool[] objectPoolList;
    int itemNo;
    public int ItemNo {
        get {
            return itemNo;
        }
    }
    static StringBuilder strBuilder = new StringBuilder();

    public ItemSelector() {
        itemNo = CreateItemListAndReturnNo();
    }

    protected abstract int CreateItemListAndReturnNo();

    public GameObject SelectItem(int itemNo) {
        GameObject item = objectPoolList[itemNo].Retain();
        item.GetComponent<Item>().SetOwnObjectPool(objectPoolList[itemNo]);
        return item;
    }

    protected Item CreateItemWithPathAndName(string path, string name) {
        strBuilder.Length = 0;
        strBuilder.Append("Prefab/").Append(path).Append("/").Append(name);
        GameObject obj = Resources.Load(strBuilder.ToString()) as GameObject;
        Item item = obj.AddComponent<Item>();
        item.ItemName = name;
        return item;
    }

    protected int CreateObjPoolsWithItems(Item[] itemList) {
        int itemNo = itemList.Length;
        objectPoolList = new ObjectPool[itemNo];
        for (int i = 0; i < itemNo; ++i) {
            objectPoolList[i] = ObjectPool.CreatePoolOfObjWithNumber(itemList[i].gameObject);
        }
        return itemNo;
    }
}

public class SpineItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Enemy/Spine";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Spine")
            .AddEffectorAndReturnItem(new DamageEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class StarItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Normal/Star";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "YellowStar")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore)),
            CreateItemWithPathAndName(path, "RedStar")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 5)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class ChildItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Child";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Basket")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Toiletries")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Ball")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class StudentItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Student";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Gum")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -8))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "DrawingPaper")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "SelfTeachingBook")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class UniversityItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/University";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Runner")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Assignment")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -8))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "APlus")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 20))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class UnemployedItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Unemployed";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Mask")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Alcohol")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -8))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Ramen")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -8))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class WorkerItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Worker";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Document")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 20))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "BusinessCard")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 20))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Superior")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -20))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class ChickenItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Chicken";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Chicken")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "AngryCustomer")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Stock")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class SeniorItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Special/Senior";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Leaf")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "BankBook")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 10))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "DisappearMan")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * -8))
            .AddEffectorAndReturnItem(new GainEffector(1)),
            CreateItemWithPathAndName(path, "Flower"),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class CSATItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Branch/CSAT";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "TextBook")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 30))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class JobHuntItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Branch/JobHunt";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Suit")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 30))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class DarwinismItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Branch/Darwinism";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Salary")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 30))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}

public class MarriageItemSelector : ItemSelector {

    protected override int CreateItemListAndReturnNo() {
        string path = "Branch/Marriage";

        Item[] itemList = {
            CreateItemWithPathAndName(path, "Bouquet")
            .AddEffectorAndReturnItem(new DestroyItemEffector(1))
            .AddEffectorAndReturnItem(new ScoreEffector(normalScore * 30))
            .AddEffectorAndReturnItem(new GainEffector(1)),
        };

        return CreateObjPoolsWithItems(itemList);
    }
}