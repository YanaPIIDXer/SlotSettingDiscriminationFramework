using System;
using System.Collections.Generic;

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
		/// 自分が回したゲーム数
		/// </summary>
		public int GameCount
		{
			get
			{
				return (CurrentGameCount - StartGameCount);
			}
		}

		/// <summary>
		/// 判別要素ディクショナリ
		/// </summary>
		private Dictionary<string, IElement> ElementDic;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Machine()
		{
			StartGameCount = 0;
			CurrentGameCount = 0;
			ElementDic = new Dictionary<string, IElement>();
		}
		
		/// <summary>
		/// 判別要素オブジェクト取得
		/// </summary>
		/// <param name="Name">要素名</param>
		/// <returns>判別要素オブジェクト</returns>
		public IElement GetElement(string Name)
		{
			if(!ElementDic.ContainsKey(Name)) { return null; }
			return ElementDic[Name];
		}

		/// <summary>
		/// 判別要素追加
		/// </summary>
		/// <param name="Element">判別要素オブジェクト</param>
		protected void AddElement(IElement Element)
		{
			if (ElementDic.ContainsKey(Element.Name)) { return; }
			ElementDic.Add(Element.Name, Element);
		}
	}
}
