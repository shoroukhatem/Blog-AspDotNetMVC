namespace Blog.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files",folderName);
            //2. Get File Name && Make it Unique
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            //3. Get File Path
            var filePath = Path.Combine(folderPath,fileName);
            //4.
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        } 
        public static void DeleteFile(string FileName)
        {
           
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", "Images");
            var filePath = Path.Combine(folderPath, FileName);
            File.Delete(filePath);
            // using var fileStream = new FileStream(filePath, FileMode.Truncate);
        }

    }
}
