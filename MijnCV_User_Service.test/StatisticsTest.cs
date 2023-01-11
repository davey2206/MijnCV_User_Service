using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MijnCV_User_Service.Controllers;
using MijnCV_User_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MijnCV_User_Service.Services;

namespace MijnCV_User_Service.test
{
    public class StatisticsTest
    {
        private readonly Mock<IStatisticsService> _service;

        public StatisticsTest()
        {
            _service = new Mock<IStatisticsService>();
        }

        [Fact]
        public void GetStatisticsByCvTest()
        {
            var data = GetStatisticsData();
            _service.Setup(x => x.GetStatisticsByCv("test")).Returns(async () => data[0] );
            var controller = new StatisticsController(_service.Object);

            var Result = controller.GetStatistics("test");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(data[0]);
        }

        [Fact]
        public void GetStatisticsByCvNoViewTest()
        {
            var expected = new Statistics { cv = "test2", Views = 0 };

            var data = GetStatisticsData();
            _service.Setup(x => x.GetStatisticsByCv("test2")).Returns(async () => new Statistics { cv = "test2", Views = 0});
            var controller = new StatisticsController(_service.Object);

            var Result = controller.GetStatistics("test");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void UpdateStatisticsViewsFirstTime()
        {
            var data = GetStatisticsData();
            _service.Setup(x => x.UpdateViewCountByCv("test2")).Returns(async () => data);
            var controller = new StatisticsController(_service.Object);

            controller.UpdateViewCountByCv("test2");
            var Result = controller.GetStatistics("test2");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(data);
        }

        [Fact]
        public void UpdateStatisticsViews()
        {
            var data = GetStatisticsData();
            _service.Setup(x => x.UpdateViewCountByCv("test")).Returns(async () => data);
            var controller = new StatisticsController(_service.Object);

            controller.UpdateViewCountByCv("test");
            var Result = controller.GetStatistics("test");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(data);
        }

        public List<Statistics> GetStatisticsData()
        {
            List<Statistics> statisticsData = new List<Statistics>()
            {
                new Statistics() { Id = 1, cv="test", Views = 5}
            }; 
            
            return statisticsData;

        }
    }
}
