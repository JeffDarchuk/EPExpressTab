using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Tracking;
using Sitecore.Configuration;
using Sitecore.Data;

namespace EPExpressTab.Handlers
{
	public class EpExpressTabRenderer
	{
		public static string Render()
		{
			string contactId = System.Web.HttpContext.Current.Request.QueryString["cid"];
			string tabIdentifier = Sitecore.Context.Item["Placeholder Name"].Substring(3);
			ContactRepository contactRepository = Factory.CreateObject("tracking/contactRepository", true) as ContactRepository;
			Contact contact = contactRepository.LoadContactReadOnly(new ID(contactId));
			var tab = EpContext.Tabs[tabIdentifier];
			if (tab.UseDefaultWrapper)
				return string.Format(Constants.DefaultWrapperPrefix, tab.Heading) + tab.RenderToString(contact) + Constants.DefaultWrapperSuffix;
			return tab.RenderToString(contact);
		}
	}
}
