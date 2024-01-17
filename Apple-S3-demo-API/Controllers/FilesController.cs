using Amazon.S3;
using Amazon.S3.Model;
using Apple_S3_demo_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apple_S3_demo_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IAmazonS3 _amazonS3;
        public FilesController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

		// Uploading Files in S3 bucket in AWS

		[HttpPost("upload")]
		public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
		{
			var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
			if (bucketExists)
			{
				return BadRequest($"Bucket {bucketName} already exists");
			}
			if (!bucketExists)
			{
				return NotFound($"Bucket {bucketName} does not exists.");
			}
			var request = new PutObjectRequest()
			{
				BucketName = bucketName,
				Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
				InputStream = file.OpenReadStream()
			};
			request.Metadata.Add("Content-Type", file.ContentType);
			await _amazonS3.PutObjectAsync(request);
			return Ok($"File {prefix} / {file.FileName} uploaded to S3 bucket in AWS Successfully !");
		}


		[HttpGet("get-all")]
		public async Task<IActionResult> GetAllFilesAsync(string bucketName, string?prefix)
		{
			var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
			if (!bucketExists)
			{
				return NotFound($"Bucket {bucketName} does not exists.");
			}
			var request = new ListObjectsV2Request()
			{
				BucketName = bucketName,
				Prefix = prefix
			};

			var result = await _amazonS3.ListObjectsV2Async(request); // return the lists of Objects in RAW format
			var s3Objects = result.S3Objects.Select(s =>
			{
				var urlRequest = new GetPreSignedUrlRequest()
				{
					BucketName = bucketName,
					Key = s.Key,
					Expires = DateTime.UtcNow.AddMinutes(1) // PreSignedUrl helps to expose the link and we can configure when it will be expiring
				};

				return new S3ObjectDto()
				{
					Name = s.Key.ToString(),
					PresignedUrl = _amazonS3.GetPreSignedURL(urlRequest),
				};

			});
			return Ok(s3Objects);
		}

    }
}
