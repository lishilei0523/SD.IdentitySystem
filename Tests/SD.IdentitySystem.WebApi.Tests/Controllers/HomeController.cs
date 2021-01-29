using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.StubWCF.Server.Interfaces;
using SD.IdentitySystem.WebApi.Tests.Models;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using SD.IOC.Core.Mediators;
using SD.Toolkits.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace SD.IdentitySystem.WebApi.Tests.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public string GetProducts(string keywords, int pageIndex, int pageSize)
        {
            Trace.WriteLine(keywords);
            Trace.WriteLine(pageIndex);
            Trace.WriteLine(pageSize);

            //伪造一个登录信息
            IAuthenticationContract authenticationContract = ResolveMediator.Resolve<IAuthenticationContract>();
            LoginInfo fakeLoginInfo = authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            //实例化服务接口
            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            //调用服务获取消息头
            string header = serverContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakeLoginInfo.PublicKey.ToString() == header)
            {
                Console.WriteLine("认证通过！");
            }

            return "Hello World";
        }

        [HttpPost]
        [WrapPostParameters]
        public void CreateProduct(string productNo, string productName, IEnumerable<PriceParam> priceParams)
        {
            Trace.WriteLine(productNo);
            Trace.WriteLine(productName);
            Trace.WriteLine(priceParams);

            //伪造一个登录信息
            IAuthenticationContract authenticationContract = ResolveMediator.Resolve<IAuthenticationContract>();
            LoginInfo fakeLoginInfo = authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            //实例化服务接口
            IServerContract serverContract = ResolveMediator.Resolve<IServerContract>();

            //调用服务获取消息头
            string header = serverContract.GetHeader();

            //如果消息头内容即是上述伪造的公钥，即说明整个认证过程没问题
            if (fakeLoginInfo.PublicKey.ToString() == header)
            {
                Console.WriteLine("认证通过！");
            }
        }
    }
}