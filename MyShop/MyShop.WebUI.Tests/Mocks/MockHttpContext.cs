using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockHttpContext : HttpContextBase
    {
        private MockRequest request;
        private MockResponse response;
        private HttpCookieCollection cookies;
        private IPrincipal fakeUser;
        public MockHttpContext() 
        { 
            this.cookies = new HttpCookieCollection();
            this.request = new MockRequest(cookies);
            this.response = new MockResponse(cookies);
        }
        public override IPrincipal User { get { return fakeUser; } set { this.fakeUser = value; } }

        public override HttpRequestBase Request { get { return request; } }
        public override HttpResponseBase Response { get { return response; } }

        public class MockResponse : HttpResponseBase
        {
            private readonly HttpCookieCollection cookies;
            public MockResponse(HttpCookieCollection cookies)
            {
                this.cookies = cookies;
            }

            public override HttpCookieCollection Cookies
            {
                get
                { 
                    return cookies;
                }
            } 
        }

        public class MockRequest : HttpRequestBase
        {
            private readonly HttpCookieCollection cookies;
            public MockRequest(HttpCookieCollection cookies)
            {
                this.cookies = cookies;
            }
            public override HttpCookieCollection Cookies
            {
                get { return cookies; }
            }
        } 

    }
}
