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
            plant.DefinePropertiesOf<House>(new
            {
                Color = "Red",
                SquareFoot = 1000
            });
        }


        [Test]
        public void BuildSimpleList()
        {
            var houses = plant.BuildList<House>();

            Assert.That(houses.Count(), Is.GreaterThan(0));
            foreach (var house in houses)
            {
                Assert.That(house.SquareFoot, Is.EqualTo(1000));
            }
        }

        [Test]
        public void BuildExactNumberOfItemsInAList()
        {
            var houses = plant.BuildList<House>(numberOfInstances: 3);

            Assert.That(houses.Count(), Is.EqualTo(3));
        }

        [Test]
        public void BuildAListOfVariations()
        {
            plant.DefineVariationOf<House>("appartment", new {SquareFoot = 400});
            var houses = plant.BuildList<House>(3, "appartment");
            foreach (var house in houses)
            {
                Assert.That(house.SquareFoot, Is.EqualTo(400));
            }
        }

        [Test]
        public void BuildAListWithAnInstance()
        {
            var example = new House{Color = "Green", SquareFoot = 800};
            var houses = plant.BuildList(3, example);
            Assert.That(houses.Count(), Is.EqualTo(3));
            foreach (var house in houses)
            {
                Assert.That(house, Is.Not.SameAs(example));
                Assert.That(house.Color, Is.EqualTo("Green"));
                Assert.That(house.SquareFoot, Is.EqualTo(800));
            }
        }
    }
}