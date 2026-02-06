using System;
using System.Numerics;

namespace BitMapperLib;

public class Class1
{
    /* Hard coded values for where each source bit (index) maps to a target bit */
    public static short[] mapping = [27, 3, 14, 0, 30, 8, 11, 22, 5, 17, 29, 1, 19, 12, 25, 7, 31, 10, 16, 2, 21, 15, 6, 28, 9, 20, 18, 4, 13, 23, 26, 24];

    private readonly uint[] targetMasks;
    private readonly uint[] sourceMasks;

    public Class1()
    {
        targetMasks = new uint[mapping.Length];
        for (int i = 0; i < mapping.Length; i++)
        {
            targetMasks[i] = 1u << mapping[i];
        }
        sourceMasks = new uint[mapping.Length];
        for (int i = 0; i < mapping.Length; i++)
        {
            // mapping[i] is the target for source i, so set the source mask at the target index
            sourceMasks[mapping[i]] = 1u << i;
        }
    }

    public int Map(int value)
    {
        return (int)ApplyMasks((uint)value, targetMasks);
    }

    public int Unmap(int mappedValue)
    {
        return (int)ApplyMasks((uint)mappedValue, sourceMasks);
    }

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
