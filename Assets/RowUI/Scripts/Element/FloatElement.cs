using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 整数を扱う要素
	/// </summary>
	public class FloatElement : BuilderElement {

		[SerializeField]
		private InputField _inputField;

		public InputField inputField {
			get {
				return _inputField;
			}
		}

		private FloatEvent _onValueChanged;

		public FloatEvent onValueChanged {
			get {
				return _onValueChanged;
			}
		}

		private void Awake() {
			_inputField.contentType = InputField.ContentType.DecimalNumber;
			_inputField.onEndEdit.AddListener(OnEndEdit);
			_onValueChanged = new FloatEvent();
		}

		private void OnEndEdit(string str) {
			float result;
			if (float.TryParse(str, out result)) {
				_onValueChanged.Invoke(result);
			}
		}
	}
}