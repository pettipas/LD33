using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public Text timer;
	public Text helath;
	public float totalSeconds = 0;
	bool deathStarted;
	public Health hero;
	public Control control;
	public List<Wave> waves = new List<Wave>();
	public List<Transform> positions = new List<Transform>();
	public GameObject tankPrefab;
	public GameObject soliderPrefab;


	public Transform destroy;
	public Transform win;
	public Transform gameOver;

	public void Awake(){
		totalSeconds=0;
		waves.ForEach(x=>{
			totalSeconds+=x.timeTonext;
		});
		control.enabled = false;
	}
	public IEnumerator Start(){
		yield return new WaitForSeconds(3.0f);
		if(deathStarted){
			yield break;
		}
		control.enabled = true;
		destroy.GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds(1.0f);
		if(deathStarted){
			yield break;
		}
		yield return StartCoroutine(Waves());
		if(deathStarted){
			yield break;
		}
		win.gameObject.SetActive(true);
		yield return new WaitForSeconds(3.0f);
	
		Application.LoadLevel("start");
		yield break;
	}
	public void Update(){
		helath.text = hero.health +"";
		UpdateTimer();
		if(LoseCondition &&!deathStarted){
			deathStarted =true;
			StartCoroutine(YouLose());
		}
	}

	public IEnumerator Waves(){
		if(deathStarted){
			yield break;
		}
		if(waves.Count == 0){
			yield break;
		}

		Wave nextWave = null;
		if(waves.Count >0){
			nextWave = waves[0];
			waves.RemoveAt(0);
		}
		StartCoroutine(Spawn(nextWave));
		yield return new WaitForSeconds(nextWave.timeTonext);
		yield return StartCoroutine(Waves());
	}

	public bool LoseCondition{
		get{
			return hero.health<=0;
		}
	}

	public IEnumerator YouWin(){
		Debug.Log("You win");
		yield break;
	}

	public IEnumerator YouLose(){
		gameOver.gameObject.SetActive(true);
		yield return new WaitForSeconds(10.0f);
		while(!(Input.anyKey && !Input.anyKeyDown)){
			Application.LoadLevel("start");
			yield return null;
		}
	}

	public IEnumerator Spawn(Wave wave){
		for(int i = 0; i < wave.tanks; i++){
			yield return new WaitForSeconds(1.0f);
			if(deathStarted){
				yield break;
			}
			Instantiate(tankPrefab, NextPosition(),Quaternion.identity);
		}

		for(int i = 0; i < wave.soliders; i++){
			yield return new WaitForSeconds(1.0f);
			if(deathStarted){
				yield break;
			}	
			Instantiate(soliderPrefab,NextPosition(),Quaternion.identity);
		}
	}

	int nextIndex = 0;
	public Vector3 NextPosition(){

		if(nextIndex >=positions.Count){
			nextIndex = 0;
		}
		Vector3 pos = positions[nextIndex].transform.position;
		nextIndex++;
		return pos;
	}

	public void UpdateTimer(){
		totalSeconds -= Time.deltaTime;
		var minutes = (int)(totalSeconds / 60);
		var seconds = (int)(totalSeconds % 60);
		timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public void OnDrawGizmos(){
		positions.ForEach(x=>{
			Gizmos.DrawCube(x.position,Vector3.one);
		});
	}
}

