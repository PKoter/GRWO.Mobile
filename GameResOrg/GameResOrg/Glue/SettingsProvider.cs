using GameResOrg.Local.Contracts;

namespace GameResOrg.Glue
{
	public sealed class Settings : ISettingsProvider
	{
		private string _path;
		private string Path => _path ?? (_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));

		public string LocalDbPath => System.IO.Path.Combine(Path, "context.db");
		public string ImageCachePath => System.IO.Path.Combine(Path, "avtcache");
		public string SBlobPath => System.IO.Path.Combine(Path, "sblob");
		public string CrtBlobPath => System.IO.Path.Combine(Path, "crtblob");
		public string ConfigPath => System.IO.Path.Combine(Path, "config.json");
	}
}
