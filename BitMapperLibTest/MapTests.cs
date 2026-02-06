using NUnit.Framework;
namespace BitMapperLibTest;

public class MapTests
{
    [Test]
    public void SetsTargetBit_WhenSourceBitIsSet()
    {
        var sut = new BitMapperLib.Class1();
        int result = sut.Map(1); // source bit 0 set
        int expected = 1 << sut.mapping[0];
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ClearsTargetBit_WhenSourceBitIsNotSet()
    {
        var sut = new BitMapperLib.Class1();
        int valueWithTargetSet = 1 << sut.mapping[0]; // target bit set, corresponding source (0) not set
        int result = sut.Map(valueWithTargetSet);
        int targetBitMask = 1 << sut.mapping[0];
        Assert.That((result & targetBitMask), Is.EqualTo(0));
    }

    [Test]
    public void ClearsTargetBit_custom()
    {
        var sut = new BitMapperLib.Class1();
        int valueWithTargetSet = 1 ; // target bit set, corresponding source (0) not set
        int result = sut.Map(valueWithTargetSet);
        
        Assert.That(result, Is.EqualTo(Math.Pow(2, sut.mapping[0])));
    }
}
