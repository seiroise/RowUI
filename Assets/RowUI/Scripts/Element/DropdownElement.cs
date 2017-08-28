using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 幾つかの項目をドロップダウンで表示する要素
	/// </summary>
	public class DropdownElement : BuilderElement {

		[SerializeField]
		private Dropdown _dropdown;

		public Dropdown dropdown {
			get {
				return _dropdown;
			}
		}
	}
}