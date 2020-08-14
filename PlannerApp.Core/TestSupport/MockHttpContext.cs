using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Shared.TestSupport
{
    public class MockHttpContext : HttpContext
    {
        public static MockHttpContext CreateAuthenticatedHttpContext()
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("UserId", "123", ClaimValueTypes.Integer32)
            }, "Custom");
            var context = new MockHttpContext
            {
                User = new ClaimsPrincipal(identity)
            };
            return context;
        }
        public override IFeatureCollection Features => throw new NotImplementedException();

        public override HttpRequest Request => throw new NotImplementedException();

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override AuthenticationManager Authentication => throw new NotImplementedException();

        public override ClaimsPrincipal User { get; set; } = new ClaimsPrincipal();
        public override IDictionary<object, object> Items { get; set; } = new Dictionary<object, object>();
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
