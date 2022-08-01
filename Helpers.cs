namespace Helpers;

public class Helpers
{
    public static async Task<string> UploadFile(IFormFile file, IWebHostEnvironment _env, string directory = "Posts")
    {
        var basePath = Path.Combine(_env.WebRootPath + $"/images/{directory}");
        bool basePathExists = System.IO.Directory.Exists(basePath);
        if (!basePathExists) Directory.CreateDirectory(basePath);
        var filePath = Path.Combine(basePath, file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);

        if (System.IO.File.Exists($"{_env.WebRootPath}/images/{directory}/{file.FileName}"))
        {


            System.IO.File.Delete($"{_env.WebRootPath}/images/{directory}/{file.FileName}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"images/{directory}/{file.FileName}";
        }
        else
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"images/{directory}/{file.FileName}";
        }
    }
}