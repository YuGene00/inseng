using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

    //inspector
    public float immortalTime = 1f;

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
        }
    }
    bool immortal = false;
    WaitForSeconds waitForImmortal;
    GameObject[] balloonList;
    int balloonNo;
    AudioSource audioSource;

    void ChangeBalloon() {
        int addBalloonNo = lifeNo - balloonNo;
        if (addBalloonNo < 0) {
            RemoveBalloon(addBalloonNo);
        } else {
            AddBalloon(addBalloonNo);
        }
    }

    void RemoveBalloon(int removeNo) {
        for (int i = balloonNo - 1; i >= balloonNo - removeNo; --i) {
            balloonList[i].SetActive(false);
        }
        PlayBoomSound();
    }

    void AddBalloon(int addNo) {
        for(int i = balloonNo; i < balloonNo + addNo; ++i) {
            balloonList[i].SetActive(true);
        }
    }

    void Awake() {
        InitImmortalTime();
        InitBalloon();
        InitAudioSource("EffectSound/Explosion");
    }

    void InitImmortalTime() {
        waitForImmortal = new WaitForSeconds(immortalTime);
    }

    void InitBalloon() {
        balloonList = GameObject.FindGameObjectsWithTag("Balloon");
        balloonNo = balloonList.Length;
    }

    void InitAudioSource(string name) {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(name);
        audioSource.volume = 1f;
    }

    public void AddLife(int value) {
        const int minLife = 0;
        const int maxLife = 6;
        LifeNo = Mathf.Max(minLife, lifeNo + value);
        LifeNo = Mathf.Min(lifeNo, maxLife);
    }

    public void Damaged(int value) {
        if (immortal) {
            return;
        }
        AddLife(value);
        StartCoroutine("WaitWhileImmortal");
    }

    IEnumerator WaitWhileImmortal() {
        immortal = true;
        yield return waitForImmortal;
        immortal = false;
    }

    void PlayBoomSound() {
        audioSource.Play();
    }
}