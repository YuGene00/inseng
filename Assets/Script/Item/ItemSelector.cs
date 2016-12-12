using UnityEngine;
using System.Collections;
using System.Text;

public abstract class ItemSelector {

    //variable
    protected ObjectPool[] objectPoolList;
    protected int itemNo;
    static StringBuilder strBuilder = new StringBuilder();

    public ItemSelector() {
        itemNo = GenerateItemList();
    }

    protected abstract int GenerateItemList();

    public GameObject SelectItem(int itemNo) {
        GameObject item = objectPoolList[itemNo].Retain();
        item.GetComponent<Item>().SetOwnObjectPool(objectPoolList[itemNo]);
        return item;
    }

    public int ItemNo() {
        return itemNo;
    }

    protected int GenerateObjPoolsWithPathAndNames(string path, string[] names) {
        int itemNo = names.Length;
        objectPoolList = new ObjectPool[itemNo];
        strBuilder.Length = 0;
        for (int i = 0; i < itemNo; ++i) {
            strBuilder.Append("Prefab/").Append(path).Append("/").Append(names[i]);
            objectPoolList[i] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load(strBuilder.ToString()) as GameObject);
        }
        return itemNo;
    }
}

public class SpineSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Enemy/Spine";
        string[] names = { "Spine" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class StarSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Normal/Star";
        string[] names = { "YellowStar", "RedStar" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class ChildSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Child";
        string[] names = { "Basket", "Toiletries", "Ball" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class StudentSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Student";
        string[] names = { "Gum", "DrawingPaper", "SelfTeachingBook" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class UniversitySelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/University";
        string[] names = { "Runner", "Assignment", "APlus" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class UnemployedSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Unemployed";
        string[] names = { "Mask", "Alcohol", "Ramen" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class WorkerSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Worker";
        string[] names = { "Document", "BusinessCard", "Superior" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class ChickenSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Chicken";
        string[] names = { "Chicken", "AngryCustomer", "Stock" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class SeniorSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Special/Senior";
        string[] names = { "Leaf", "BankBook", "DisappearMan", "Flower" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class CSATSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Branch/CSAT";
        string[] names = { "TextBook" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class JobHuntSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Branch/JobHunt";
        string[] names = { "Suit" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class DarwinismSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Branch/Darwinism";
        string[] names = { "Salary" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}

public class MarriageSelector : ItemSelector {

    protected override int GenerateItemList() {
        string path = "Branch/Marriage";
        string[] names = { "Bouquet" };
        return GenerateObjPoolsWithPathAndNames(path, names);
    }
}