using Innoloft_Backend_Domain;
using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Services.Events;
using Innoloft_Backend_Persistence.Contexts;
using Innoloft_Backend_Persistence.Repositories;
using Innoloft_Backend_Persistence.Repositories.Events;
using Moq;
using System;

namespace Innoloft.UnitTest
{
    public class Event_Test
    {
        [Fact]
        public async void Add_Event_ShouldReturnSuccess()
        {
            //Arrange
            var dbc = new Mock<AppDbContext>();
            var u = new Mock<UnitOfWork>(dbc.Object);
            var rep = new Mock<EventRepository>(dbc.Object, u.Object);
            var ser = new EventService(rep.Object);


            //Action
            var query = await ser.CreateAsync(new Event 
            {
                Title = "test",
                Description = "test",  
                CreatedById = "id"
            });

            // Assert
            Assert.True(query.Succeeded);
        }
    }
}