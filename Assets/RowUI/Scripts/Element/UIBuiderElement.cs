using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace RowUI {

	/// <summary>
	/// UIBuilderで扱う要素
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
	public abstract class UIBuilderElement : MonoBehaviour {

		/// <summary>
		/// 背景
		/// </summary>
		[SerializeField]
		protected Image _background;

		public Image background {
			get {
				return _background;
			}
		}

		/// <summary>
		/// ラベル
		/// </summary>
		[SerializeField]
		protected Text _label;

		public Text label {
			get {
				return _label;
			}
		}

		/// <summary>
		/// rectTransform
		/// </summary>
		private RectTransform _rectTransform;

		public RectTransform rectTransform {
			get {
				return _rectTransform;
			}
		}

		protected virtual void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}

		/// <summary>
		/// 高さを返す
		/// </summary>
		/// <returns>The height.</returns>
		public virtual float GetHeight() {
			return _rectTransform.sizeDelta.y;
		}

		/// <summary>
		/// 要素の最小のy座標を返す
		/// </summary>
		/// <returns>The minimum.</returns>
		public virtual float GetMin() {
			float min = _rectTransform.anchoredPosition.y;
			min -= _rectTransform.sizeDelta.y;
			return min;
		}
	}
}