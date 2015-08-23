using UnityEngine;
using System.Collections;

public class FootStomp : MonoBehaviour {

	public AudioSource audioSource;
	public static float dampenTime = 1f;
	bool stepping;
	public Transform target;
	public Transform foot;
	public Vector3 dampingVelocity;
	public float maxDampenTime;
	public float footPrint=0.5f;

	public Transform Foot{
		get{
			return foot;
		}
	}

	public bool Stepping{
		get{
			return stepping;
		}
	}

	public void Awake(){
		maxDampenTime = dampenTime;
	}

	public float detectedHeight = 0;

	public void Update(){
		Vector3 targetPosition = target.transform.position;
		foot.position = Vector3.SmoothDamp(foot.position, new Vector3(targetPosition.x, targetPosition.y, targetPosition.z), ref dampingVelocity,maxDampenTime);
	}

	public IEnumerator Step(float duration, float stepHeight, float stride, Vector3 dir){
		audioSource.Play();
		Vector3 goal = foot.transform.position + dir * stride;
		maxDampenTime = 1.0f;
		target.position = foot.transform.position;
		target.position += new Vector3(0,stepHeight,0);
		Vector3 start = target.position;
		goal = new Vector3(goal.x,target.position.y,goal.z);

		float dt = 1/duration;
		float t = 0;

		while(t < 1){
			t+=dt*Time.deltaTime * 5.0f;
			target.position = Vector3.Lerp(start,goal,t);
			yield return null;
		}
		yield return new WaitForSeconds(1.5f);
		SetHeight();
		maxDampenTime = 0.1f;
		target.position =new Vector3(target.position.x,detectedHeight+1,target.position.z);
	
		while(Vector3.Distance(foot.position,target.position) > 0.2f){
			yield return null;
		}
		foot.position = target.position;
		stepping = false;
	}

	public void SetHeight(){
		detectedHeight = 0;
		RaycastHit takenHit;
		RaycastHit hit;
		if(Physics.Raycast(target.position+ new Vector3(0,0,0),-target.transform.up,out hit,Mathf.Infinity)){
			if(hit.point.y >= detectedHeight){
				detectedHeight = hit.point.y;
				takenHit = hit;
			}
        }

		if(Physics.Raycast(target.position + new Vector3(footPrint,0,footPrint),-target.transform.up,out hit,Mathf.Infinity)){
			if(hit.point.y >= detectedHeight){
				detectedHeight = hit.point.y;
				takenHit = hit;
            }
        }

		if(Physics.Raycast(target.position + new Vector3(-footPrint,0,footPrint),-target.transform.up,out hit,Mathf.Infinity)){
			if(hit.point.y >= detectedHeight){
				detectedHeight = hit.point.y;
				takenHit = hit;
            }
        }

		if(Physics.Raycast(target.position + new Vector3(footPrint,0,-footPrint),-target.transform.up,out hit,Mathf.Infinity)){
			if(hit.point.y >= detectedHeight){
				detectedHeight = hit.point.y;
				takenHit = hit;
            }
        }

		if(Physics.Raycast(target.position + new Vector3(-footPrint,0,-footPrint),-target.transform.up,out hit,Mathf.Infinity)){
			if(hit.point.y >= detectedHeight){
				detectedHeight = hit.point.y;
				takenHit = hit;
            }
        }

		Destroyable dest = takenHit.transform.GetComponentInParent<Destroyable>();
		if(dest !=null){
			dest.health-=10;
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(target.position,Vector3.one);

		Gizmos.color = Color.green;
		Gizmos.DrawCube(foot.position,Vector3.one);
	}
}
