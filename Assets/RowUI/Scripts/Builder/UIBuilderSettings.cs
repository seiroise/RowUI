using UnityEngine;
using System.Collections;

namespace RowUI {

	/// <summary>
	/// UIビルダーの設定
	/// </summary>
	[CreateAssetMenu(fileName = "BuilderSetting", menuName = "UIBuilder/BuilderSettings")]
	public class UIBuilderSettings : ScriptableObject {

		[Header("UI Element")]

		/// <summary>
		/// 実数を表示するための要素
		/// </summary>
		[SerializeField]
		private UIFloatValueElement _floatValueElement;

		public UIFloatValueElement floatValueElement {
			get {
				return _floatValueElement;
			}
		}

		/// <summary>
		/// 文字列を編集するための要素
		/// </summary>
		[SerializeField]
		private UIStringValueElement _stringValueElement;

		public UIStringValueElement stringValueElement {
			get {
				return _stringValueElement;
			}
		}

		/// <summary>
		/// ラベルを表示するの要素
		/// </summary>
		[SerializeField]
		private UILabelElement _labelElement;

		public UILabelElement labelElement {
			get {
				return _labelElement;
			}
		}

		/// <summary>
		/// ドロップダウンを表示するの要素
		/// </summary>
		[SerializeField]
		private UIDropdownElement _dropdownElement;

		public UIDropdownElement dropdownElement {
			get {
				return _dropdownElement;
			}
		}

		/// <summary>
		/// グループ作成用の要素
		/// </summary>
		[SerializeField]
		private UIGroupElement _groupElement;

		public UIGroupElement groupElement {
			get {
				return _groupElement;
			}
		}

		[Header("UI Option")]

		/// <summary>
		/// 要素同士の間隔
		/// </summary>
		[SerializeField, Range(0f, 100f)]
		private float _interval = 4f;

		public float interval {
			get {
				return _interval;
			}
		}
	}
}