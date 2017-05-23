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
	public class viewtest : EpExpressViewModel
	{
		public override string Heading => "Stuffs and things";
		public override string TabLabel => "my super tab";
		public override object GetModel(Contact contact)
		{
			return contact;
		}

		public override string GetFullViewPath(object model)
		{
			return "/views/EpExpressDemo.cshtml";
		}
	}
}
