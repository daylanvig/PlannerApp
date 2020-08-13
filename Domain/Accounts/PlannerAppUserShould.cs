using Xunit;

namespace Domain.Accounts
{
    public class PlannerAppUserShould
    {
        private readonly PlannerAppUser sut;
        private const string TENANTID = "Tenant123";
        public PlannerAppUserShould()
        {
            sut = new PlannerAppUser();
        }

        [Fact]
        public void SetAndGetTenantID()
        {
            sut.TenantID = TENANTID;
            Assert.Equal(TENANTID, sut.TenantID);
        }
    }
}
