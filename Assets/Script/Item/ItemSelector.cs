using UnityEngine;
using System.Collections;

public abstract class ItemSelector {

    //variable
    protected ObjectPool[] objectPoolList;
    protected int itemNo;

    public ItemSelector() {
        itemNo = GenerateItemList();
    }

    protected abstract int GenerateItemList();

    public GameObject SelectItem(int itemNo) {
        GameObject item = objectPoolList[itemNo].Retain();
        item.GetComponent<Item>().SetOwnObjectPool(objectPoolList[itemNo]);
        return item;
    }
}

public class StarSelector : ItemSelector {

    //const
    const float activeChance = 0.3f;

    protected override int GenerateItemList() {
        int itemNo = 2;
        objectPoolList = new ObjectPool[2];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Child/Basket") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Child/Toiletries") as GameObject);
        return itemNo;
    }
}

public class ChildSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Child/Basket") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Child/Toiletries") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Child/Ball") as GameObject);
        return itemNo;
    }
}

public class StudentSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Student/Gum") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Student/DrawingPaper") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Student/SelfTeachingBook") as GameObject);
        return itemNo;
    }
}

public class UniversitySelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/University/Runner") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/University/Assignment") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/University/APlus") as GameObject);
        return itemNo;
    }
}

public class UnemployedSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Unemployed/Mask") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Unemployed/Alcohol") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Unemployed/Ramen") as GameObject);
        return itemNo;
    }
}

public class WorkerSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Worker/Document") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Worker/BusinessCard") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Worker/Superior") as GameObject);
        return itemNo;
    }
}

public class ChickenSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 3;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Chicken/Chicken") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Chicken/AngryCustomer") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Chicken/Stock") as GameObject);
        return itemNo;
    }
}

public class SeniorSelector : ItemSelector {

    protected override int GenerateItemList() {
        int itemNo = 4;
        objectPoolList = new ObjectPool[itemNo];
        objectPoolList[0] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Senior/Leaf") as GameObject);
        objectPoolList[1] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Senior/BankBook") as GameObject);
        objectPoolList[2] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Senior/DisappearMan") as GameObject);
        objectPoolList[3] = ObjectPool.MakePoolOfObjWithNumber(Resources.Load("Prefab/Senior/Flower") as GameObject);
        return itemNo;
    }
}