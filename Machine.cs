using System;

namespace SlotSettingDiscriminationFramework
{
	/// <summary>
	/// 台のデータを扱うクラス
	/// </summary>
	public class Machine
	{
		/// <summary>
		/// 開始ゲーム数
		/// </summary>
		public int StartGameCount { get; set; }

		/// <summary>
		/// 現在ゲーム数
		/// </summary>
		public int CurrentGameCount { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Machine()
		{
			StartGameCount = 0;
			CurrentGameCount = 0;
		}
	}
}
