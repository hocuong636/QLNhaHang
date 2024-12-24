using System;
using System.Collections.ObjectModel;

namespace QLNhaHang.ViewModels
{
    public class FoodViewModel
    {
        // Thuộc tính mô tả món ăn
        public string Ma { get; set; }
        public string Ten { get; set; }
        public decimal Gia { get; set; }
        public int ThoiGianNau { get; set; }

        // Danh sách các món ăn
        public ObservableCollection<FoodViewModel> Foods { get; set; }

        public FoodViewModel()
        {
            // Dữ liệu mẫu
            Foods = new ObservableCollection<FoodViewModel>
            {
                new FoodViewModel { Ma = "001", Ten = "Phở", Gia = 50000, ThoiGianNau = 15 },
                new FoodViewModel { Ma = "002", Ten = "Bánh mì", Gia = 15000, ThoiGianNau = 5 },
                new FoodViewModel { Ma = "003", Ten = "Bánh xèo", Gia = 25000, ThoiGianNau = 10 }
            };
        }
    }
}
