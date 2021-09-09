namespace TimeManagementApp.App.ViewModels
{
    public class ConnectionViewModel
    {
        public MongoConnectionViewModel MongoConnectionViewModel { get; set; } = new MongoConnectionViewModel();
        public SqlServerConnectionViewModel SqlServerConnectionViewModel { get; set; } =  new SqlServerConnectionViewModel();
        public string DbType { get; set; }
        public string DbName { get; set; }
    }
}
