using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateZone : MonoBehaviour {

    //struct
    struct CreatedObj {
        public Transform trans;
        public BoxCollider2D collider;
    }

    //singleton
    public static CreateZone instance = null;
    
    //inspector
    public Vector2 genOffset = new Vector2(100f, 100f);

	//variable
    AreaController areaController = new AreaController();
    List<CreatedObj> createdObjList = new List<CreatedObj>(5);
    Vector2 pos, halfSize;

    void Awake() {
        Initialize();
    }

    void Initialize() {
        instance = this;
        InitAreaController();
    }

    void InitAreaController() {
        Transform trans = transform;
        pos = trans.position;
        halfSize = trans.localScale * 0.5f;
        areaController.SetArea(pos - halfSize, pos + halfSize);
    }

    void Start() {
        StartCoroutine("CheckArea");
    }

    IEnumerator CheckArea() {
        while (true) {
            for (int i = createdObjList.Count - 1; i >= 0; --i) {
                if (!areaController.IsInArea(createdObjList[i].trans.position)) {
                    createdObjList.RemoveAt(i);
                }
            }
            yield return null;
        }
    }

    public Vector2 SelectGenPoint() {
        Vector2 genPoint;
        do {
            genPoint = CreateGenPoint();
        } while (IsOverlapWithOthers(genPoint));
        return genPoint;
    }

    Vector2 CreateGenPoint() {
        Vector2 genPoint;
        genPoint.x = Random.Range(-halfSize.x, halfSize.x);
        genPoint.y = pos.y;
        return genPoint;
    }

    bool IsOverlapWithOthers(Vector2 genPoint) {
        for (int i = 0; i < createdObjList.Count; ++i) {
            if (IsPointOverlapWithCollider(genPoint, createdObjList[i].collider)) {
                return true;
            }
        }
        return false;
    }

    bool IsPointOverlapWithCollider(Vector2 genPoint, BoxCollider2D collider) {
        Vector2 genLeftBot = genPoint - genOffset;
        Vector2 genRightTop = genPoint + genOffset;
        Vector2 colliderPos = collider.transform.position;
        Vector2 colHalfSize = collider.size * 0.5f;
        Vector2 colLeftBot = colliderPos - colHalfSize;
        Vector2 colRightTop = colliderPos + colHalfSize;

        return ((genLeftBot.x <= colRightTop.x && genRightTop.x >= colLeftBot.x) && (genLeftBot.y <= colRightTop.y && genRightTop.y >= colLeftBot.y));
    }

    public void AddObjToCreatedObjList(GameObject gameObj) {
        CreatedObj createdObj;
        createdObj.trans = gameObj.transform;
        createdObj.collider = gameObj.GetComponent<BoxCollider2D>();
        createdObjList.Add(createdObj);
    }
}
