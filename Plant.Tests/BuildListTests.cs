using System.Linq;
using NUnit.Framework;
using Plant.Core;
using Plant.Tests.TestModels;

namespace Plant.Tests
{
    [TestFixture]
    public class BuildListTests
    {
        private BasePlant plant;

        [SetUp]
        public void SetUp()
        {
            plant = new BasePlant();
            plant.DefinePropertiesOf<House>(new { SquareFoot = 1000 });
        }


        [Test]
        public void CreateSimpleList()
        {
            var houses = plant.BuildList<House>();

            Assert.That(houses.Count(), Is.GreaterThan(0));
            foreach (var house in houses)
            {
                Assert.That(house, Is.Not.Null);
            }
        }

        [Test]
        public void CreateExactNumberOfItemsInAList()
        {
            var houses = plant.BuildList<House>(numberOfInstances: 3);

            Assert.That(houses.Count(), Is.EqualTo(3));
        }

        [Test]
        public void CreateAListOfVariations()
        {
            plant.DefineVariationOf<House>("appartment", new {SquareFoot = 400});
            var houses = plant.BuildList<House>(3, "appartment");
            foreach (var house in houses)
            {
                Assert.That(house.SquareFoot, Is.EqualTo(400));
            }
        }

    }
}