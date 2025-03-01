using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Demo_WebAPI_Access_AWSResources.Services
{
    public class AWSS3Service
    {     
        private static AmazonS3Client _client;

        public AWSS3Service(BasicAWSCredentials credentials, AmazonS3Config config)
        {
            _client = new AmazonS3Client(credentials, config);
        }

        public async Task<string> CreateBucketAsync(string bucketName)
        {
            try
            {
                await _client.PutBucketAsync(new PutBucketRequest {
                        BucketName = bucketName,
                        UseClientRegion = true                
                }); 

                return "Bucket created successfully.";
            }
            catch(AmazonS3Exception ex)
            {
                if(ex.ErrorCode == "BucketAlreadyExists" || ex.ErrorCode == "BucketAlreadyOwnedByYou")
                    return "Bucket already exists.";
                
                return $"Error occurred: {ex.Message}";
            }
            catch(Exception ex)
            {
                return $"Error occurred: {ex.Message}";
            }            
        }
        
        public async Task<ListBucketsResponse> ListBucketsAsync()
        {
            return await _client.ListBucketsAsync();
        }

        public async Task UploadToS3Async(S3ObjectNew s3Object)
        {
            var transferUtilityUploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3Object.Stream,
                Key = s3Object.FileName,
                BucketName = s3Object.BucketName
            };

            var transferUtility = new TransferUtility(_client);

            await transferUtility.UploadAsync(transferUtilityUploadRequest);           
        }

        public async Task DownloadFromS3Async(string bucketName, string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            
            var transferUtilityRequest = new TransferUtilityDownloadRequest()
            {
                BucketName = bucketName,
                FilePath = $"{currentDirectory}/{fileName}",
                Key = fileName
            };

            var transferUtility = new TransferUtility(_client);

            await transferUtility.DownloadAsync(transferUtilityRequest);
        }
    }
}