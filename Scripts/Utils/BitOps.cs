using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace JumpGuy.Utils
{
	public static class BitOps
	{
		[MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
		public static int PopCount(int value)
		{
			return PopCount((uint)value);
		}

		[MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
		public static int PopCount(uint value)
		{
#if NET8_0_OR_GREATER
			BitOperations.PopCount(value)
#else
			return Popcnt.IsSupported
				? (int)Popcnt.PopCount(value)
				: BitOperations.PopCount(value);
#endif
		}

		[MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
		public static int PopCount(long value)
		{
			return PopCount((ulong)value);
		}

		[MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
		public static int PopCount(ulong value)
		{
#if NET8_0_OR_GREATER
			BitOperations.PopCount(value)
#else
			return Popcnt.X64.IsSupported
				? (int)Popcnt.X64.PopCount(value)
				: BitOperations.PopCount(value);
#endif
		}
	}
}
