using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //enum
    enum StarType {
        YELLOW, RED
    } 

    //inspector
    public Transform createZoneTrans;
    public float enemyItemPeriod = 0.65f;
    public float normalItemPeriod = 0.5f;
    public float specialItemPeriod = 0.8f;
    public float branchItemPeriod = 0.8f;
    public float redStarChance = 0.7f;

    //variable
    ItemSelector[] enemySelector;
    ItemSelector[] normalSelector;
    ItemSelector[] specialSelector;
    ItemSelector[] branchSelector;
    delegate WaitForSeconds ReturnWaitFunc();
    ReturnWaitFunc createEnemyDelegate;
    ReturnWaitFunc createItemDelegate;
    WaitForSeconds waitForEnemy;
    WaitForSeconds waitForNormal;
    WaitForSeconds waitForSpecial;
    WaitForSeconds waitForBranch;

    void Start() {
        Initialize();
        RunCoroutine();
    }

    void Initialize() {
        SetEnemySelectors();
        SetNormalSelectors();
        SetSpecialSelectors();
        SetBranchSelectors();
        SetWaitForSeconds();
        BindFuncToEvent();
        InitDelegate();
    }

    void SetEnemySelectors() {
        enemySelector = new ItemSelector[(int)EventManager.EventTypeForEnemy.END];
        enemySelector[(int)EventManager.EventTypeForEnemy.SPINE] = new SpineItemSelector();
    }

    void SetNormalSelectors() {
        normalSelector = new ItemSelector[(int)EventManager.EventTypeForNormal.END];
        normalSelector[(int)EventManager.EventTypeForNormal.STAR] = new StarItemSelector();
    }

    void SetSpecialSelectors() {
        specialSelector = new ItemSelector[(int)EventManager.EventTypeForSpecial.END];
        specialSelector[(int)EventManager.EventTypeForSpecial.CHILD] = new ChildItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.STUDENT] = new StudentItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNIVERSITY] = new UniversityItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNEMPLOYED] = new UnemployedItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.WORKER] = new WorkerItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.CHICKEN] = new ChickenItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.SENIOR] = new SeniorItemSelector();
    }

    void SetBranchSelectors() {
        branchSelector = new ItemSelector[(int)EventManager.EventTypeForBranch.END];
        branchSelector[(int)EventManager.EventTypeForBranch.CSAT] = new CSATItemSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.JOBHUNT] = new JobHuntItemSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.DARWINISM] = new DarwinismItemSelector();
        branchSelector[(int)EventManager.EventTypeForBranch.MARRIAGE] = new MarriageItemSelector();
    }

    void SetWaitForSeconds() {
        waitForEnemy = new WaitForSeconds(enemyItemPeriod);
        waitForNormal = new WaitForSeconds(normalItemPeriod);
        waitForSpecial = new WaitForSeconds(specialItemPeriod);
        waitForBranch = new WaitForSeconds(branchItemPeriod);
    }

    void BindFuncToEvent() {
        EventManager.instacne.AddFuncToEventForStart(SetDelegateToEnemy, EventManager.EventType.ENEMY);
        EventManager.instacne.AddFuncToEventForStart(SetDelegateToNormal, EventManager.EventType.NORMAL);
        EventManager.instacne.AddFuncToEventForStart(SetDelegateToSpecial, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForStart(SetDelegateToBranch, EventManager.EventType.BRANCH);
    }

    void SetDelegateToEnemy() {
        createEnemyDelegate = CreateEnemyAndReturnWait;
    }

    void SetDelegateToNormal() {
        createItemDelegate = CreateNormalAndReturnWait;
    }

    void SetDelegateToSpecial() {
        createItemDelegate = CreateSpecialAndReturnWait;
    }

    void SetDelegateToBranch() {
        createItemDelegate = CreateBranchAndReturnWait;
    }

    void InitDelegate() {
        CreateEnemyAndReturnWait();
        CreateNormalAndReturnWait();
    }

    void RunCoroutine() {
        StartCoroutine("CreateEnemyItem");
        StartCoroutine("CreateItem");
    }

    IEnumerator CreateEnemyItem() {
        while (true) {
            yield return createEnemyDelegate();
        }
    }

    IEnumerator CreateItem() {
        while (true) {
            yield return createItemDelegate;
        }
    }

    WaitForSeconds CreateEnemyAndReturnWait() {
        CreateItemWithSelector(0, enemySelector[(int)EventManager.instacne.GetCurrentEventForEnemy]);
        return waitForEnemy;
    }

    WaitForSeconds CreateNormalAndReturnWait() {
        float dice = Random.Range(0f, 1f);
        if (dice > 0.3f) {
            CreateItemWithSelector((int)StarType.YELLOW, normalSelector[(int)EventManager.instacne.GetCurrentEvent.detail]);
        } else {
            CreateItemWithSelector((int)StarType.RED, normalSelector[0]);
        }
        return waitForNormal;
    }

    WaitForSeconds CreateSpecialAndReturnWait() {
        CreateItemWithSelector(EventManager.instacne.Mission.CurrentMission, specialSelector[(int)EventManager.instacne.GetCurrentEvent.detail]);
        return waitForSpecial;
    }

    WaitForSeconds CreateBranchAndReturnWait() {
        CreateItemWithSelector(EventManager.instacne.Mission.CurrentMission, branchSelector[(int)EventManager.instacne.GetCurrentEvent.detail]);
        return waitForBranch;
    }

    void CreateItemWithSelector(int itemNo, ItemSelector itemSelector) {
        Vector2 genPoint = SelectGenPoint();
        GameObject item = itemSelector.SelectItem(itemNo);
        item.transform.position = genPoint;
    }

    Vector2 SelectGenPoint() {
        Vector2 genPoint;
        float halfZoneXScale = createZoneTrans.localScale.x / 2;
        genPoint.x = Random.Range(-halfZoneXScale, halfZoneXScale);
        genPoint.y = createZoneTrans.position.y;
        return genPoint;
    }
}