using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using Moq;
using Xunit;

namespace XUnitXamarinTests
{
    public class ClientServiceTests
    {
        [Fact]
        public void ClientServiceReturnsNotNullList()
        {
            Assert.NotNull(GetTestObjectWithList().TestData);
        }

        [Fact]
        public void ClientServiceReturnsListOfStrings()
        {
            Assert.Equal(2, GetTestObjectWithList().TestData.Count);
        }

        [Fact]
        public void ClientServiceReturnsListOfValidElements()
        {
            Assert.Equal("test data 1", GetTestObjectWithList().TestData.ElementAt(0));
        }

        [Fact]
        public void ClientServiceReturnsNotNullElementByIdIfListIsNotEmpty()
        {
            Assert.NotNull(GetTestObjectWithList().GetValue(0));
        }

        [Fact]
        public void ClientServiceReturnsValidElementById()
        {
            Assert.Equal("test data 1", GetTestObjectWithList().GetValue(0));
        }

        [Fact]
        public void ClientServiceAddValueWorksCorrectly()
        {
            var mock = new Mock<IClientService>();
            mock.Setup(x => x.TestData).Returns(GetTestData());
            mock.Setup(x => x.GetValue(0)).Returns(GetTestData().ElementAt(0));
            mock.Setup(x => x.AddValue("test value")).Callback(() =>
            {
                mock.Object.TestData.Add("test value");
            });
            var testService = mock.Object;
            testService.AddValue("test value");
            Assert.Equal("test value", testService.TestData.Last());
        }

        [Fact]
        public void ClientServiceRemoveValueWorksCorrectly()
        {
            var mock = new Mock<IClientService>();
            mock.Setup(x => x.TestData).Returns(GetTestData());
            mock.Setup(x => x.GetValue(0)).Returns(GetTestData().ElementAt(0));
            mock.Setup(x => x.RemoveValue("test data 2")).Callback(() =>
            {
                mock.Object.TestData.Remove("test data 2");
            });
            var testService = mock.Object;
            testService.RemoveValue("test data 2");

            Assert.Equal(1, testService.TestData.Count);
            Assert.Equal("test data 1", testService.TestData.ElementAt(0));
        }

        //Methods-helpers
        private IClientService GetTestObjectWithList()
        {
            var testList = GetTestData();

            var mock = new Mock<IClientService>();
            mock.Setup(x => x.TestData).Returns(testList);
            mock.Setup(x => x.GetValue(0)).Returns(testList.ElementAt(0));
            return mock.Object;
        }

        private IList<string> GetTestData()
        {
            return new List<string>
            {
                "test data 1",
                "test data 2"
            };
        }
    }
}
