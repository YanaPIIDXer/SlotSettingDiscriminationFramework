using System;
using System.Collections.Generic;
using System.Text;

namespace SlotSettingDiscriminationFramework
{
	/// <summary>
	/// 判別要素インタフェース
	/// </summary>
	public interface IElement
	{
		/// <summary>
		/// 要素名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 設定毎の期待値を取得。
		/// ※必ず要素数が６の配列を返す事。
		/// </summary>
		/// <returns>設定毎の期待値</returns>
		float[] GetSettingExpection();
	}
}
