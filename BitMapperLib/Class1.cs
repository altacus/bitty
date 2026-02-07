using System;
using System.Collections;
using System.Numerics;

namespace BitMapperLib;

public class Class1
{
    /* Hard coded values for where each source bit (index) maps to a target bit */
    public static readonly short[] Mapping = [27, 3, 14, 0, 30, 8, 11, 22, 5, 17, 29, 1, 19, 12, 25, 7, 31, 10, 16, 2, 21, 15, 6, 28, 9, 20, 18, 4, 13, 23, 26, 24];

    private static readonly uint[] targetMasks = [.. Enumerable.Range(0, Mapping.Length).Select(i => 1u << Mapping[i])];

    private static readonly uint[] sourceMasks = BuildSourceMasks();

    private static uint[] BuildSourceMasks()
    {
        var masks = new uint[Mapping.Length];
        for (int i = 0; i < Mapping.Length; i++)
        {
            masks[Mapping[i]] = 1u << i;
        }
        return masks;
    }

    public static int Map(int value) => (int)ApplyMasks((uint)value, targetMasks);

    public static int Unmap(int mappedValue) => (int)ApplyMasks((uint)mappedValue, sourceMasks);

    private static uint ApplyMasks(uint v, uint[] masks)
    {
        uint result = 0;
        while (v != 0)
        {
            int tz = BitOperations.TrailingZeroCount(v);
            result |= masks[tz];
            v &= v - 1;
        }
        return result;
    }
}
