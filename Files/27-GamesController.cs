#region Copyright
//-----------------------------------------------------------------------------
// <copyright file="GamesController.cs" company="ATSI S.A.">
//     Copyright (c) ATSI S.A. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
#endregion

namespace Test.FlashWebsiteWrapper.Controllers
{
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    /// <summary>
    /// The games swf controller.
    /// </summary>
    public class GamesController : Controller
    {
        /// <summary>
        /// The run game.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <param name="gameId">
        /// The game id.
        /// </param>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <param name="userIdentity">
        /// The user identity.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult RunGame(string serverName, int gameId, string mode, string userIdentity = null, string currencyCode = "PLN")
        {
            var url = string.Format("http://{0}/Games/RunGame?gameId={1}&mode={2}&userIdentity={3}", serverName, gameId, mode, userIdentity);

            var request = WebRequest.Create(url);
            var requestStream = request.GetResponse().GetResponseStream();
            this.ViewBag.ServerName = serverName;

            if (requestStream != null)
            {
                var htmlContent = new StreamReader(requestStream).ReadToEnd();
                var gameInstanceId = Regex.Match(htmlContent, @"gameInstanceID"", ""(.+?)""").Groups[1].Value;
                
                this.ViewBag.GameInstanceId = gameInstanceId;
                return this.View();
            }

            this.ViewBag.GameInstanceId = string.Empty;

            return this.View();
        }
    }
}
