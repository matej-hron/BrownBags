using FsCheck;
using NUnit.Framework;

namespace BBUnitTesting
{
    [TestFixture]
    public class PropBasedTests
    {
        [Test]
        public void Associativity()
        {
            var sut = new Calculator();
            Prop.ForAll<int, int>((x, y) => sut.Add(x, y) == sut.Add(y, x)).QuickCheckThrowOnFailure();
        }

        [Test]
        public void Identity()
        {
            var sut = new Calculator();
            Prop.ForAll<int>((x) => sut.Add(x, 0) == x).QuickCheckThrowOnFailure();
        }


        [Test]
        public void AssociativitySub()
        {
            var sut = new Calculator();
            Prop.ForAll<int, int>((x, y) => sut.Sub(x, y) == sut.Sub(y, x)).QuickCheckThrowOnFailure();
        }
    }

    public class Calculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Sub(int x, int y) => x - y;
    }
}