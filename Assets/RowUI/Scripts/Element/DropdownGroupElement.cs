using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// GroupElementにDropdownが合体したもの
	/// </summary>
	public class DropdownGroupElement : GroupElement {

		[SerializeField]
		private Dropdown _dropdown;

		public Dropdown dropdown {
			get {
				return _dropdown;
			}
		}
	}
}