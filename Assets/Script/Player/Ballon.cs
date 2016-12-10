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
					StartCoroutine (ActiveOff (i));
				}
			}
			number = player.Life;
		}
	}

	IEnumerator ActiveOff(int index)
	{
		yield return new WaitForSeconds (0.5f);

		ballonGroup.GetChild (index).gameObject.SetActive (false);
	}
}