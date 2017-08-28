using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RowUI.Demo {

	public class BuiderDemo : MonoBehaviour {

		public UIBuilder builder;

		// Use this for initialization
		void Start() {
			builder.MakeLabelElement("demo");
			builder.MakeFloatValueElement("value", 1, 0, 10, OnValueChange01);
			builder.MakeDropdownElement("Options", 0, new string[]{ "A", "B", "C", "D"}, OnValueChange02);

			var group = builder.MakeElementGroup("Test Group A");
			group.builder.MakeFloatValueElement("Sub Parameter A", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter B", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter C", 1, 0, 10, OnValueChange01);

			group = builder.MakeElementGroup("Test Group B");
			group.builder.MakeFloatValueElement("Sub Parameter A", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter B", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter C", 1, 0, 10, OnValueChange01);

			group = group.builder.MakeElementGroup("Test Group BA");
			group.builder.MakeFloatValueElement("Sub Parameter A", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter B", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter C", 1, 0, 10, OnValueChange01);

			group = builder.MakeElementGroup("Test Group C");
			group.builder.MakeFloatValueElement("Sub Parameter A", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter B", 1, 0, 10, OnValueChange01);
			group.builder.MakeFloatValueElement("Sub Parameter C", 1, 0, 10, OnValueChange01);

			group.builder.MakeStringValueElement("Sub Text", "", OnEndEdit);

		}

		private void Update() {
		}

		private void OnValueChange01(float v) {
			Debug.Log("OnValueChange01 : " + v);
		}

		private void OnValueChange02(int i) {
			
		}

		private void OnEndEdit(string str) {
			Debug.Log("OnEndEdit : " + str);
		}
	}
}