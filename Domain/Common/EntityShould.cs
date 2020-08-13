using Xunit;

namespace Domain.Common
{
    public class EntityShould
    {
        private readonly Entity sut;
        private const int ID = 1;
        private const string TENANTID = "TENANT123";
        public EntityShould()
        {
            sut = new Entity();
        }

        [Fact]
        public void SetAndGetID()
        {
            sut.ID = ID;
            Assert.Equal(ID, sut.ID);
        }

        [Fact]
        public void SetAndGetTenantID()
        {
            sut.TenantID = TENANTID;
            Assert.Equal(TENANTID, sut.TenantID);
        }
    }
}
