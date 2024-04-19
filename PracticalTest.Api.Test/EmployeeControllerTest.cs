using Castle.Core.Resource;
using PracticalTest.Api.Controllers;
using PracticalTest.Common.Dtos;
using PracticalTest.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalTest.Api.Test
{
    public class EmployeeControllerTest : BaseControllerTest<EmployeeController>
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);
            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Get().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(OkObjectResult)));
            Assert.That((result as OkObjectResult)?.Value.GetType(), Is.SameAs(typeof(List<EmployeeResponseDto>)));
            Assert.That(((result as OkObjectResult)?.Value as List<EmployeeResponseDto>)?.Count, Is.EqualTo(10));
        }

        [Test]
        public void GetTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, "Some Full Name", DateTime.Now.AddYears(-10));

            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Get(id).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(OkObjectResult)));
            Assert.That((result as OkObjectResult)?.Value.GetType(), Is.SameAs(typeof(EmployeeResponseDto)));
        }

        [Test]
        public void CreateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            var modelDto = new EmployeeCreateDto
            {
                FullName = $"First Name for {id}",
                BirthDate = DateTime.Now.AddYears(-10)
            };

            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Post(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(CreatedResult)));
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, "Full Name", DateTime.Now.AddYears(-10));
            
            var modelDto = new EmployeeUpdateDto
            {
                Id = id,
                FullName = $"New Name",
                BirthDate = DateTime.Now.AddYears(-15)
            };

            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Put(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(StatusCodeResult)));
        }

        [Test]
        public void DeleteTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, "Full Name", DateTime.Now.AddYears(-10));

            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Delete(id).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(StatusCodeResult)));
        }

        [Test]
        public void DeleteNotFoundTest()
        {
            // Setup
            var id = Guid.NewGuid();

            var controller = new EmployeeController(LoggerController.Object, EmployeeDomain, Mapper);

            // Act
            var result = controller.Delete(id).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetType(), Is.SameAs(typeof(NotFoundObjectResult)));
        }

        #region Private Methods

        private void GenerateDbRecords(int numberRecords)
        {
            var employeeList = new List<Employee>();
            for (int i = 0; i < numberRecords; i++)
            {
                var id = Guid.NewGuid();
                employeeList.Add(new Employee
                {
                    Id = id,
                    FullName = $"Name for {id}",
                    BirthDate = DateTime.Now.AddYears(-i).AddMonths(-1).AddYears(-1),
                    CreationDate = DateTime.Now.AddDays(i).AddHours(i).AddMinutes(i),
                    ModificationDate = DateTime.Now.AddDays(i).AddHours(i).AddMinutes(i)
                });
            }

            EmployeeRepositoryMock.Setup(x => x.GetAll(null, null)).ReturnsAsync(employeeList.AsQueryable());
        }

        private void GenerateDbRecord(Guid id, string fullName, DateTime birthDate)
        {
            var employee = new Employee
            {
                Id = id,
                FullName = fullName,
                BirthDate = birthDate,
                CreationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3)
            };

            EmployeeRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(employee);
        }

        #endregion
    }
}
