using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //inspector
    public Transform createZoneTrans;
    public GameObject[] childItem;
    public float normalPeriod = 0.5f;
    public float specialPeriod = 2f;

    //variable
    ItemSelector normalSelector;
    ItemSelector specialSelector;
    StarSelector starSelector;
    ChildSelector childSelector;
    StudentSelector studentSelector;
    UniversitySelector universitySelector;
    UnemployedSelector unemploySelector;
    WorkerSelector workerSelector;
    ChickenSelector chickenSelector;
    SeniorSelector seniorSelector;
    int specialItem;
        
    void Start() {
        SetSelector();
        GameController.NormalEvent += NormalStart;
        GameController.SpecialEvent += SpecialStart;
        GameController.SectionEvent += SectionStart;
    }

    void SetSelector() {
        starSelector = new StarSelector();
        childSelector = new ChildSelector();
        studentSelector = new StudentSelector();
        universitySelector = new UniversitySelector();
        unemploySelector = new UnemployedSelector();
        workerSelector = new WorkerSelector();
        chickenSelector = new ChickenSelector();
        seniorSelector = new SeniorSelector();
    }

    void NormalStart() {
        normalSelector = starSelector;
        StopCoroutine("CreateSpecialItem");
        StartCoroutine("CreateNormalItem");
    }

    void SpecialStart() {
        specialItem = Random.Range(0, 4);
        ChangeSpecialSelector();
        StopCoroutine("CreateNormalItem");
        StartCoroutine("CreateSpecialItem");
    }

    void SectionStart() {

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

    void CreateItemWithSelector(ItemSelector itemSelector, int itemNo) {
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