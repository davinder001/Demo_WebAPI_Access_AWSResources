namespace Demo_WebAPI_Access_AWSResources.Services
{
    public class S3ObjectNew
    {
        public string FileName { get; set; }
        public MemoryStream Stream { get; set; }
        public string BucketName { get; set; }
    }
}