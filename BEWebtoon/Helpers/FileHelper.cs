using BEWebtoon.Helpers;
namespace IOC.ApplicationLayer.Utilities
{
    public static class FileHelper
    {
        /// <summary>
        /// Save file to server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<string> SaveFile(IFormFile file, string folderName, string? customFileName = null)
        {
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);
                var fileName = customFileName != null ? customFileName + "_" + file.FileName : file.FileName;
                var uniqueFilePath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(uniqueFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var filePath = Path.Combine(pathToSave, fileName);
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new CustomException("Khong tim thay file");
                }
                return Path.Combine(folderName, fileName);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
