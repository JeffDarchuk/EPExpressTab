using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPExpressTab.Data;
using Sitecore.Analytics.Tracking;

namespace EPExpressTab
{
	public class EpExpressViewDemo : EpExpressViewModel
	{
		public override string Heading => "Look Ma!  MVC!";
		public override string TabLabel => "Special MVC Tab";
		public override bool UseDefaultWrapper => false;

		public override object GetModel(Contact contact)
		{
			return new EpExpressDemoModel
			{
				ContactId = contact.ContactId.ToString(),
				VisitCount = (int)((dynamic)contact).VisitCount
			};
		}
		public override string GetFullViewPath(object model)
		{
			return "/views/EpExpressDemo.cshtml";
		}
	}
}
