using UnityEngine;
using System.Collections;

namespace RowUI {

	/// <summary>
	/// 実数値を扱う要素
	/// </summary>
	public class UIFloatValueElement : UIBuilderElement {

		[SerializeField]
		private UISliderField _valueField;

		public UISliderField valueField {
			get {
				return _valueField;
			}
		}


	}
}