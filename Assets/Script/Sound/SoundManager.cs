using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    //enum
    public enum EffectType {
        BOOM, COIN, MINUS, END
    }

    //singleton
    public static SoundManager instance = null;

	AudioSource BGMSource;
    AudioClip[] BGMList;
    AudioSource[] effectSource = new AudioSource[(int)EffectType.END];

    void Awake() {
        instance = this;
        InitBGM();
        InitEffect();
    }

    void InitBGM() {
        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMList = Resources.LoadAll<AudioClip>("BGM");
    }

    void InitEffect() {
        for (int i = 0; i < (int)EffectType.END; ++i) {
            effectSource[i] = gameObject.AddComponent<AudioSource>();
            switch((EffectType)i) {
                case EffectType.BOOM:
                    effectSource[i].clip = Resources.Load<AudioClip>("EffectSound/Explosion");
                    break;
                case EffectType.COIN:
                    effectSource[i].clip = Resources.Load<AudioClip>("EffectSound/Coin");
                    break;
                case EffectType.MINUS:
                    effectSource[i].clip = Resources.Load<AudioClip>("EffectSound/Minus");
                    break;
            }
        }
    }

    void Start() {
        BindFuncToEvent();
    }

    void BindFuncToEvent() {
        EventManager.instacne.AddFuncToEventForStart(NormalBGM, EventManager.EventType.NORMAL);
        EventManager.instacne.AddFuncToEventForStart(SpecialBGM, EventManager.EventType.SPECIAL);
        EventManager.instacne.AddFuncToEventForStart(SpecialBGM, EventManager.EventType.BRANCH);
        EventManager.instacne.AddFucToEventForDie(DieBGM);
    }

	void NormalBGM() {
        if (BGMSource.clip != BGMList[0]) {
            BGMSource.clip = BGMList [0];
            BGMSource.Play();
        }
	}

	void SpecialBGM() {
        if (BGMSource.clip != BGMList[1]) {
            BGMSource.clip = BGMList[1];
            BGMSource.Play();
        }
	}

	void DieBGM() {
		BGMSource.clip = BGMList [2];
		BGMSource.Play ();
	}

    public void PlayEffectSound(EffectType effectType) {
        effectSource[(int)effectType].Play();
    }
}