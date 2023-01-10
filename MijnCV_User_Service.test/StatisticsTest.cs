using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MijnCV_User_Service.Controllers;
using MijnCV_User_Service.Models;
using MijnCV_User_Service.test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnCV_User_Service.test
{
    public class StatisticsTest
    {
        private readonly StatisticsController _controller;
        private readonly StatisticsServiceFake _service;

        public StatisticsTest()
        {
            _service = new StatisticsServiceFake();
            _controller = new StatisticsController(_service);
        }

        [Fact]
        public void GetStatisticsByCvTest()
        {
            var expected = new Statistics { Id = 1, cv = "test", Views = 5 };

            var Result = _controller.GetStatistics("test");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void GetStatisticsByCvNoViewTest()
        {
            var expected = new Statistics { cv = "test2", Views = 0 };

            var Result = _controller.GetStatistics("test2");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void UpdateStatisticsViewsFirstTime()
        {
            var expected = new Statistics { Id = 2, cv = "test2", Views = 1 };

            _controller.UpdateViewCountByCv("test2");

            var Result = _controller.GetStatistics("test2");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void UpdateStatisticsViews()
        {
            var expected = new Statistics { Id = 1, cv = "test", Views = 6 };

            _controller.UpdateViewCountByCv("test");

            var Result = _controller.GetStatistics("test");

            Assert.IsType<Task<ActionResult<Statistics>>>(Result);
            Assert.NotNull(Result);
            Result.Result.Value.Should().Equals(expected);
        }
    }
}
