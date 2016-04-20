﻿using System.Xml;
using IceWarpObjects.Helpers;
using IceWarpObjects.Rpc.Classes;
using IceWarpRpc.Responses;
using IceWarpRpc.Utilities;

namespace IceWarpRpc.Requests.Account
{
    /// <summary>
    /// Gets the list of account api variables, its values, data types and rights, its values, data types and rights. See <see cref="IceWarpCommand{TPropertyInfoList}"/> for return type.
    /// </summary>
    public class GetAccountAPIConsole : IceWarpCommand<TPropertyInfoListResponse>
    {
        /// <summary>
        /// Email address of IceWarp account existing on server
        /// </summary>
        public string AccountEmail { get; set; }
        /// <summary>
        /// Filter for account api variables. See <see cref="TPropertyListFilter"/> for more information.
        /// </summary>
        public TPropertyListFilter Filter { get; set; }
        /// <summary>
        /// Specifies offset start of returned items.
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Specifies count of returned items.
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Specifies whether return comments or not
        /// </summary>
        public bool Comments { get; set; }

        protected override void BuildCommandParams(XmlDocument doc, XmlElement command)
        {
            var commandParams = XmlHelper.CreateElement(doc, "commandparams");

            XmlHelper.AppendTextElement(commandParams, "accountemail", AccountEmail);

            if (Filter != null)
            {
                commandParams.AppendChild(Filter.BuildXmlElement(doc, "filter"));
            }

            XmlHelper.AppendTextElement(commandParams, "offset", Offset.ToString());
            XmlHelper.AppendTextElement(commandParams, "count", Count.ToString());
            XmlHelper.AppendTextElement(commandParams, "comments", Comments.ToBitString());

            command.AppendChild(commandParams);
        }

        public override TPropertyInfoListResponse FromHttpRequestResult(HttpRequestResult httpRequestResult)
        {
            return new TPropertyInfoListResponse(httpRequestResult);
        }
    }
}