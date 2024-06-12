using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbECommerce.Services
{
    public class GCSConfigOptions
    {
        public string GCPStorageAuthFile { get; set; }
        public string GoogleCloudStorageBucketName { get; set; }
    }
}