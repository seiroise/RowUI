using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RowUI {

	/// <summary>
	/// インスタンス内の変数をグループとして扱う要素
	/// </summary>
	public class InstanceElement : GroupElement {

		private static readonly string _intName = typeof(int).FullName;
		private static readonly string _floatName = typeof(float).FullName;
		private static readonly string _stringName = typeof(string).FullName;
		private static readonly string _boolName = typeof(bool).FullName;

		private object _ins;

		/// <summary>
		/// 表示するインスタンスを設定する
		/// </summary>
		/// <param name="ins">Instance.</param>
		public void SetInstance(object ins) {
			_ins = ins;
			MakeByInstance(ins);
		}

		/// <summary>
		/// インスタンスを元にUIを作成する
		/// </summary>
		private void MakeByInstance(object ins) {
			Type type = ins.GetType();

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
			for (int i = 0; i < fields.Length; ++i) {
				MakeByField(ins, fields[i]);
			}

			fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			for (int i = 0; i < fields.Length; ++i) {
				var attr = Attribute.GetCustomAttribute(fields[i], typeof(SerializeField));
				if (attr != null) {
					MakeByField(ins, fields[i]);
				}
			}
		}

		/// <summary>
		/// フィールド情報を元にUIを作成する
		/// </summary>
		/// <param name="ins">Ins.</param>
		/// <param name="info">Info.</param>
		private void MakeByField(object ins, FieldInfo info) {
			Type type = info.FieldType;
			string name = type.FullName;
			if (name == _intName) {
				builder.MakeInt(info.Name, (int)info.GetValue(ins), (int v) => { info.SetValue(ins, v); });
			} else if (name == _floatName) {
				builder.MakeFloat(info.Name, (float)info.GetValue(ins), (float v) => { info.SetValue(ins, v); });
			} else if (name == _stringName) {
				builder.MakeString(info.Name, (string)info.GetValue(ins), (string v) => { info.SetValue(ins, v); });
			} else if (name == _boolName) {
				builder.MakeBool(info.Name, (bool)info.GetValue(ins), (bool v) => { info.SetValue(ins, v); });
			}
		}
	}
}