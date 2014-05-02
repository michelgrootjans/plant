using System.Linq;
using NUnit.Framework;
using Plant.Core;
using Plant.Tests.TestModels;

namespace Plant.Tests
{
    [TestFixture]
    public class CreateListTests
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
        public void CreateExactNumberOfItemsInAList()
        {
            var houses = plant.CreateListOf<House>(3);

            Assert.That(houses.Count(), Is.EqualTo(3));
            foreach (var house in houses)
            {
                Assert.That(house.Color, Is.EqualTo("Red"));
                Assert.That(house.SquareFoot, Is.EqualTo(1000));
            }
        }

        [Test]
        public void CreateWithCustomProperties()
        {
            var houses = plant.CreateListOf<House>(3, new {Color = "Green"});

            Assert.That(houses.Count(), Is.EqualTo(3));
            foreach (var house in houses)
            {
                Assert.That(house.Color, Is.EqualTo("Green"));
                Assert.That(house.SquareFoot, Is.EqualTo(1000));
            }
        }

        [Test]
        public void CreateVariations()
        {
            plant.DefineVariationOf<House>("appartment", new {SquareFoot = 400});
            var houses = plant.CreateListOf<House>(3, variation: "appartment");

            foreach (var house in houses)
            {
                Assert.That(house.Color, Is.EqualTo("Red"));
                Assert.That(house.SquareFoot, Is.EqualTo(400));
            }
        }

        [Test]
        public void CreateWithCallback()
        {
            var eventcounter = 0;
            plant.BluePrintCreated += (sender, e) => eventcounter++;
            plant.CreateListOf<House>(3).ToList(); //force traversal

            Assert.AreEqual(eventcounter, 3);
        }

        [Test]
        public void BuildAListWithAnInstance()
        {
            var example = new House{Color = "Green", SquareFoot = 800};
            var houses = plant.BuildListOf(3, example);

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