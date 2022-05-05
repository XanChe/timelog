using NUnit.Framework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Timelog;
using System;
using System.Linq;
using Timelog.Data;
using Timelog.Data.Repositories;
using Timelog.Core.Entities;

namespace Test.Timelog.DbLayer
{
    public class TestsRepositories
    {
        private static Guid TEST_ID_1 = new Guid("b5a68909-8c42-433e-bd81-f76aa92c2166");
        private static Guid TEST_ID_2 = new Guid("b049aaf2-61c5-435e-b4a7-018c95925878");
        private readonly DbConnection _connection;
        private readonly DbContextOptions<TimelogDbContext> _contextOptions;

        private readonly TimelogDbContext _context;

       public TestsRepositories()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<TimelogDbContext>()
                .UseSqlite(_connection)
                .Options;



            _context = new TimelogDbContext(_contextOptions);
            _context.Database.EnsureCreated();
        }

        public void Dispose() => _connection.Dispose();

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public void TestRepositoryCreation()
        {

            var repo = new DbRepositoryGeneric<Project>(_context);
            
            Assert.Pass();
        }

        [Test]
        public void TestRepositoryCRUD()
        {
            var repo = new DbRepositoryGeneric<Project>(_context);

            var newProject = new Project() { Name = "Coding", Description = "Just coding" };
            repo.Create(newProject);
            _context.SaveChanges();
            var project = repo.Read(newProject.Id);
            Assert.AreEqual("Coding", project.Name);
            project.Name = "Testing";

            repo.Update(project);
            _context.SaveChanges();

            var updatedProject = repo.Read(newProject.Id);

            Assert.AreEqual("Testing", updatedProject.Name);

            repo.DeleteAsync(newProject.Id);
            _context.SaveChanges();

            var deletedProject = repo.Read(newProject.Id);
            Assert.IsNull(deletedProject);
        }

        [Test]
        public void TestRepositoryManager()
        {
            var repoManager = new DbUnitOfWork(_context);
            var projetcs = repoManager.Projects;

            projetcs.Create(new Project() { Id = TEST_ID_1, Name = "Jon" });
            repoManager.SaveChanges();

            var project = projetcs.Read(TEST_ID_1);

            Assert.AreEqual("Jon", project.Name);
        }
        [Test]
        public void TestRepositoryUserFilter()
        {
            var repoManager = new DbUnitOfWork(_context);
            var projects = repoManager.Projects;
            var userGuid = Guid.NewGuid();

            projects.Create(new Project { Name = "Project without user" });
            repoManager.SaveChanges();

            repoManager.UseUserFilter(userGuid);
            projects.Create(new Project { Name = "Project with user 1" });
            projects.Create(new Project { Name = "Project with user 2" });
            repoManager.SaveChanges();

            var fitredProjects = projects.GetAll();
            var firstProject = fitredProjects.FirstOrDefault();

            Assert.AreNotEqual(firstProject.Name, "Project without user");

            Assert.AreEqual(2, fitredProjects.Count());

        }
    }
}