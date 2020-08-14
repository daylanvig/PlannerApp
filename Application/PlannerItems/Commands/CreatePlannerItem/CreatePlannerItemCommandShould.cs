using Application.Interfaces.Persistence;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using AutoFixture;
using AutoMapper;
using AutoMoq;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using Xunit;

namespace Application.PlannerItems.Commands.CreatePlannerItem
{
    public class CreatePlannerItemCommandShould
    {
        private readonly CreatePlannerItemCommand sut;
        private readonly AutoMoqer mocker;
        private readonly Fixture fixture;
        private readonly PlannerItem item;
        private readonly PlannerItemModel itemModel;
        
        public CreatePlannerItemCommandShould()
        {
            fixture = TestFixture.Create();
            fixture.Customize<PlannerItem>(c => c.Without(p => p.Category));
            item = fixture.Create<PlannerItem>();
            itemModel = fixture.Create<PlannerItemModel>();
            mocker = new AutoMoqer();
            mocker.GetMock<IMapper>()
                .Setup(m => m.Map<PlannerItemCreateEditModel, PlannerItem>(It.IsAny<PlannerItemCreateEditModel>()))
                .Returns(item);
            mocker.GetMock<IMapper>()
                .Setup(m => m.Map<PlannerItem, PlannerItemModel>(It.IsAny<PlannerItem>()))
                .Returns(itemModel);
            sut = mocker.Create<CreatePlannerItemCommand>();
        }

        [Fact]
        public void CallRepositoryAddMethod()
        {
            sut.Execute(fixture.Create<PlannerItemCreateEditModel>()).GetAwaiter().GetResult();
            mocker.GetMock<IPlannerItemRepository>()
                .Verify(m => m.AddAsync(It.IsAny<PlannerItem>()), Times.Once);
        }
    }
}
