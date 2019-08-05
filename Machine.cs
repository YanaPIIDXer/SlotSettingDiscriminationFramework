using System;
using System.Collections.Generic;

namespace SlotSettingDiscriminationFramework
{
	/// <summary>
	/// 台のデータを扱うクラス
	/// </summary>
	public abstract class Machine
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
		/// 設定期待値
		/// </summary>
		private float[] SettingExpection;

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
			SettingExpection = new float[6];
			for(int i = 0; i < 6; i++)
			{
				SettingExpection[i] = 100.0f / 6;
			}
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
		/// 設定のパーセンテージを取得。
		/// </summary>
		/// <param name="Setting">設定。１～６</param>
		/// <returns>パーセンテージ</returns>
		public float GetPercentage(int Setting)
		{
			int Index = Setting - 1;
			return SettingExpection[Index];
		}

		/// <summary>
		/// 判別要素を取得後、期待値を更新する。
		/// ※判別要素に関わる数値を変動させる場合はこのメソッドを使う事。
		/// </summary>
		/// <param name="Name">要素名</param>
		/// <param name="Callback">判別要素取得用コールバック</param>
		public void GetElementAndUpdatePercentage(string Name, Action<IElement> Callback)
		{
			if (!ElementDic.ContainsKey(Name)) { return; }
			Callback(ElementDic[Name]);
			UpdateExpection();
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

		/// <summary>
		/// 設定期待値の更新。
		/// </summary>
		protected void UpdateExpection()
		{
			// 一旦初期化
			for(int i = 0; i < 6; i++)
			{
				SettingExpection[i] = 0.0f;
			}

			// 確率を合成していく。
			foreach(var KeyValue in ElementDic)
			{
				var Expections = KeyValue.Value.GetSettingExpection(CurrentGameCount);
				for(int i = 0; i < 6; i++)
				{
					SettingExpection[i] += Expections[i];
				}
			}
			for(int i = 0; i < 6; i++)
			{
				SettingExpection[i] /= ElementDic.Count;
			}
		}
	}
}
