using System;

namespace NuevoFoundationApiVersion2
{
    public static class AzureStorageConfig
    {
        public static string AccountName { get; set; } = Environment.GetEnvironmentVariable("AccountName", EnvironmentVariableTarget.Process);
        public static string AccountKey { get; set; } = Environment.GetEnvironmentVariable("AccountKey", EnvironmentVariableTarget.Process);
        public static string ContainerName { get; set; } = Environment.GetEnvironmentVariable("ContainerName", EnvironmentVariableTarget.Process);
    }
}
