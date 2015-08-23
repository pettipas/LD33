using UnityEngine;
using System.Collections;

public class Dongle : MonoBehaviour {

	public GameObject emmiter;

	public void Emitter(bool on){
		emmiter.gameObject.SetActive(on);
	}
}
