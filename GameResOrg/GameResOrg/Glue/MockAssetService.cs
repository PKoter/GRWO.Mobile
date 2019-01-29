using System;
using System.Collections.Generic;
using GameResOrg.Data.Project;
using GameResOrg.Logic.Objects;
using GameResOrg.Logic.Objects.Models;

namespace GameResOrg.Glue
{
	public sealed class MockAssetService : IAssetService
    {
		public void AddAsset(AssetDataModel creationContext)
		{
			throw new NotImplementedException();
		}

		public AssetDataModel GetFileAssetInfo(int id)
		{
			throw new NotImplementedException();
		}

		public Asset GetAsset(int id, bool cacheResult = true)
		{
			throw new NotImplementedException();
		}

		public Asset[] GetAssets(List<int> assetIds, bool requireLatest, bool cacheResult = true)
		{
			throw new NotImplementedException();
		}

		public void UpdateAsset(AssetDataModel context)
		{
			throw new NotImplementedException();
		}
	}
}
