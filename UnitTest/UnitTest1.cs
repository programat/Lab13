using Lab13;

namespace UnitTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestSorting()
    {
        Zoo TestZoo = new Zoo();
        TestZoo.Add_elem(new Mammals(1, 3, "B"));
        TestZoo.Add_elem(new Mammals(2, 2, "A"));
        TestZoo.Sort(3); //sorting by name
        Assert.AreEqual(2, TestZoo.GetAnimals[0].number);
    }

    [ExpectedException(typeof(ArgumentException))]
    [TestMethod]
    public void TestFind()
    {
        Zoo TestZoo = new Zoo();
        TestZoo.Find(0);
    }
}