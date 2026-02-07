using NUnit.Framework;
using BitMapperLib;
namespace BitMapperLibTest;

public class MapTests
{
    [Test]
    public void SetsTargetBit_WhenSourceBitIsSet()
    {
        var sut = new Class1();
        int result = Class1.Map(1); // source bit 0 set
        int expected = 1 << Class1.Mapping[0];
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ClearsTargetBit_WhenSourceBitIsNotSet()
    {
        var sut = new Class1();
        int valueWithTargetSet = 1 << Class1.Mapping[0]; // target bit set, corresponding source (0) not set
        int result = Class1.Map(valueWithTargetSet);
        int targetBitMask = 1 << Class1.Mapping[0];
        Assert.That((result & targetBitMask), Is.EqualTo(0));
    }

    [Test]
    public void ClearsTargetBit_customMap()
    {
        var sut = new Class1();
        int valueWithTargetSet = 1 ; // target bit set, corresponding source (0) not set
        int result = Class1.Map(valueWithTargetSet);
        
        Assert.That(result, Is.EqualTo(Math.Pow(2, Class1.Mapping[0])));
    }

    [Test]
    public void ClearsTargetBit_customUnmap()
    {
        int valueWithTargetSet = 189482188;
        int result = Class1.Map(valueWithTargetSet);
        int resultUnmapped = Class1.Unmap(result);
        
        Assert.That(resultUnmapped, Is.EqualTo(valueWithTargetSet));
    }
}
