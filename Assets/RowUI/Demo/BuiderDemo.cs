using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RowUI.Demo {

	public class BuiderDemo : MonoBehaviour {

		public Builder builder;

		private DropdownGroupElement _gD;
		private readonly string[] _types = { "Sword", "Spear", "Bow" };

		// Use this for initialization
		void Start() {
			builder.MakeLabel("demo");
			builder.MakeFloatValue("value", 1, 0, 10, OnFloatValueChange);
			builder.MakeDropdown("Type", 0, new string[] { "A", "B", "C", "D" }, OnValueChange02);

			var group = builder.MakeGroup("Group A");
			group.builder.MakeFloatValue("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group = builder.MakeGroup("Group B");
			group.builder.MakeFloatValue("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group = group.builder.MakeGroup("Group BA");
			group.builder.MakeFloatValue("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			_gD = builder.MakeDropdownGroup("Group D", 0, _types, OnTypeChanged);
			OnTypeChanged(0);

			group = builder.MakeGroup("Group C");
			group.builder.MakeFloatValue("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatValue("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group.builder.MakeStringValue("Sub Text", "", OnEndEdit);
		}

		private void OnFloatValueChange(float v) {
			Debug.Log("OnFloatValueChange : " + v);
		}

		private void OnBoolValueChnaged(bool v) {
			Debug.Log("OnBoolValueChange : " + v);
		}

		private void OnButtonClicked() {
			
		}

		private void OnValueChange02(int i) {

		}

		private void OnEndEdit(string str) {
			Debug.Log("OnEndEdit : " + str);
		}

		private void OnTypeChanged(int i) {
			_gD.builder.RemoveElements();
			if (_gD) {
				switch (i) {
				case 0:
					_gD.builder.MakeFloatValue("Melee damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Fire", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Ice", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeBoolValue("Guard", false, OnBoolValueChnaged);
					_gD.builder.MakeButton("Attack", OnButtonClicked);
					break;
				case 1:
					_gD.builder.MakeFloatValue("Melee damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Impact", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Range", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Fire", 10, 1, 100, OnFloatValueChange);
					break;
				case 2:
					_gD.builder.MakeFloatValue("Indirect damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Poison", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatValue("Range", 10, 1, 100, OnFloatValueChange);
					break;
				}
			}
		}
	}
}