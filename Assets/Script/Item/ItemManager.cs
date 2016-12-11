using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //inspector
    public Transform createZoneTrans;
    public GameObject[] childItem;
    public float enemyPeriod = 0.65f;
    public float normalPeriod = 0.5f;
    public float specialPeriod = 0.8f;
    public float sectionPeriod = 0.8f;

    //variable
    ItemSelector enemySelector;
    ItemSelector normalSelector;
    ItemSelector specialSelector;
    SectionSelector sectionSelector;
    SpineSelector spineSelector;
    StarSelector starSelector;
    ChildSelector childSelector;
    StudentSelector studentSelector;
    UniversitySelector universitySelector;
    UnemployedSelector unemploySelector;
    WorkerSelector workerSelector;
    ChickenSelector chickenSelector;
    SeniorSelector seniorSelector;
    int specialItem;
    int oldCount = 0;
    int sectionNumber = 0;

    void Start() {
        SetSelector();
        GameController.NormalEvent += NormalStart;
        GameController.SpecialEvent += SpecialStart;
        GameController.SectionEvent += SectionStart;
        EnemyStart();
    }

    void SetSelector() {
        sectionSelector = new SectionSelector();
        spineSelector = new SpineSelector();
        starSelector = new StarSelector();
        childSelector = new ChildSelector();
        studentSelector = new StudentSelector();
        universitySelector = new UniversitySelector();
        unemploySelector = new UnemployedSelector();
        workerSelector = new WorkerSelector();
        chickenSelector = new ChickenSelector();
        seniorSelector = new SeniorSelector();
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

    public void StopManager() {
        StopAllCoroutines();
    }
}