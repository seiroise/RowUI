using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace RowUI {

	/// <summary>
	/// 新しいUIビルダーのテスト
	/// </summary>
	public class UIBuilder : UIBuilderElement {

		/// <summary>
		/// ビルダーのセッテイング
		/// </summary>
		[SerializeField]
		private UIBuilderSettings _settings;

		public UIBuilderSettings settings {
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
		private List<UIBuilderElement> _elements;

		/// <summary>
		/// 親要素
		/// </summary>
		private UIBuilder _parent;

		protected override void Awake() {
			base.Awake();
			_elements = new List<UIBuilderElement>();
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
		private void AddElement(UIBuilderElement elem) {
			_elements.Add(elem);
			UpdateSize();
			UpdateElementsPosition();
		}

		/// <summary>
		/// 要素の削除
		/// </summary>
		/// <param name="elem">Element.</param>
		public void RemoveElement(UIBuilderElement elem) {
			if (_elements.Remove(elem)) {
				Destroy(elem.gameObject);
				UpdateSize();
				UpdateElementsPosition();
			}
		}

		/// <summary>
		/// 指定した要素を生成する
		/// </summary>
		/// <returns>The element.</returns>
		/// <param name="prefab">Prefab.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private T MakeElement<T>(T prefab) where T : UIBuilderElement {
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
		/// 実数値を編集するための要素を作成する
		/// </summary>
		/// <returns>The float value element.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		/// <param name="callback">Callback.</param>
		public UIFloatValueElement MakeFloatValueElement(string label, float value, float min, float max, UnityAction<float> callback) {
			var elem = MakeElement<UIFloatValueElement>(_settings.floatValueElement);

			elem.label.text = label;
			elem.valueField.SetValue(value, min, max);

			elem.valueField.onValueChanged.RemoveListener(callback);
			elem.valueField.onValueChanged.AddListener(callback);

			AddElement(elem);

			return elem;
		}

		/// <summary>
		/// 文字列を編集するための要素を作成する
		/// </summary>
		/// <returns>The string value element.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="callback">Callback.</param>
		public UIStringValueElement MakeStringValueElement(string label, string value, UnityAction<string> callback) {
			var elem = MakeElement<UIStringValueElement>(_settings.stringValueElement);

			elem.label.text = label;
			elem.inputField.text = value;

			elem.inputField.onEndEdit.RemoveListener(callback);
			elem.inputField.onEndEdit.AddListener(callback);

			AddElement(elem);

			return elem;
		}


		/// <summary>
		/// ラベルを表示する要素を作成する
		/// </summary>
		/// <returns>The label element.</returns>
		/// <param name="label">Label.</param>
		public UILabelElement MakeLabelElement(string label) {
			var elem = MakeElement<UILabelElement>(_settings.labelElement);

			elem.label.text = label;

			AddElement(elem);

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
		public UIDropdownElement MakeDropdownElement(string label, int select, string[] options, UnityAction<int> callback) {
			var elem = MakeElement<UIDropdownElement>(_settings.dropdownElement);

			elem.label.text = label;
			elem.dropdown.ClearOptions();
			elem.dropdown.AddOptions(new List<string>(options));

			elem.dropdown.onValueChanged.RemoveListener(callback);
			elem.dropdown.onValueChanged.AddListener(callback);

			AddElement(elem);

			return elem;
		}

		/// <summary>
		/// グループ要素を作成する
		/// </summary>
		/// <returns>The element group.</returns>
		/// <param name="label">Label.</param>
		public UIGroupElement MakeElementGroup(string label) {
			var elem = MakeElement<UIGroupElement>(_settings.groupElement);

			elem.label.text = label;
			elem.builder._parent = this;
			elem.builder.settings = _settings;

			AddElement(elem);

			return elem;
		}

		#endregion
	}
}