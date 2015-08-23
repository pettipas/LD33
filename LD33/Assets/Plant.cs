using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	public ParticleSystem pSystem;
	public AudioSource growSound;
	public Animator animation;
	bool triggered = false;
	void Update () {
		if(animation != null && !triggered){
			triggered = true;
			animation.Play("grow",0,0);
			StartCoroutine(GrowSound());
			StartCoroutine(EmitSometimes());
		}
	}

	public IEnumerator GrowSound(){
		growSound.Play();
		yield return new WaitForSeconds(5.0f);
		growSound.Stop();
	}

	public IEnumerator EmitSometimes(){
		float duration =  Random.Range(5,13);
		yield return new WaitForSeconds(duration);
		pSystem.Emit(5);
		growSound.loop = false;
		growSound.pitch = growSound.pitch + Random.Range(-0.3f,0.3f);
		growSound.Play();
		StartCoroutine(EmitSometimes());
	}
}
