using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    //inspector
    public float immortalTime = 1f;

    //caching
    GameObject[] balloonList;
    Animator[] balloonAnimator;
    int balloonNo;

    //variable
    int lifeNo = 6;
    public int GetLifeNo {
        get {
            return lifeNo;
        }
    }
    int LifeNo {
        set {
            lifeNo = value;
            ChangeBalloon();
            if (lifeNo <= 0) {
                EventManager.instacne.EventForDie();
            }
        }
    }
    bool immortal = false;
    WaitForSeconds waitForImmortal;

    void ChangeBalloon() {
        int addBalloonNo = lifeNo - balloonNo;
        if (addBalloonNo < 0) {
            RemoveBalloon(-addBalloonNo);
        } else {
            AddBalloon(addBalloonNo);
        }
    }

    void RemoveBalloon(int removeNo) {
        for (int i = balloonNo - 1; i >= balloonNo - removeNo; --i) {
            StartCoroutine(BoomingBalloon(i));
        }
        balloonNo -= removeNo;
        PlayBoomSound();
    }

    void PlayBoomSound() {
        SoundManager.instance.PlayEffectSound(SoundManager.EffectType.BOOM);
    }

    IEnumerator BoomingBalloon(int index) {
        Player.instance.SetSpriteWithState(SpriteSelector.SpriteType.SAD);
        balloonAnimator[index].SetTrigger("Boom");
        yield return null;
        yield return new WaitUntil(() => (balloonAnimator[index].GetCurrentAnimatorStateInfo(0).normalizedTime >= balloonAnimator[index].GetCurrentAnimatorStateInfo(0).length));
        balloonList[index].SetActive(false);
        Player.instance.SetSpriteWithState(SpriteSelector.SpriteType.NORMAL);
    }

    void AddBalloon(int addNo) {
        for(int i = balloonNo; i < balloonNo + addNo; ++i) {
            balloonList[i].SetActive(true);
        }
        balloonNo += addNo;
    }

    void Awake() {
        InitWait();
        Caching();
    }

    void InitWait() {
        waitForImmortal = new WaitForSeconds(immortalTime);
    }

    void Caching() {
        Transform balloonRoot = GameObject.FindGameObjectWithTag("Balloon").transform;
        balloonNo = balloonRoot.childCount;
        balloonList = new GameObject[balloonNo];
        balloonAnimator = new Animator[balloonNo];
        for (int i = 0; i < balloonNo; ++i) {
            balloonList[i] = balloonRoot.GetChild(i).Find("Object").gameObject;
            balloonAnimator[i] = balloonList[i].GetComponent<Animator>();
        }
    }

    public void AddLife(int value) {
        const int minLife = 0;
        const int maxLife = 6;
        LifeNo = Mathf.Min(Mathf.Max(minLife, lifeNo + value), maxLife);
    }

    public void Damaged(int value) {
        if (immortal) {
            return;
        }
        AddLife(-value);
        StartCoroutine("WaitWhileImmortal");
    }

    IEnumerator WaitWhileImmortal() {
        immortal = true;
        yield return waitForImmortal;
        immortal = false;
    }
}