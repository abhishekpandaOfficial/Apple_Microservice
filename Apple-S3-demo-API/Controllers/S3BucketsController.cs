using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Apple_S3_demo_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class S3BucketsController : ControllerBase
	{

		private readonly IAmazonS3 _s3Client;
        public S3BucketsController(IAmazonS3 s3Client)
        {
			_s3Client = s3Client;
        }


		[HttpPost]
		public async Task<IActionResult> CreateS3BucketAsync(string bucketName)
		{
			var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
			if (bucketExists)
			{
				return BadRequest($"Bucket {bucketName} already exists");
			}
			await _s3Client.PutBucketAsync(bucketName);
			return Ok($"Bukcet {bucketName} is created");
		}

		[HttpGet]
		public async Task<IActionResult> GetAllS3BucketAsync()
		{
			var data = await _s3Client.ListBucketsAsync();
			var buckets = data.Buckets.Select(b =>
			{
				return b.BucketName;
			});
			return Ok(buckets);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteS3BucketAsync(string bucketName)
		{
			await _s3Client.DeleteBucketAsync(bucketName);
			return NoContent();
		}

    }
}
