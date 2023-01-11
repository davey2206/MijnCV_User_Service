using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class UserTest
    {
        private readonly Mock<IUserService> _service;
        public UserTest()
        {
            _service = new Mock<IUserService>();
        }

        [Fact]
        public void GetAllUsersTest()
        {
            var data = GetUserData();
            _service.Setup(x => x.GetUsers()).Returns(async () => data);
            var controller = new UsersController(_service.Object);

            var Result = controller.GetUsers();

            Assert.NotNull(Result);
            Assert.IsType<Task<ActionResult<IEnumerable<User>>>>(Result);
            Result.Result.Value.Should().Equals(data);
        }

        [Fact]
        public void GetUsersTest()
        {
            var data = GetUserData();
            _service.Setup(x => x.GetUser(1)).Returns(async () => data[0]);
            var controller = new UsersController(_service.Object);

            var Result = controller.GetUser(1);

            Assert.NotNull(Result);
            Assert.IsType<Task<ActionResult<User>>>(Result);
            Result.Result.Value.Should().Equals(data[0]);
        }

        [Fact]
        public void DeleteUserTest()
        {
            var data = GetUserData();
            _service.Setup(x => x.DeleteUser(1)).Returns(async () => true);
            var controller = new UsersController(_service.Object);

            var Result = controller.DeleteUser(1);
            var Result2 = controller.GetUsers();

            Assert.NotNull(Result);
            Result2.Result.Value.Should().Equals(data);
        }

        [Fact]
        public void PutUserTest()
        {
            User user = new User() { Id = 1, CV = "TestCV", Email = "69" };

            var data = GetUserData();
            _service.Setup(x => x.PutUser(1, user)).Returns(async () => true);
            var controller = new UsersController(_service.Object);

            var Result = controller.PutUser(1, user);
            var Result2 = controller.GetUser(1);

            Assert.NotNull(Result);
            Result2.Result.Value.Should().Equals(data[0]);
        }

        [Fact]
        public void PostUserTest()
        {
            User User = new User { Id = 3, CV = "TestCV3", Email = "11111" };

            var data = GetUserData();
            _service.Setup(x => x.PostUser(User)).Returns(async () => data);
            var controller = new UsersController(_service.Object);

            var Result = controller.PostUser(User);
            var Result2 = controller.GetUsers();

            Assert.NotNull(Result);
            Assert.IsType<Task<ActionResult<User>>>(Result);
            Result2.Result.Value.Should().Equals(data);
        }

        public List<User> GetUserData()
        {
            List<User> user = new List<User>()
            {
                new User { Id = 1, CV = "TestCV1", Email = "12345"},
                new User { Id = 2, CV = "TestCV2", Email = "54321"}
            };

            return user;
        }
    }
}
