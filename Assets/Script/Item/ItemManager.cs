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
    ItemSelector[] enemySelector = new ItemSelector[(int)EventManager.EventTypeForEnemy.END];
    ItemSelector[] normalSelector = new ItemSelector[(int)EventManager.EventTypeForNormal.END];
    ItemSelector[] specialSelector = new ItemSelector[(int)EventManager.EventTypeForSpecial.END];
    ItemSelector[] branchSelector = new ItemSelector[(int)EventManager.EventTypeForBranch.END];
    delegate WaitForSeconds ReturnWaitFunc();
    ReturnWaitFunc createEnemyDelegate;
    ReturnWaitFunc createItemDelegate;
    WaitForSeconds waitForEnemy;
    WaitForSeconds waitForNormal;
    WaitForSeconds waitForSpecial;
    WaitForSeconds waitForBranch;

    void Awake() {
        Initialize();
    }

    void Start() {
        InitDelegate();
        BindFuncToEvent();
        RunCoroutine();
    }

    void Initialize() {
        SetEnemySelectors();
        SetNormalSelectors();
        SetSpecialSelectors();
        SetBranchSelectors();
        SetWaitForSeconds();
    }

    void SetEnemySelectors() {
        enemySelector[(int)EventManager.EventTypeForEnemy.SPINE] = new SpineItemSelector();
    }

    void SetNormalSelectors() {
        normalSelector[(int)EventManager.EventTypeForNormal.STAR] = new StarItemSelector();
    }

    void SetSpecialSelectors() {
        specialSelector[(int)EventManager.EventTypeForSpecial.CHILD] = new ChildItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.STUDENT] = new StudentItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNIVERSITY] = new UniversityItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.UNEMPLOYED] = new UnemployedItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.WORKER] = new WorkerItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.CHICKEN] = new ChickenItemSelector();
        specialSelector[(int)EventManager.EventTypeForSpecial.SENIOR] = new SeniorItemSelector();
    }

    void SetBranchSelectors() {
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
            if(createEnemyDelegate == null) {
                yield return null;
            } else {
                yield return createEnemyDelegate();
            }
        }
    }

    IEnumerator CreateItem() {
        while (true) {
            if (createItemDelegate == null) {
                yield return null;
            } else {
                yield return createItemDelegate();
            }
        }
    }

    WaitForSeconds CreateEnemyAndReturnWait() {
        CreateItemWithSelector(0, enemySelector[(int)EventManager.instacne.GetCurrentEventForEnemy]);
        return waitForEnemy;
    }

    WaitForSeconds CreateNormalAndReturnWait() {
        float dice = Random.Range(0f, 1f);
        if (dice > 0.3f) {
            CreateItemWithSelector((int)StarType.YELLOW, normalSelector[EventManager.instacne.GetCurrentEvent.detail]);
        } else {
            CreateItemWithSelector((int)StarType.RED, normalSelector[0]);
        }
        return waitForNormal;
    }

    WaitForSeconds CreateSpecialAndReturnWait() {
        CreateItemWithSelector(MissionManager.instacne.GetCurrentMission.index, specialSelector[EventManager.instacne.GetCurrentEvent.detail]);
        return waitForSpecial;
    }

    WaitForSeconds CreateBranchAndReturnWait() {
        CreateItemWithSelector(MissionManager.instacne.GetCurrentMission.index, branchSelector[EventManager.instacne.GetCurrentEvent.detail]);
        return waitForBranch;
    }

    void CreateItemWithSelector(int itemNo, ItemSelector itemSelector) {
        if(itemNo == (int)MissionManager.MeaningOfNumber.NO_MISSION) {
            return;
        }

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