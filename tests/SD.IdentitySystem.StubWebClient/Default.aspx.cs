using System;
using System.Web.UI;
using SD.IdentitySystem.StubServer.Interfaces;
using SD.IOC.Core.Mediator;
using ShSoft.ValueObjects;

namespace SD.IdentitySystem.StubWebClient
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid newGuid = Guid.NewGuid();

            this.Session.Add(SessionKey.CurrentPublishKey, newGuid);

            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            string header = serverContract.GetHeader();

            this.Response.Write(header);

            ResolveMediator.Dispose();
        }
    }
}