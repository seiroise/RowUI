using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 真偽値を扱う要素
	/// </summary>
	public class BoolElement : BuilderElement {

		[SerializeField]
		private Toggle _toggle;

		public Toggle toggle {
			get {
				return _toggle;
			}
		}
	}
}