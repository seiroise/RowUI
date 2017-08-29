using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RowUI {

	[System.Serializable]
	public class IntEvent : UnityEvent<int> { }

	[System.Serializable]
	public class FloatEvent : UnityEvent<float> { }
}