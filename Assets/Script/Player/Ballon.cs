using UnityEngine;
using System.Collections;

public class Ballon : MonoBehaviour 
{
	public Player player = null;
	public Transform ballonGroup = null;

	private int number = 0;

	void Start () 
	{
		number = player.Life;

		for (int i = 0; i < ballonGroup.childCount; i++)
			ballonGroup.GetChild (i).gameObject.SetActive (false);

		for (int i = 0; i <number; i++)
			ballonGroup.GetChild (i).gameObject.SetActive (true);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("EffectSound/Explosion") as AudioClip;
        audioSource.volume = 1f;
    }

	void Update ()
	{
		if (!number.Equals (player.Life)) 
		{
			for (int i = 0; i <player.Life; i++)
				ballonGroup.GetChild (i).gameObject.SetActive (true);

			for (int i = player.Life; i < ballonGroup.childCount; i++)
			{
				if (ballonGroup.GetChild (i).gameObject.activeSelf) 
				{
					ballonGroup.GetChild (i).GetComponentInChildren<Animator> ().SetTrigger ("Boom");
                    PlaySound();
                    StartCoroutine (ActiveOff (i));
				}
			}
			number = player.Life;
		}
	}

    AudioSource audioSource;
    public AudioClip boomSound;

    IEnumerator ActiveOff(int index)
	{
		yield return new WaitForSeconds (0.5f);

		ballonGroup.GetChild (index).gameObject.SetActive (false);
    }

    public void PlaySound() {
        audioSource.Play();
    }
}