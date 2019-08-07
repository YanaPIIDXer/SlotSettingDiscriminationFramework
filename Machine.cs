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
		public int StartGameCount
		{
			get { return _StartGameCount; }
			set
			{
				_StartGameCount = value;
				UpdateExpection();
			}
		}
		private int _StartGameCount;

		/// <summary>
		/// 現在ゲーム数
		/// </summary>
		public int CurrentGameCount
		{
			get { return _CurrentGameCount; }
			set
			{
				_CurrentGameCount = value;
				UpdateExpection();
			}
		}
		private int _CurrentGameCount;

		/// <summary>
		/// 期待値が変動した時のイベント
		/// </summary>
		public Action OnExpectionChanged { set; private get; }

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
			_StartGameCount = 0;
			_CurrentGameCount = 0;
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
		/// 設定の期待値を取得。
		/// </summary>
		/// <param name="Setting">設定。１～６</param>
		/// <returns>期待値</returns>
		public float GetExpection(int Setting)
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
			bool[] IsDenied = new bool[6];
			int DenyCount = 0;

			// 初期化
			for(int i = 0; i < 6; i++)
			{
				SettingExpection[i] = 0.0f;
				IsDenied[i] = false;
			}

			// 期待値を合成していく。
			foreach(var KeyValue in ElementDic)
			{
				var Expections = KeyValue.Value.GetSettingExpection(CurrentGameCount);
				for(int i = 0; i < 6; i++)
				{
					SettingExpection[i] += Expections[i];
					if(Expections[i] == 0.0f && !IsDenied[i])
					{
						// 設定を否定。（４５６確等）
						IsDenied[i] = true;
						DenyCount++;
					}
				}
			}

			// 設定が否定された期待値は他の設定に分配される。
			if(DenyCount > 0)
			{
				float DenyValue = 0.0f;
				for (int i = 0; i < 6; i++)
				{
					if (IsDenied[i])
					{
						DenyValue += SettingExpection[i];
						SettingExpection[i] = 0.0f;
					}
				}
				if(DenyValue > 0.0f)
				{
					for (int i = 0; i < 6; i++)
					{
						if (!IsDenied[i])
						{
							SettingExpection[i] += DenyValue / (6 - DenyCount);
						}
					}
				}
			}

			for (int i = 0; i < 6; i++)
			{
				if(SettingExpection[i] == 0.0f) { continue; }
				SettingExpection[i] /= ElementDic.Count;
			}

			if(OnExpectionChanged != null)
			{
				OnExpectionChanged();
			}
		}
	}
}
