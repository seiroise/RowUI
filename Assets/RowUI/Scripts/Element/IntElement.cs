using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 整数を扱う要素
	/// </summary>
	public class IntElement : BuilderElement {

		[SerializeField]
		private InputField _inputField;

		public InputField inputField {
			get {
				return _inputField;
			}
		}

		private IntEvent _onValueChanged;

		public IntEvent onValueChanged {
			get {
				return _onValueChanged;
			}
		}

		private void Awake() {
			_inputField.contentType = InputField.ContentType.IntegerNumber;
			_inputField.onEndEdit.AddListener(OnEndEdit);
			_onValueChanged = new IntEvent();
		}

		private void OnEndEdit(string str) {
			int result;
			if (int.TryParse(str, out result)) {
				_onValueChanged.Invoke(result);
			}
		}
	}
}