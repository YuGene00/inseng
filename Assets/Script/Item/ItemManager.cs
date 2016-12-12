using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //inspector
    public Transform createZoneTrans;
    public float enemyItemPeriod = 0.65f;
    public float normalItemPeriod = 0.5f;
    public float specialItemPeriod = 0.8f;
    public float branchItemPeriod = 0.8f;

    //variable
    ItemSelector[] enemySelector;
    ItemSelector[] normalSelector;
    ItemSelector[] specialSelector;
    ItemSelector[] branchSelector;
    int specialEventType;

    int specialItem;
    int oldCount = 0;
    int sectionNumber = 0;

    void Start() {
        Initialize();
    }

    void Initialize() {
        SetEnemySelectors();
        SetNormalSelectors();
        SetSpecialSelectors();
        SetBranchSelectors();
    }

    void SetEnemySelectors() {
        enemySelector = new ItemSelector[1];
        enemySelector[0] = new SpineSelector();
    }

    void SetNormalSelectors() {
        normalSelector = new ItemSelector[1];
        normalSelector[0] = new StarSelector();
    }

    void SetSpecialSelectors() {
        specialSelector = new ItemSelector[(int)EventManager.SpecialType.END];
        specialSelector[(int)EventManager.SpecialType.CHILD] = new ChildSelector();
        specialSelector[(int)EventManager.SpecialType.STUDENT] = new StudentSelector();
        specialSelector[(int)EventManager.SpecialType.UNIVERSITY] = new UniversitySelector();
        specialSelector[(int)EventManager.SpecialType.UNEMPLOYED] = new UnemployedSelector();
        specialSelector[(int)EventManager.SpecialType.WORKER] = new WorkerSelector();
        specialSelector[(int)EventManager.SpecialType.CHICKEN] = new ChickenSelector();
        specialSelector[(int)EventManager.SpecialType.SENIOR] = new SeniorSelector();
    }

    void SetBranchSelectors() {
        branchSelector = new ItemSelector[(int)EventManager.BranchType.END];
        branchSelector[(int)EventManager.BranchType.CSAT] = new CSATSelector();
        branchSelector[(int)EventManager.BranchType.JOBHUNT] = new JobHuntSelector();
        branchSelector[(int)EventManager.BranchType.DARWINISM] = new DarwinismSelector();
        branchSelector[(int)EventManager.BranchType.MARRIAGE] = new MarriageSelector();
    }

    void EnemyStart() {
        enemySelector = spineSelector;
        StartCoroutine("CreateEnemyItem");
    }

    void NormalStart() {
        normalSelector = starSelector;
        StopCoroutine("CreateSpecialItem");
        StopCoroutine("CreateFlower");
        StopCoroutine("CreateSectionItem");
        StartCoroutine("CreateNormalItem");
    }

    void SpecialStart() {
        specialItem = Random.Range(0, 3);
        ChangeSpecialSelector();
        StopCoroutine("CreateNormalItem");
        StopCoroutine("CreateFlower");
        StopCoroutine("CreateSectionItem");
        if (GameController.GetInstance().nowStage == GameController.JOBSTAGE.SENIOR_STAGE) {
            if(oldCount <= 0) {
                StartCoroutine("CreateSpecialItem");
                ++oldCount;
            } else {
                oldCount = 0;
                StopCoroutine("CreateSpecialItem");
                StartCoroutine("CreateFlower");
            }
        } else {
            StartCoroutine("CreateSpecialItem");
        }
    }

    void SectionStart() {
        StopCoroutine("CreateSpecialItem");
        StopCoroutine("CreateFlower");
        StopCoroutine("CreateNormalItem");
        StartCoroutine("CreateSectionItem");
        if(sectionNumber <= 4) {
            sectionNumber = 0;
        } else {
            ++sectionNumber;
        }
    }

    void ChangeSpecialSelector() {
        switch (GameController.GetInstance().nowStage) {
            case GameController.JOBSTAGE.CHILD_STAGE:
                specialSelector = childSelector;
                break;
            case GameController.JOBSTAGE.STUDENT_STAGE:
                specialSelector = studentSelector;
                break;
            case GameController.JOBSTAGE.UNIVERSITY_STAGE:
                specialSelector = universitySelector;
                break;
            case GameController.JOBSTAGE.UNEMPLOYED_STAGE_0:
            case GameController.JOBSTAGE.UNEMPLOYED_STAGE_1:
                specialSelector = unemploySelector;
                break;
            case GameController.JOBSTAGE.WORKER_STAGE_0:
            case GameController.JOBSTAGE.WORKER_STAGE_1:
                specialSelector = workerSelector;
                break;
            case GameController.JOBSTAGE.CHICKEN_STAGE:
                specialSelector = chickenSelector;
                break;
            case GameController.JOBSTAGE.SENIOR_STAGE:
                specialSelector = seniorSelector;
                break;
        }
    } 

    IEnumerator CreateEnemyItem() {
        while(true) {
            CreateItemWithSelector(enemySelector, 0);
            yield return new WaitForSeconds(enemyPeriod);
        }
    }

    IEnumerator CreateNormalItem() {
        while(true) {
            float dice = Random.Range(0f, 1f);
            if(dice > 0.3f) {
                CreateItemWithSelector(normalSelector, 0);
            } else {
                CreateItemWithSelector(normalSelector, 1);
            }
            yield return new WaitForSeconds(normalPeriod);
        }
    }

    IEnumerator CreateSpecialItem() {
        while (true) {
            CreateItemWithSelector(specialSelector, specialItem);
            yield return new WaitForSeconds(specialPeriod);
        }
    }

    IEnumerator CreateSectionItem() {
        while(true) {
            CreateItemWithSelector(sectionSelector, sectionNumber);
            yield return new WaitForSeconds(sectionPeriod);
        }
    }

    IEnumerator CreateFlower() {
        while(true) {
            CreateItemWithSelector(specialSelector, 3);
            yield return new WaitForSeconds(specialPeriod);
        }
    }

    void CreateNormalItem() {
        float dice = Random.Range(0f, 1f);
        if (dice > 0.3f) {
            CreateItemWithSelector(normalSelector[0], 0);
        }
        else {
            CreateItemWithSelector(normalSelector[0], 1);
        }
    }

    void CreateSpecialItem() {
        CreateItemWithSelector(specialSelector[specialEventType], specialItem);
    }

    void CreateItemWithSelector(ItemSelector itemSelector, int itemNo) {
        Vector2 genPoint = SelectGenPoint();
        GameObject item = itemSelector.SelectItem(itemNo);
        item.transform.position = genPoint;
    }

    Vector2 SelectGenPoint() {
        Vector2 genPoint;
        float halfZoneXScale = 720f / 2;
        genPoint.x = Random.Range(-halfZoneXScale, halfZoneXScale);
        genPoint.y = 790f;
        return genPoint;
    }
}