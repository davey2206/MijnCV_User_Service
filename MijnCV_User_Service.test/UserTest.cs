﻿using FluentAssertions;
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
    public class UserTest
    {
        private readonly UsersController _controller;
        private readonly UserServiceFake _service;
        public UserTest()
        {
            _service = new UserServiceFake();
            _controller = new UsersController(_service);
        }

        [Fact]
        public void GetAllUsersTest()
        {
            var expected = new List<User>()
            {
                new User { Id = 1, CV = "TestCV1", SubID = 12345},
                new User { Id = 2, CV = "TestCV2", SubID = 54321}
            };

            var Result = _controller.GetUsers();

            Assert.NotNull(Result);
            Assert.IsType<Task<ActionResult<IEnumerable<User>>>>(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void GetUsersTest()
        {
            User expected = new User { Id = 1, CV = "TestCV1", SubID = 12345 };
            var Result = _controller.GetUser(1);

            Assert.NotNull(Result);
            Assert.IsType<Task<ActionResult<User>>>(Result);
            Result.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void DeleteUserTest()
        {
            var expected = new List<User>()
            {
                new User { Id = 1, CV = "TestCV1", SubID = 12345},
            };

            var result = _service.DeleteUser(1);
            var test = _controller.GetUsers();

            Assert.IsType<Task<bool>>(result);
            test.Result.Value.Should().Equals(expected);
        }

        [Fact]
        public void PutUserTest()
        {

        }

        [Fact]
        public void PostUserTest()
        {
            var expected = new List<User>()
            {
                new User { Id = 1, CV = "TestCV1", SubID = 12345},
                new User { Id = 2, CV = "TestCV2", SubID = 54321},
                new User { Id = 3, CV = "TestCV3", SubID = 11111}
            };

            User User = new User { Id = 3, CV = "TestCV3", SubID = 11111 };

            var result = _service.PostUser(User);
            var test = _controller.GetUsers();

            Assert.IsType<Task<User>>(result);
            test.Result.Value.Should().Equals(expected);
        }
    }
}