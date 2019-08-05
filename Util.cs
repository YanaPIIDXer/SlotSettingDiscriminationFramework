﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SlotSettingDiscriminationFramework
{
	/// <summary>
	/// ユーティリティメソッド
	/// </summary>
	static class Util
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
			Prob = 1.0f / Prob;
			ulong Comb = CalcComb(X, Success);
			return (float)(Comb * Math.Pow(Prob, Success) * Math.Pow((1.0f - Prob), (X - Success)));
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