   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       	QLNhaHang   QLNhaHang.DashboardPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   $
��66.6��6*   �� 5      �� 5      $��2* �6   .+�5   
   �� $%��KẾT QUẢ BÁN HÀNG HÔM NAY��6   $��Center=�5      $��20(�61   $
��Bold"�5      $��	0,20,0,20q�6&   $	��298��69   �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��196,-103,0,20q�6I   $��1��6`   $	��960��6m   .+�5      #  MaterialDesignThemes.Wpf.Card  $	��300��6"   $	��150��6.   $��10q�6;   ���	#FFFDE7E7.��5      � $	��150��6   $��Bottom=�6$    � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      $��* �6+   .+�5      � $��50��6   $��50��6&   $��10q�62   .+�5       A� ���#F443366"   �� 5!      $��$��6$   
�������$��24(�6@   $
��Bold"�6N   $��Center=�5"   $   $��Center=�6A   5#      �� 5$      $��1��6!   $��Center=�61   $��10,0,0,0q�6L   .+�5%      �� $��0 đơn đã xong��6$   $��16(�69   
�������6G   �� 5&      $��0��6$   $��30(�6-   $
��Bold"�6;   ���#F443366M   �� 5'      $��↓ 100%��6$   $��12(�62   
��� � �6@   5)      5*      5+        5.      $	��300��6"   $	��150��6.   $��10q�6;   ���#E8F5E9.��5/      �  50      �� 51      $
��Auto �6+   �� 52      $��* �6+   .+�54      � $��50��6   $��50��6&   $��10q�62   .+�55      A� ���#4CAF506"   �� 56      $	��✎��6$   
�������$��24(�6@   $
��Bold"�6N   $��Center=�57   $   $��Center=�6A   58      �� 59      $��1��6!   $��Center=�61   $��10,0,0,0q�6L   .+�5:      �� $��0 đơn đang phục vụ��6$   $��16(�6>   
�������6L   �� 5;      $��0��6$   $��30(�6-   $
��Bold"�6;   ���#4CAF506M   5<      5=      5>        5A      $	��300��6"   $	��150��6.   $��10q�6;   ���	#FFFDE7E7.��5B      �  5C      �� 5D      $
��Auto �6+   �� 5E      $��* �6+   .+�5G      � $��50��6   $��50��6&   $��10q�62   .+�5H      A� ���#F443366"   �� 5I      $
��👤��6$   
�������$��24(�6A   $
��Bold"�6O   $��Center=�5J   $   $��Center=�6A   5K      �� 5L      $��1��6!   $��Center=�61   $��10,0,0,0q�6L   .+�5M      �� $��Khách hàng��6$   $��16(�66   
�������6D   �� 5N      $��0��6$   $��30(�6-   $
��Bold"�6;   ���#F443366M   �� 5O      $��↓ 100%��6$   $��12(�62   
��� � �6@   5Q      5R      5S      5T      #   LiveCharts.Wpf.CartesianChart  5W   
   $��1��6   $��10,176,87,55q�6*   &    LiveCharts.Wpf.Charts.Base.Chart   Zoom$ X=�6@   
   Pan$ X=�6I      LegendLocation$	 Top=�6Q   $��2��6f      Series 5Y      !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series   Title$ Chi nhánh trung tâm��6#      Fill �#2196F3	   Values	 6?   �� *	SalesData+6\   
   AxisX
 5]         LiveCharts.Wpf.Axis     Title$ Giờ��6      Labels9   3LiveCharts.Wpf.Converters.StringCollectionConverter$_ Y08:00,09:00,10:00,11:00,12:00,13:00,14:00,15:00,16:00,17:00,18:00,19:00,20:00,21:00,22:00 5^         AxisY 5b        $ Doanh số (triệu)��6      LabelFormatter 64   �� *SalesFormatter+5d      5g      5h      