using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutListener : MonoBehaviour
{

    private Action<bool> testBoolListener;
	private Action<int> testIntListener;

	private void Awake() {
		testBoolListener = new Action<bool>(TestBool);
		testIntListener = new Action<int>(TestInt);
	}

	private void OnEnable() {
		EventManager.StartListening("bool_trigger", testBoolListener);
		EventManager.StartListening("int_trigger", testIntListener);
	}

	private void OnDisable() {
		EventManager.StopListening("bool_trigger", testBoolListener);
		EventManager.StopListening("int_trigger", testIntListener);
	}

	private void TestBool(bool objParams) {
		Debug.Log("Bool value: " + objParams);
	}

	private void TestInt(int objParams) {
		Debug.Log("Int value: " + objParams);
	}
}
