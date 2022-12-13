namespace GestorTareas.Client.Services.Utils;

public static class ImageValidator
{
    public static bool IsImage(this byte[] fileBytes)
    {
        if(fileBytes.Length < 2)
        {
            return false;
        }

        var headers = new List<byte[]>
        {
            new byte[] {0xFF, 0xD8, 0xFF}, //JPG
            new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, //PNG
            //new byte[] {}, //SVG
            new byte[] { 0x52, 0x49, 0x46, 0x46 } //WEBP
        };

        return headers.Any(x => x.SequenceEqual(fileBytes.Take(x.Length)));
    }
}