using System;
using System.Collections.Generic;
using System.Text;

namespace SlotSettingDiscriminationFramework
{
	/// <summary>
	/// ユーティリティメソッド
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// 試行回数Ｘに対する二項分布の確率を計算。
		/// </summary>
		/// <param name="X">試行回数</param>
		/// <param name="Success">成功回数</param>
		/// <param name="Prob">成功確率</param>
		/// <returns>二項分布の確率</returns>
		public static float CalcBinomDist(ulong X, ulong Success, float Prob)
		{
			ulong Comb = CalcComb(X, Success);
			return (float)(Comb * Math.Pow(Prob, Success) * Math.Pow((1.0f - Prob), (X - Success)));
		}

		/// <summary>
		/// 二項分布の確率から期待値に変換
		/// </summary>
		/// <param name="BinomDists">二項分布の確率</param>
		/// <param name="SettingLevel">設定段階</param>
		/// <returns>期待値</returns>
		public static float[] BinomDistToExpection(float[] BinomDists, int SettingLevel = 6)
		{
			float Total = 0.0f;
			for(int i = 0; i < SettingLevel; i++)
			{
				Total += BinomDists[i];
			}
			float[] Expections = new float[SettingLevel];
			if (Total == 0.0f)
			{
				// あまりにも極端な数値が入った場合はBinomDistが全て0になっている可能性がある。
				// その場合は実質最高設定確定として扱う。
				// （だからと言って他の設定に0.0fを突っ込むと確定演出が出た扱いとなってしまうので小さな値は放り込んでおく。）
				float MinimumValue = 0.00001f * SettingLevel;
				for (int i = 0; i < SettingLevel - 1; i++)
				{
					Expections[i] = MinimumValue;
				}
				Expections[SettingLevel - 1] = 100.0f - MinimumValue;
				return Expections;
			}

			for (int i = 0; i < SettingLevel; i++)
			{
				Expections[i] = BinomDists[i] / Total * 100;
			}

			return Expections;
		}

		/// <summary>
		/// 異なるn個のものからm個を選ぶ組み合わせの総和（nCm）を計算。
		/// </summary>
		/// <param name="n">n</param>
		/// <param name="m">m</param>
		/// <returns>組み合わせの総和</returns>
		private static ulong CalcComb(ulong n, ulong m)
		{
			// 参考資料：https://zebratch.blog.so-net.ne.jp/2009-01-01
			if (m == 0) { return 1; }
			return CalcComb(n, m - 1) * (n - m + 1) / m;
		}
	}
}
