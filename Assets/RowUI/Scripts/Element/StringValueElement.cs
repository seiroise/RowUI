using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 文字列を扱う要素
	/// </summary>
	public class StringValueElement : BuilderElement {

		[SerializeField]
		private InputField _inputField;

		public InputField inputField {
			get {
				return _inputField;
			}
		}
	}
}