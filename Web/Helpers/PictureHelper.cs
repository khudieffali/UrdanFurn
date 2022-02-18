using Entities;

namespace Web.Helpers
{
    public static class PictureHelper
    {
        public static string getCoverPhoto(int? coverId,List<ProductPicture> ProductPictures)
        {
            foreach (var item in  ProductPictures)
            {
                if (item.PictureId == coverId)
                {
                    return item.Picture.Url;
                }
            }   
                return "";

        }
    }
}
