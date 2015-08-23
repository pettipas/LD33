using UnityEngine;
using System.Collections;

public class Dood : MonoBehaviour {

	public IEnumerator Start(){
		float val= Random.Range(0.3f,0.5f);
		yield return new WaitForSeconds(val);
		Animator a = GetComponent<Animator>();
		a.enabled = true;
	}
}
