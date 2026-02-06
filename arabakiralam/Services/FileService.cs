using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace arabakiralam.Services
{
    public class FileService
    {
       

        public string UploadingFile(IFormFile ImageFile,string foldername="Images")
        {
            long maxFilesize = 5 * 1024 * 1024;
            string[] AllowTypesExtension = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if (ImageFile != null && ImageFile.Length > 0)
            {
                if (ImageFile.Length > maxFilesize)
                {
                    return "Belirtilen dosya 5Mb'dan Büyük Olmaz";

                }
                if (!AllowTypesExtension.Contains(Path.GetExtension(ImageFile.FileName).ToLower()))
                {
                    return "Dosya Tipi Jpg png gif jpeg olmalıdır";

                }
                var newName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName).ToLower();
                
                var filepath = Path.Combine("wwwroot/"+foldername+"/", newName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
                return "/"+foldername+"/"+newName;
            }
            return "Dosya Seçilmedi";
        }
    }
}
