using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace RowUI {

	/// <summary>
	/// UIのビルダー
	/// </summary>
	public class Builder : BuilderElement {

		/// <summary>
		/// ビルダーのセッテイング
		/// </summary>
		[SerializeField]
		private BuilderSettings _settings;

		public BuilderSettings settings {
			get {
				return _settings;
			}
			set {
				_settings = value;
			}
		}

		/// <summary>
		/// 管理している要素
		/// </summary>
		private List<BuilderElement> _elements;

		/// <summary>
		/// 親要素
		/// </summary>
		private Builder _parent;

		/// <summary>
		/// 閉じた状態か
		/// </summary>
		private bool _isClosed = false;

		public bool isClosed {
			get {
				return _isClosed;
			}
		}

		protected override void Awake() {
			base.Awake();
			_elements = new List<BuilderElement>();
		}

		/// <summary>
		/// 管理している要素を内包する大きさを取得する
		/// </summary>
		public Vector2 GetSize() {
			Vector2 size = rectTransform.sizeDelta;
			float height = 0f;
			int actives = 0;
			// ついでにactiveな数を数える
			for (int i = 0; i < _elements.Count; ++i) {
				if (_elements[i].gameObject.activeInHierarchy) {
					height += _elements[i].GetHeight();
					actives++;
				}
			}
			if (actives > 1) {
				height += _settings.interval * (actives - 1);
			}
			size.y = height;
			return size;
		}

		/// <summary>
		/// すべての要素を非activeにして閉じた状態にする
		/// </summary>
		public void Close() {
			_isClosed = true;
			for (int i = 0; i < _elements.Count; ++i) {
				_elements[i].gameObject.SetActive(false);
			}
			// 高さを0にする
			Vector2 size = Vector2.zero;
			size.x = rectTransform.sizeDelta.x;
			rectTransform.sizeDelta = size;

			UpdateSize();
			UpdateElementsPosition();
		}

		/// <summary>
		/// すべての要素をactiveにして開いた状態にする
		/// </summary>
		public void Open() {
			_isClosed = false;
			for (int i = 0; i < _elements.Count; ++i) {
				_elements[i].gameObject.SetActive(true);
			}
			rectTransform.sizeDelta = GetSize();

			UpdateSize();
			UpdateElementsPosition();
		}

		/// <summary>
		/// 大きさの更新
		/// </summary>
		public void UpdateSize() {
			rectTransform.sizeDelta = GetSize();
			if (_parent) {
				_parent.UpdateSize();
			}
		}

		/// <summary>
		/// 管理している要素の座標の更新
		/// </summary>
		public void UpdateElementsPosition() {
			float y = 0f;
			Vector2 pos;
			for (int i = 0; i < _elements.Count; ++i) {
				pos = _elements[i].rectTransform.anchoredPosition;
				pos.y = y;
				_elements[i].rectTransform.anchoredPosition = pos;

				y -= _elements[i].GetHeight() + _settings.interval;
			}
			if (_parent) {
				_parent.UpdateElementsPosition();
			}
		}

		#region Element

		/// <summary>
		/// 要素の追加
		/// </summary>
		/// <param name="elem">Element.</param>
		private void AddElement(BuilderElement elem) {
			_elements.Add(elem);
			UpdateSize();
			UpdateElementsPosition();
		}

		/// <summary>
		/// 指定した要素を生成する
		/// </summary>
		/// <returns>The element.</returns>
		/// <param name="prefab">Prefab.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private T MakeElement<T>(T prefab) where T : BuilderElement {
			var elem = Instantiate<T>(prefab);
			var rect = elem.rectTransform;
			rect.SetParent(this.transform, false);

			// 大きさ
			rect.sizeDelta = new Vector2(-_settings.interval * 2, 20f);

			// 親がいる場合はpivotを調整
			if (_parent) {
				var pivot = rect.pivot;
				pivot.x = 1f;
				rect.pivot = pivot;
			}

			return elem;
		}

		/// <summary>
		/// 要素を生成した後処理
		/// </summary>
		/// <param name="elem">Element.</param>
		private void MakedElement(BuilderElement elem) {
			if (_isClosed) {
				elem.gameObject.SetActive(false);
			}
			AddElement(elem);
		}

		/// <summary>
		/// 要素の削除
		/// </summary>
		/// <param name="elem">Element.</param>
		public void RemoveElement(BuilderElement elem) {
			if (_elements.Remove(elem)) {
				Destroy(elem.gameObject);
				UpdateSize();
				UpdateElementsPosition();
			}
		}

		/// <summary>
		/// すべての要素の削除
		/// </summary>
		public void RemoveElements() {
			for (int i = 0; i < _elements.Count; ++i) {
				Destroy(_elements[i].gameObject);
			}
			_elements.Clear();
			UpdateSize();
			UpdateElementsPosition();
		}

		/// <summary>
		/// 実数値を編集するための要素を作成する
		/// </summary>
		/// <returns>The float value element.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		/// <param name="callback">Callback.</param>
		public FloatValueElement MakeFloatValue(string label, float value, float min, float max, UnityAction<float> callback) {
			var elem = MakeElement<FloatValueElement>(_settings.floatValueElement);

			elem.label.text = label;
			elem.valueField.SetValue(value, min, max);

			elem.valueField.onValueChanged.RemoveListener(callback);
			elem.valueField.onValueChanged.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// 文字列を編集するための要素を作成する
		/// </summary>
		/// <returns>The string value element.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="callback">Callback.</param>
		public StringValueElement MakeStringValue(string label, string value, UnityAction<string> callback) {
			var elem = MakeElement<StringValueElement>(_settings.stringValueElement);

			elem.label.text = label;
			elem.inputField.text = value;

			elem.inputField.onEndEdit.RemoveListener(callback);
			elem.inputField.onEndEdit.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// 真偽値を編集するための要素を作成する
		/// </summary>
		/// <returns>The bool value.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">If set to <c>true</c> value.</param>
		/// <param name="callback">Callback.</param>
		public BoolValueElement MakeBoolValue(string label, bool value, UnityAction<bool> callback) {
			var elem = MakeElement<BoolValueElement>(_settings.boolValueElement);

			elem.label.text = label;
			elem.toggle.isOn = value;

			elem.toggle.onValueChanged.RemoveListener(callback);
			elem.toggle.onValueChanged.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// ラベルを表示する要素を作成する
		/// </summary>
		/// <returns>The label element.</returns>
		/// <param name="label">Label.</param>
		public LabelElement MakeLabel(string label) {
			var elem = MakeElement<LabelElement>(_settings.labelElement);

			elem.label.text = label;

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// ボタンを表示する要素を作成する
		/// </summary>
		/// <returns>The label element.</returns>
		/// <param name="label">Label.</param>
		public ButtonElement MakeButton(string label, UnityAction callback) {
			var elem = MakeElement<ButtonElement>(_settings.buttonElement);

			elem.label.text = label;

			elem.button.onClick.RemoveListener(callback);
			elem.button.onClick.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// ドロップダウンを表示する要素を作成する
		/// </summary>
		/// <returns>The dropdown element.</returns>
		/// <param name="label">Label.</param>
		/// <param name="select">Select.</param>
		/// <param name="options">Options.</param>
		/// <param name="callback">Callback.</param>
		public DropdownElement MakeDropdown(string label, int select, string[] options, UnityAction<int> callback) {
			var elem = MakeElement<DropdownElement>(_settings.dropdownElement);

			elem.label.text = label;
			elem.dropdown.ClearOptions();
			elem.dropdown.AddOptions(new List<string>(options));
			elem.dropdown.value = select;

			elem.dropdown.onValueChanged.RemoveListener(callback);
			elem.dropdown.onValueChanged.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// グループ要素を作成する
		/// </summary>
		/// <returns>The element group.</returns>
		/// <param name="label">Label.</param>
		public GroupElement MakeGroup(string label) {
			var elem = MakeElement<GroupElement>(_settings.groupElement);

			elem.label.text = label;
			elem.builder._parent = this;
			elem.builder.settings = _settings;

			MakedElement(elem);

			return elem;
		}

		/// <summary>
		/// ドロップダウン付きのグループ要素を作成する
		/// </summary>
		/// <returns>The dropdown group.</returns>
		/// <param name="label">Label.</param>
		/// <param name="select">Select.</param>
		/// <param name="options">Options.</param>
		/// <param name="callback">Callback.</param>
		public DropdownGroupElement MakeDropdownGroup(string label, int select, string[] options, UnityAction<int> callback) {
			var elem = MakeElement<DropdownGroupElement>(_settings.dropdownGroupElement);

			elem.label.text = label;
			elem.builder._parent = this;
			elem.builder.settings = _settings;

			elem.dropdown.ClearOptions();
			elem.dropdown.AddOptions(new List<string>(options));
			elem.dropdown.value = select;

			elem.dropdown.onValueChanged.RemoveListener(callback);
			elem.dropdown.onValueChanged.AddListener(callback);

			MakedElement(elem);

			return elem;
		}

		#endregion
	}
}