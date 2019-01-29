using System;
using System.Collections.Generic;
using GameResOrg.Data.Project;
using GameResOrg.Logic.Infrastructure;
using GameResOrg.Models;
using Object = GameResOrg.Data.Project.Object;

namespace GameResOrg.Glue
{
	public sealed class MockRelatedDataService : IRelatedDataService
    {
		public Asset[] GetNewRelatedAssets()
		{
			throw new NotImplementedException();
		}

		public List<Object> GetNewGroupObjects()
		{
			throw new NotImplementedException();
		}

		public void MarkRead(Asset asset)
		{
			throw new NotImplementedException();
		}

		public void MarkRead(PrivateConversation thread)
		{
			throw new NotImplementedException();
		}

		public void MarkRead(SlimUser newUser)
		{
			
		}

		public RelatedIns CurrentIns { get; }
	}
}
