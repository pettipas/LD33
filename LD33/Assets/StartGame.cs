using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public Animator response;
	public AudioSource sound;
	bool play;
	public void Update(){
		if(Input.anyKey && !play && !Input.anyKeyDown){
			play = true;
			StartCoroutine(Play());
		}
	}

	public IEnumerator Play(){
		sound.Play();
		response.Play("damage");
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel("MonsterLab");
	}
}
