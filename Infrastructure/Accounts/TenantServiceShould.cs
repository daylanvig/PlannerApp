using AutoFixture;
using Domain.Accounts;
using Microsoft.AspNetCore.Http;
using Moq;
using Shared.TestSupport;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Accounts
{
    public class TenantServiceShould
    {
        private readonly TenantService sut;
        private readonly MockHttpContext mockHttpContext;
        private readonly PlannerAppUser testUser;
        private readonly Mock<IUserManagerWrapper> userManagerWrapperMock;
        public TenantServiceShould()
        {
            var fixture = TestFixture.Create();
            testUser = fixture.Create<PlannerAppUser>();
            mockHttpContext = MockHttpContext.CreateAuthenticatedHttpContext();
            var httpContextMock = new Mock<IHttpContextAccessor>();
            httpContextMock.Setup(m => m.HttpContext)
                .Returns(mockHttpContext);
            userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            userManagerWrapperMock.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            sut = new TenantService(httpContextMock.Object, userManagerWrapperMock.Object);
        }

        [Fact]
        public void ReturnAnEmptyString()
        {
            mockHttpContext.User = new ClaimsPrincipal();
            Assert.Equal(string.Empty, sut.GetTenantID());
        }

        [Fact]
        public void LoadFromUserManager()
        {
            sut.GetTenantID();
            userManagerWrapperMock.Verify(m => m.FindByNameAsync(It.IsAny<string>()), Times.Once);
            userManagerWrapperMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ReturnPlannerAppUsersTenantID()
        {
            Assert.Equal(testUser.TenantID, sut.GetTenantID());
        }
    }
}
