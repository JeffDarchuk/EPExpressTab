﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPExpressTab.Data;
using Sitecore.Analytics.Tracking;

namespace EPExpressTab
{
	public class EpExpressDemo : EpExpressModel
	{
		public override string TabLabel => "Awesome Tab";
		public override string Heading => "This is the start of something";
		public override string RenderToString(Contact model)
		{
			return $"<h1>AWESOME!!</h1><p>{model.ContactId}</p>";
		}
	}
}
