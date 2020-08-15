using Application.Interfaces;
using Application.PlannerItems.Commands.CreatePlannerItem;
using Application.PlannerItems.Commands.EditPlannerItem;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Application.PlannerItems.Queries.GetCompletedItems;
using Application.PlannerItems.Queries.GetOverdueItems;
using Application.PlannerItems.Queries.GetPlannerItemsByDate;
using AutoFixture;
using AutoMapper;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationServer.PlannerItems
{
    public class PlannerItemsControllerTestFixture
    {
        public readonly Fixture fixture;

        public readonly PlannerItemModel itemModel;
        public readonly PlannerItemCreateEditModel createEditModel;

        public Mock<IRepository<PlannerItem>> plannerItemRepositoryMock;
        public Mock<IGetPlannerItemsByDateQuery> getPlannerItemsByDateQueryMock;
        public Mock<IGetCompletedItemsQuery> getCompletedItemsQueryMock;
        public Mock<IGetOverdueItemsQuery> getOverdueItemsQueryMock;

        public Mock<ICreatePlannerItemCommand> createPlannerItemCommandMock;
        public Mock<IEditPlannerItemCommand> editPlannerItemCommandMock;

        public PlannerItemsControllerTestFixture()
        {
            fixture = TestFixture.Create();
            itemModel = fixture.Create<PlannerItemModel>();
            createEditModel = fixture.Create<PlannerItemCreateEditModel>();

            var items = new List<PlannerItemModel> { itemModel };

            getPlannerItemsByDateQueryMock = fixture.Freeze<Mock<IGetPlannerItemsByDateQuery>>();
            getPlannerItemsByDateQueryMock
                .Setup(m => m.Execute(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(items);

            getCompletedItemsQueryMock = fixture.Freeze<Mock<IGetCompletedItemsQuery>>();
            getCompletedItemsQueryMock
                .Setup(m => m.Execute())
                .ReturnsAsync(items);

            getOverdueItemsQueryMock = fixture.Freeze<Mock<IGetOverdueItemsQuery>>();
            getOverdueItemsQueryMock
                .Setup(m => m.Execute())
                .ReturnsAsync(items);

            plannerItemRepositoryMock = fixture.Freeze<Mock<IRepository<PlannerItem>>>();

            fixture.Freeze<Mock<IMapper>>()
                .Setup(m => m.Map<PlannerItemCreateEditModel>(It.IsAny<PlannerItem>()))
                .Returns(createEditModel);

            createPlannerItemCommandMock = fixture.Freeze<Mock<ICreatePlannerItemCommand>>();
            createPlannerItemCommandMock.Setup(m => m.Execute(createEditModel))
                .ReturnsAsync(itemModel);

            editPlannerItemCommandMock = fixture.Freeze<Mock<IEditPlannerItemCommand>>();
        }

        public PlannerItemsController CreateSUT()
        {
            return fixture.Build<PlannerItemsController>().OmitAutoProperties().Create();
        }
    }
}
