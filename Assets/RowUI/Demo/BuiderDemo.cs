﻿using System.Collections;
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
			builder.MakeFloatSlider("value", 1, 0, 10, OnFloatValueChange);
			builder.MakeDropdown("Type", 0, new string[] { "A", "B", "C", "D" }, OnDropdownChanged);

			var group = builder.MakeGroup("Group A");
			group.builder.MakeFloatSlider("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group = builder.MakeGroup("Group B");
			group.builder.MakeFloatSlider("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group = group.builder.MakeGroup("Group BA");
			group.builder.MakeFloatSlider("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			_gD = builder.MakeDropdownGroup("Group D", 0, _types, OnTypeChanged);
			OnTypeChanged(0);

			group = builder.MakeGroup("Group C");
			group.builder.MakeFloatSlider("Sub Parameter A", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter B", 1, 0, 10, OnFloatValueChange);
			group.builder.MakeFloatSlider("Sub Parameter C", 1, 0, 10, OnFloatValueChange);

			group.builder.MakeString("Sub Text", "", OnEndEdit);
		}

		private void OnFloatValueChange(float v) {
			Debug.Log("OnFloatValueChanged : " + v);
		}

		private void OnBoolValueChnaged(bool v) {
			Debug.Log("OnBoolValueChanged : " + v);
		}

		private void OnButtonClicked() {
			Debug.Log("OButtonClicked");
		}

		private void OnDropdownChanged(int i) {
			Debug.Log("OnDropdownChanged : " + i);
		}

		private void OnEndEdit(string str) {
			Debug.Log("OnEndEdit : " + str);
		}

		private void OnTypeChanged(int i) {
			_gD.builder.RemoveElements();
			if (_gD) {
				switch (i) {
				case 0:
					_gD.builder.MakeFloatSlider("Melee damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Fire", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Ice", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeBool("Super", false, OnBoolValueChnaged);
					_gD.builder.MakeButton("Attack", OnButtonClicked);
					break;
				case 1:
					_gD.builder.MakeFloatSlider("Melee damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Impact", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Range", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Fire", 10, 1, 100, OnFloatValueChange);
					break;
				case 2:
					_gD.builder.MakeFloatSlider("Indirect damage", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Poison", 10, 1, 100, OnFloatValueChange);
					_gD.builder.MakeFloatSlider("Range", 10, 1, 100, OnFloatValueChange);
					break;
				}
			}
		}
	}
}