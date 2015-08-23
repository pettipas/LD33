using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {

	public List<Renderer> boundaries = new List<Renderer>();
	public List<Vector3> previews = new List<Vector3>(4);
	public static Control PlayerInstance;

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

	public void Awake(){
		if(PlayerInstance ==null){
			PlayerInstance = this;
		}
	}

	bool running;

	public void Update(){
		if(Input.GetKey(KeyCode.W) && !running){
			if(OkToGo(Vector3.forward)){
				running = true;
				StartCoroutine(Stride(Vector3.forward));
			}
		}else if(Input.GetKey(KeyCode.S) && !running){
            if(OkToGo(-Vector3.forward)){
				running = true;
				StartCoroutine(Stride(-Vector3.forward));
			}
		}else if(Input.GetKey(KeyCode.A) && !running){
			if(OkToGo(-Vector3.right)){
				running = true;
				StartCoroutine(Stride(-Vector3.right));
			}
		}else if(Input.GetKey(KeyCode.D) && !running){
			if(OkToGo(Vector3.right)){
				running = true;
				StartCoroutine(Stride(Vector3.right));
			}
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

	public bool OkToGo(Vector3 dir){

		previews.Clear();

		previews.Add(frontLeft.PreviewPosition(dir, strideWidth));
		previews.Add(frontRight.PreviewPosition(dir, strideWidth));
		previews.Add(rearLeft.PreviewPosition(dir, strideWidth));
		previews.Add(rearRight.PreviewPosition(dir, strideWidth));

		for(int i = 0; i < boundaries.Count; i++){
			Renderer r = boundaries[i];
			for(int j = 0; j < previews.Count; j++){
				if(r.bounds.Contains(previews[j])){
					return false;
				}
			}
		}
		return true;
	}
}
