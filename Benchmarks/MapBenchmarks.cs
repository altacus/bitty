using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BitMapperLib;

namespace Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class MapBenchmarks
{
    private readonly Class1 mapper = new();

    private int[] inputsOneBit;
    private int[] inputsFourBits;
    private int[] inputsHalf;
    private int[] inputsAll;

    [GlobalSetup]
    public void Setup()
    {
        // prepare inputs of varying densities
        inputsOneBit = new int[32];
        for (int i = 0; i < 32; i++) inputsOneBit[i] = 1 << i;

        inputsFourBits = new int[1000];
        var rnd = new Random(42);
        for (int i = 0; i < inputsFourBits.Length; i++)
        {
            int v = 0;
            for (int b = 0; b < 4; b++) v |= 1 << rnd.Next(0, 32);
            inputsFourBits[i] = v;
        }

        inputsHalf = new int[1000];
        for (int i = 0; i < inputsHalf.Length; i++)
        {
            int v = 0;
            for (int b = 0; b < 16; b++) v |= 1 << rnd.Next(0, 32);
            inputsHalf[i] = v;
        }

        inputsAll = new int[1] { unchecked((int)0xFFFFFFFF) };
    }

    [Benchmark(Baseline = true)]
    public int Map_OneBit()
    {
        int sum = 0;
        foreach (var v in inputsOneBit) sum += mapper.Map(v);
        return sum;
    }

    [Benchmark]
    public int Map_FourBits()
    {
        int sum = 0;
        foreach (var v in inputsFourBits) sum += mapper.Map(v);
        return sum;
    }

    [Benchmark]
    public int Map_Half()
    {
        int sum = 0;
        foreach (var v in inputsHalf) sum += mapper.Map(v);
        return sum;
    }

    [Benchmark]
    public int Map_All()
    {
        int sum = 0;
        foreach (var v in inputsAll) sum += mapper.Map(v);
        return sum;
    }
}
