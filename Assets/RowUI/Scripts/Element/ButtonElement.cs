using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// ボタン要素
	/// </summary>
	public class ButtonElement : BuilderElement {

		[SerializeField]
		private Button _button;

		public Button button {
			get {
				return _button;
			}
		}
	}
}