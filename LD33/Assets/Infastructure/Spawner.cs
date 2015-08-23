using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	bool deathStarted;
	public Health hero;

	public List<Wave> waves = new List<Wave>();
	public List<Transform> positions = new List<Transform>();
	public GameObject tankPrefab;
	public GameObject soliderPrefab;

	public void Start(){
		StartCoroutine(Waves());
	}
	public void Update(){
		if(hero.health <=0 &&!deathStarted){
			deathStarted =true;
			StartCoroutine(YouLose());
		}
	}

	public IEnumerator Waves(){
		if(waves.Count == 0){
			StartCoroutine(YouWin());
			yield break;
		}

		Wave nextWave = null;
		if(waves.Count >0){
			nextWave = waves[0];
			waves.RemoveAt(0);
		}
		StartCoroutine(Spawn(nextWave));
		yield return new WaitForSeconds(nextWave.timeTonext);
		StartCoroutine(Waves());
	}

	public IEnumerator YouWin(){
		Debug.Log("You win");
		yield break;
	}

	public IEnumerator YouLose(){
		Debug.Log("You lose");
		yield break;
	}

	public IEnumerator Spawn(Wave wave){
		for(int i = 0; i < wave.tanks; i++){
			yield return new WaitForSeconds(1.0f);
			Instantiate(tankPrefab, NextPosition(),Quaternion.identity);
		}

		for(int i = 0; i < wave.soliders; i++){
			yield return new WaitForSeconds(1.0f);
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

	public void OnDrawGizmos(){
		positions.ForEach(x=>{
			Gizmos.DrawCube(x.position,Vector3.one);
		});
	}
}

