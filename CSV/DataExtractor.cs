using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataExtractor {

	ExperimentManager manager;

	public DataExtractor() {
		GameObject exp = GameObject.Find("Experiment");
		if (exp == null) {
			Debug.Log("/!\\ Error:: can't find experiment gameobject");
		}
		manager = exp.GetComponent<ExperimentManager>();
		if (manager == null) {
			Debug.Log("/!\\ Error:: can't find ExperimentManager gameobject");
		}
	}

	public string[] GetTracebackData() {
		var tmp = manager.GetCurrentTrial();
		return new string[] {tmp[0], tmp[1], tmp[2], tmp[3], Time.time.ToString("N3")};
	}

	public string[] GetExperimentData() {
		var tmp = manager.GetCurrentTrial();
		return new string[] {tmp[0], tmp[1], tmp[2], tmp[3], Time.time.ToString("N3")};
	}
}
