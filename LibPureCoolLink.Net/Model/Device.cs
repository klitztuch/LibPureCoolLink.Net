namespace LibPureCoolLink.Net.Model
{
    public class Device : IDevice
    {
        #region Properties

        public string Serial { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string LocalCredentials { get; set; }
        public bool AutoUpdate { get; set; }
        public bool NewVersionAvailable { get; set; }
        public string ProductType { get; set; }
        public string ConnectionType { get; set; }

        #endregion

        #region Ctor

        public Device(string serial,
            string name,
            string version,
            string localCredentials,
            bool autoUpdate,
            bool newVersionAvailable,
            string productType,
            string connectionType)
        {
            Serial = serial;
            Name = name;
            Version = version;
            LocalCredentials = localCredentials;
            AutoUpdate = autoUpdate;
            NewVersionAvailable = newVersionAvailable;
            ProductType = productType;
            ConnectionType = connectionType;
        }

        public Device()
        {
        }

        #endregion
    }
}