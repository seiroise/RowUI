using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngineInternal;

namespace RowUI {

	public class SerializeDemo : MonoBehaviour {

		[Serializable]
		public class Demo {
			
			public int age = 1;

			public string name = "Name";

			[SerializeField]
			private int _money = 100000;

			[SerializeField]
			private bool _isMale = true;

			[SerializeField]
			private float _weight = 50f;
		}

		[Serializable]
		public struct DemoS {

			public int age;

			public string name;

			[SerializeField]
			private int _money;

			[SerializeField]
			private bool _isMale;

			[SerializeField]
			private float _weight;
		}

		public Builder _builder;

		// Use this for initialization
		void Start() {
			var ins = new DemoS();
			_builder.MakeInstance("Demo", ins);
		}
	}
}