using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck.Management.Test.Application.Interfaces;
using Truck.Management.Test.Application.Models;
using Truck.Management.Test.Application.Notifications;
using Truck.Management.Test.Application.Services;
using Truck.Management.Test.Domain.Interfaces;
using Truck.Management.Test.Domain.Models;
using Xunit;

namespace Truck.Management.Test.Application.UnitTest
{
    public class TruckApplicationTest
    {
        private readonly TruckApplication _truckApplication;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<ITruckRepository> _truckRepository;
        private readonly Mock<ApplicationNotificationHandler> _notifications;

        public TruckApplicationTest()
        {
            _mediator = new Mock<IMediatorHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _truckRepository = new Mock<ITruckRepository>();
            _notifications = new Mock<ApplicationNotificationHandler>();

            _truckApplication = new TruckApplication(_unitOfWork.Object, _mediator.Object, _notifications.Object, _truckRepository.Object);
        }

        [Fact]
        public async Task ShouldReturnTruckIfAddWithSucessTruckFM()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckdto = new TruckDto { AnoModelo = 2022, Cor = "blue", DigitalPanel = false, Modelo = "FM" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().NotBeNull();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnTruckIfAddWithSucessTruckFH()
        {
            //Arrange
            var truck = new TruckFH("black", (int)ModelTruckEnum.FH, DateTime.Now.Year + 1, true);
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckdto = new TruckDto { AnoModelo = 2022, Cor = "black", DigitalPanel = true, Modelo = "FH" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().NotBeNull();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnNullIfModelYearIsNoTValid()
        {
            //Arrange
            var truckdto = new TruckDto { AnoModelo = 2019, Cor = "black", DigitalPanel = true, Modelo = "FH" };

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Invalid truck model year!")));
        }

        [Fact]
        public async Task ShouldReturnNullIfColorIsEmpty()
        {
            //Arrange
            var truckdto = new TruckDto { AnoModelo = DateTime.Now.Year, Cor = "", DigitalPanel = true, Modelo = "FH" };

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "The core is mandatory")));
        }

        [Fact]
        public async Task ShouldReturnNullIfColorIsLong()
        {
            //Arrange
            var truckdto = new TruckDto { AnoModelo = DateTime.Now.Year, Cor = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", DigitalPanel = true, Modelo = "FM" };

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "The maximum length of the color is 100")));
        }

        [Fact]
        public async Task ShouldReturnNullIfProblemToCommited()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _unitOfWork.Setup(x => x.Commit()).Returns(false);
            var truckdto = new TruckDto { AnoModelo = 2022, Cor = "blue", DigitalPanel = false, Modelo = "FM" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.AddTruck(truckdto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Problem to register the truck!")));
        }

        [Fact]
        public async Task ShouldReturnTruckFMIsUpdatedWithSucess()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);


            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckUpdateDto = new TruckUpdateDto { Id = 1, AnoModelo = DateTime.Now.Year, Cor = "blue", DigitalPanel = false, Modelo = "FM" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.UpdateTruck(truckUpdateDto);

            //Assert
            result.Should().NotBeNull();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnTruckFHIsUpdatedWithSucess()
        {
            //Arrange
            var truck = new TruckFH("black", (int)ModelTruckEnum.FH, DateTime.Now.Year, false);


            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckUpdateDto = new TruckUpdateDto { Id = 10, AnoModelo = DateTime.Now.Year + 1, Cor = "black", DigitalPanel = null, Modelo = "FH" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.UpdateTruck(truckUpdateDto);

            //Assert
            result.Should().NotBeNull();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnNullIfWrongDate()
        {
            //Arrange
            var truckDto = new TruckUpdateDto { Id = 10, AnoModelo = DateTime.Now.Year + 1, Cor = "black", DigitalPanel = null, Modelo = "FH" };

            //Act
            var result = await _truckApplication.UpdateTruck(truckDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == $"Truck with Id {truckDto.Id} not found!")));
        }

        [Fact]
        public async Task ShouldReturnNullIfNotFoundTruckById()
        {
            //Arrange
            var truckDto = new TruckUpdateDto { Id = 10, AnoModelo = DateTime.Now.Year + 5, Cor = "black", DigitalPanel = null, Modelo = "FH" };
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.UpdateTruck(truckDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == $"Invalid truck model year!")));
        }

        [Fact]
        public async Task ShouldReturnNullIfTryUpdatedTruckFHWithAnotherModel()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckUpdateDto = new TruckUpdateDto { Id = 1, AnoModelo = DateTime.Now.Year, Cor = "blue", DigitalPanel = false, Modelo = "FH" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.UpdateTruck(truckUpdateDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == $"It is not possible to change the model of the truck, the current model is {ModelTruckEnum.FM}")));
        }

        [Fact]
        public async Task ShouldReturnNullIfTryUpdatedTruckFMWithAnotherModel()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FH, DateTime.Now.Year);
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truckUpdateDto = new TruckUpdateDto { Id = 1, AnoModelo = DateTime.Now.Year, Cor = "blue", DigitalPanel = false, Modelo = "FM" };
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.UpdateTruck(truckUpdateDto);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == $"It is not possible to change the model of the truck, the current model is {ModelTruckEnum.FH}")));
        }

        [Fact]
        public async Task ShouldReturnTruckIfGetById()
        {
            //Arrange
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.GetTruckById(1);

            //Assert
            result.Should().NotBeNull();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnNullIfGetByIdNotFound()
        {
            //Arrange

            //Act
            var result = await _truckApplication.GetTruckById(1);

            //Assert
            result.Should().BeNull();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Truck with Id: 1 not found")));
        }

        [Fact]
        public async Task ShouldReturnAllTruck()
        {
            //Arrange
            var truckFM = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            var truckFH = new TruckFH("black", (int)ModelTruckEnum.FH, DateTime.Now.Year, true);
            var lstTruck = new List<Domain.Models.Truck> { truckFM, truckFH };
            _truckRepository.Setup(x => x.ListAllTrucks()).ReturnsAsync(lstTruck);

            //Act
            var resultList = await _truckApplication.ListAllTrucks();

            //Assert
            resultList.Should().NotBeNull();
            resultList.Count().Should().Be(2);
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnTrueIfRemoveTruckWithSucess()
        {
            //Arrange
            _unitOfWork.Setup(x => x.Commit()).Returns(true);
            var truck = new TruckFM("blue", (int)ModelTruckEnum.FM, DateTime.Now.Year);
            _truckRepository.Setup(x => x.GetTruckById(It.IsAny<int>())).ReturnsAsync(truck);

            //Act
            var result = await _truckApplication.RemoveTruck(1);

            //Assert
            result.Should().BeTrue();
            _mediator.Invocations.Count().Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnFalseIfTruckNotFoundToRemove()
        {
            //Arrange

            //Act
            var result = await _truckApplication.RemoveTruck(1);

            //Assert
            result.Should().BeFalse();
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "The truck id 1 not found!")));
        }
    }
}
