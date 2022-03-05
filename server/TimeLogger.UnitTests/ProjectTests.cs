using System;
using Timelogger.Entities;
using Xunit;

namespace TimeLogger.UnitTests
{
    public class ProjectsTests
    {
        [Fact]
        public void Add_TimeLog_to_Project()
        {
            // Arrange
            var name = "E-commerce Project";
            var deadline = DateTime.Parse("20220731");
            var project = new Project(name, deadline);
            var timeLogDescription = "Initial Interviews";
            var timeLogDuration = 50;

            // Act
            var sut = project.AddTimeLog(timeLogDescription, timeLogDuration);

            // Assert
            Assert.Equal(timeLogDescription, sut.Description);
        }
    }
}
