using FMS.API.Model;

namespace FMS.API.Data
{
    public static class ProductStore
    {
        public static List<ProductDto> GetAllProducts=new List<ProductDto>
        {            
                new ProductDto {Id=1,Name="Papaya",Code="001",Barcode="P00150"},
                new ProductDto {Id=2,Name="Chilli",Code="002",Barcode="P00250"}
            };
        }
    }

