using UnityEngine;
using System.Collections;

namespace RowUI {

	/// <summary>
	/// 実数値をスライダー扱う要素
	/// </summary>
	public class FloatSliderElement : BuilderElement {

		[SerializeField]
		private UISliderField _valueField;

		public UISliderField valueField {
			get {
				return _valueField;
			}
		}
	}
}