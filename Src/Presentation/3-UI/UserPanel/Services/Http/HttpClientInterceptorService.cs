﻿using System.Net.Http.Headers;
using TaskoMask.Presentation.UI.UserPanel.Helpers;

namespace TaskoMask.Presentation.UI.UserPanel.Services.Http
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpClientInterceptorService : DelegatingHandler
    {

        public HttpClientInterceptorService( )
        {
        }


        /// <summary>
        /// 
        /// </summary>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // How to add a JWT to all of the requests
            //var jwtToken =  _cookieService.Get(MagicKey.Jwt_Token);
            //if (jwtToken is not null)
            //    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", jwtToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
