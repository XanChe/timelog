using NUnit.Framework;
using Timelog.Repositories;
using Timelog.EF;
using Timelog.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Timelog;
using System;
using System.Linq;

namespace Test.Timelog.DbLayer
{
    public class TestsRepositories
    {
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

            repo.Create(new Project() { Id = 1, Name = "Coding", Description = "Just coding" });
            _context.SaveChanges();
            var project = repo.Read(1);
            Assert.AreEqual("Coding", project.Name);
            project.Name = "Testing";

            repo.Update(project);
            _context.SaveChanges();

            var updatedProject = repo.Read(1);

            Assert.AreEqual("Testing", updatedProject.Name);

            repo.Delete(1);
            _context.SaveChanges();

            var deletedProject = repo.Read(1);
            Assert.IsNull(deletedProject);
        }

        [Test]
        public void TestRepositoryManager()
        {
            var repoManager = new DbRepositoryManager(_context);
            var projetcs = repoManager.Projects;

            projetcs.Create(new Project() { Id = 1, Name = "Jon" });
            repoManager.SaveChanges();

            var project = projetcs.Read(1);

            Assert.AreEqual("Jon", project.Name);
        }
        [Test]
        public void TestRepositoryUserFilter()
        {
            var repoManager = new DbRepositoryManager(_context);
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