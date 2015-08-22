using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public FootStomp frontLeft;
	public FootStomp frontRight;
	public FootStomp rearLeft;
	public FootStomp rearRight;

	[SerializeField]
	protected float stepHeight = 2.0f;
	[SerializeField]
	protected float stepDuration = 2.0f;
	[SerializeField]
	protected float strideWidth = 2.0f;

	bool running;

	public void Update(){
		if(Input.GetKey(KeyCode.W) && !running){
			running = true;
			StartCoroutine(Stride(Vector3.forward));
		}else if(Input.GetKey(KeyCode.S) && !running){
			running = true;
			StartCoroutine(Stride(-Vector3.forward));
		}else if(Input.GetKey(KeyCode.A) && !running){
			running = true;
			StartCoroutine(Stride(-Vector3.right));
		}else if(Input.GetKey(KeyCode.D) && !running){
			running = true;
			StartCoroutine(Stride(Vector3.right));
		}
	}
	bool flip;
	public IEnumerator Stride(Vector3 dir){

		IEnumerator fl = frontLeft.Step(stepDuration,stepHeight,strideWidth,dir);
		IEnumerator fr = frontRight.Step(stepDuration,stepHeight,strideWidth,dir);

		IEnumerator rl = rearLeft.Step(stepDuration,stepHeight,strideWidth,dir);
		IEnumerator rr = rearRight.Step(stepDuration,stepHeight,strideWidth,dir);

	
		while(fl.MoveNext() && rr.MoveNext()){
			yield return null;
		}
		while(fr.MoveNext() && rl.MoveNext()){
			yield return null;
		}
	

		running = false;
	}
}
