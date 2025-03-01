using Demo_WebAPI_Access_AWSResources.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_Access_AWSResources.Controller
{
    [Route("api/aws/s3")]
    [ApiController]
    public class AWSS3DemoController: ControllerBase
    {
        private readonly AWSS3Service _aWSS3Service;
        public AWSS3DemoController(AWSS3Service aWSS3Service)
        {
            _aWSS3Service = aWSS3Service;
        }

        [HttpPost("UploadFile", Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, string bucketName)
        {
            try
            {
                await using var stream = new MemoryStream();
                await file.CopyToAsync(stream);

                var s3Object = new S3ObjectNew()
                {
                    BucketName = bucketName,
                    Stream = stream,
                    FileName = file.FileName
                };                

                await _aWSS3Service.UploadToS3Async(s3Object);

                return Ok();
            }
            catch(Exception ex)
            {
                // Logger
                return StatusCode(500);
            }
        }

        [HttpPost("DownloadFile", Name = "DownloadFile")]
        public async Task<IActionResult> DownloadFile(string bucketName, string fileName)
        {
            try
            {
                await _aWSS3Service.DownloadFromS3Async(bucketName, fileName);

                return Ok();
            }
            catch(Exception ex)
            {
                // Logger
                return StatusCode(500);
            }
        }

        [HttpPost("CreateBucket", Name = "CreateBucket")]
        public async Task<IActionResult> CreateBucket(string bucketName)
        {
            try
            {
                return Ok(await _aWSS3Service.CreateBucketAsync(bucketName));                
            }
            catch(Exception ex)
            {
                //Logger
                return StatusCode(500);
            }
        }

        [HttpGet("ListBuckets", Name = "ListBuckets")]
        public async Task<IActionResult> ListBuckets()
        {
            try
            {
                return Ok(await _aWSS3Service.ListBucketsAsync());                
            }
            catch(Exception ex)
            {
                //Logger
                return StatusCode(500);
            }
        }
    }
}