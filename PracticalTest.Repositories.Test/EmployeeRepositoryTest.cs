using PracticalTest.Entities.Entities;

namespace PracticalTest.Repositories.Test
{
    public class EmployeeRepositoryTest : BaseRepositoryTest
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);

            // Act
            var result = EmployeeRepository.GetAll().Result;

            // Assert
            Assert.That(10, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id);

            // Act
            var result = EmployeeRepository.GetById(id).Result;

            // Assert
            Assert.That(id, Is.EqualTo(result.Id));
        }

        [Test]
        public void GetNotFoundTest()
        {
            // Setup
            var id = Guid.NewGuid();

            // Act
            var result = EmployeeRepository.GetById(id).Result;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CreateTest()
        {
            // Setup
            var id = Guid.NewGuid();

            var model = new Employee
            {
                Id = id,
                FullName = $"Name for {id}",
                BirthDate = DateTime.Now.AddYears(-1).AddMonths(-2).AddYears(-3),
                CreationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3)
            };

            // Act
            var result = EmployeeRepository.Create(model).Result;
            var dbRecord = EmployeeRepository.GetById(model.Id).Result;

            // Assert
            Assert.That(1, Is.EqualTo(result));
            Assert.That(model.FullName, Is.EqualTo(dbRecord.FullName));
            Assert.That(model.BirthDate, Is.EqualTo(dbRecord.BirthDate));
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            const string newName = "Updated Name";
            GenerateDbRecord(id);

            var model = EmployeeRepository.GetById(id).Result;
            model.FullName = newName;

            // Act
            var result = EmployeeRepository.Update(model).Result;
            var dbRecord = EmployeeRepository.GetById(model.Id).Result;

            // Assert
            Assert.IsTrue(result);
            Assert.That(newName, Is.EqualTo(dbRecord.FullName));
        }

        [Test]
        public void DeleteTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id);
            var model = EmployeeRepository.GetById(id).Result;

            // Act
            var result = EmployeeRepository.Delete(model).Result;
            var dbRecord = EmployeeRepository.GetById(model.Id).Result;

            // Assert
            Assert.That(1, Is.EqualTo(result));
            Assert.IsNull(dbRecord);
        }

        #region Private methods

        private void GenerateDbRecords(int numberRecords)
        {
            for (int i = 0; i < numberRecords; i++)
            {
                var id = Guid.NewGuid();
                Context.Employees.Add(new Employee
                {
                    Id = id,
                    FullName = $"Name for {id}",
                    BirthDate = DateTime.Now.AddYears(-i).AddMonths(-1).AddYears(-1),
                    CreationDate = DateTime.Now.AddDays(i).AddHours(i).AddMinutes(i),
                    ModificationDate = DateTime.Now.AddDays(i).AddHours(i).AddMinutes(i)
                });
            }

            Context.SaveChanges();
        }

        private void GenerateDbRecord(Guid id)
        {
            Context.Employees.Add(new Employee
            {
                Id = id,
                FullName = $"Name for {id}",
                BirthDate = DateTime.Now.AddYears(-1).AddMonths(-2).AddYears(-3),
                CreationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTime.Now.AddDays(1).AddHours(2).AddMinutes(3)
            });

            Context.SaveChanges();
        }

        #endregion
    }
}
