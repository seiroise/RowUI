using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RowUI {

	/// <summary>
	/// 幾つかのUI要素をまとめて表示する要素
	/// </summary>
	public class UIGroupElement : UIBuilderElement {

		/// <summary>
		/// 開閉用ヘッダーボタン
		/// </summary>
		[SerializeField]
		private Button _headerButton;

		public Button headerButton {
			get {
				return _headerButton;
			}
		}

		[SerializeField]
		private UIBuilder _builder;

		public UIBuilder builder {
			get {
				return _builder;
			}
		}

		/// <summary>
		/// 閉じているか
		/// </summary>
		private bool _isClosed = false;

		protected override void Awake() {
			base.Awake();
			_headerButton.onClick.AddListener(OnHeaderButtonClicked);
		}

		/// <summary>
		/// 高さを返す
		/// </summary>
		/// <returns>The height.</returns>
		public override float GetHeight() {
			if (_isClosed) {
				return base.GetHeight();
			} else {
				return _builder.GetHeight() + rectTransform.sizeDelta.y + _builder.settings.interval;
			}
		}

		/// <summary>
		/// ヘッダーボタンのクリック
		/// </summary>
		private void OnHeaderButtonClicked() {
			if (_isClosed) {
				_isClosed = false;
				_builder.Open();
			} else {
				_isClosed = true;
				_builder.Close();
			}
		}
	}
}