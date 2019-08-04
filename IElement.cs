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
		/// 判別要素の強さ
		/// </summary>
		float Weight { get; }
	}
}
