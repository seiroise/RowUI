using UnityEngine;
using System.Collections;

namespace RowUI {

	/// <summary>
	/// UIビルダーの設定
	/// </summary>
	[CreateAssetMenu(fileName = "BuilderSetting", menuName = "UIBuilder/BuilderSettings")]
	public class BuilderSettings : ScriptableObject {

		[Header("UI Element")]

		/// <summary>
		/// 整数値を扱うための要素
		/// </summary>
		[SerializeField]
		private IntElement _intElement;

		public IntElement intElement {
			get {
				return _intElement;
			}
		}

		/// <summary>
		/// 実数値を扱うための要素
		/// </summary>
		[SerializeField]
		private FloatElement _floatElement;

		public FloatElement floatElement {
			get {
				return _floatElement;
			}
		}

		/// <summary>
		/// 実数を表示するための要素
		/// </summary>
		[SerializeField]
		private FloatSliderElement _floatSliderElement;

		public FloatSliderElement floatSliderElement {
			get {
				return _floatSliderElement;
			}
		}

		/// <summary>
		/// 文字列を編集するための要素
		/// </summary>
		[SerializeField]
		private StringElement _stringElement;

		public StringElement stringElement {
			get {
				return _stringElement;
			}
		}

		/// <summary>
		/// 真偽値を表示するための要素
		/// </summary>
		[SerializeField]
		private BoolElement _boolElement;

		public BoolElement boolElement {
			get {
				return _boolElement;
			}
		}

		/// <summary>
		/// ラベルを表示するための要素
		/// </summary>
		[SerializeField]
		private LabelElement _labelElement;

		public LabelElement labelElement {
			get {
				return _labelElement;
			}
		}

		/// <summary>
		/// ボタンを表示するための要素
		/// </summary>
		[SerializeField]
		private ButtonElement _buttonElement;

		public ButtonElement buttonElement {
			get {
				return _buttonElement;
			}
		}

		/// <summary>
		/// ドロップダウンを表示するの要素
		/// </summary>
		[SerializeField]
		private DropdownElement _dropdownElement;

		public DropdownElement dropdownElement {
			get {
				return _dropdownElement;
			}
		}

		/// <summary>
		/// グループ作成用の要素
		/// </summary>
		[SerializeField]
		private GroupElement _groupElement;

		public GroupElement groupElement {
			get {
				return _groupElement;
			}
		}

		/// <summary>
		/// ドロップダウン付きのグループ作成用の要素
		/// </summary>
		[SerializeField]
		private DropdownGroupElement _dropdownGroupElement;

		public DropdownGroupElement dropdownGroupElement {
			get {
				return _dropdownGroupElement;
			}
		}

		[SerializeField]
		private InstanceElement _instanceElement;

		public InstanceElement instanceElement {
			get {
				return _instanceElement;
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