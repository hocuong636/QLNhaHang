   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf   	QLNhaHang   QLNhaHang.DashboardPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  
�������.���   � RowDefinitions  �� $
��Auto �$
��66.6���� �� $��2* �.+��� $%��KẾT QUẢ BÁN HÀNG HÔM NAY��$��Center=�$��20(�$
��Bold"�$��	0,20,0,20q�$	��298����  �� Orientation$ 
Horizontal=�$
��Left=�$��196,-103,0,20q�$��1��$	��960��.+�#  MaterialDesignThemes.Wpf.Card  $	��300��$	��150��$��10q����	#FFFDE7E7.��� $	��150��$��Bottom=� � ColumnDefinitions �� $
��Auto ��� $��* �.+�� $��50��$��50��$��10q�.+�A� ���#F44336�� $��$��
�������$��24(�$
��Bold"�$��Center=�$��Center=��� $��1��$��Center=�$��10,0,0,0q�.+��� $��0 đơn đã xong��$��16(�
��������� $��0��$��30(�$
��Bold"����#F44336�� $��↓ 100%��$��12(�
��� � �  $	��300��$	��150��$��10q����#E8F5E9.���  �� $
��Auto ��� $��* �.+�� $��50��$��50��$��10q�.+�A� ���#4CAF50�� $	��✎��
�������$��24(�$
��Bold"�$��Center=�$��Center=��� $��1��$��Center=�$��10,0,0,0q�.+��� $��0 đơn đang phục vụ��$��16(�
��������� $��0��$��30(�$
��Bold"����#4CAF50  $	��300��$	��150��$��10q����	#FFFDE7E7.���  �� $
��Auto ��� $��* �.+�� $��50��$��50��$��10q�.+�A� ���#F44336�� $
��👤��
�������$��24(�$
��Bold"�$��Center=�$��Center=��� $��1��$��Center=�$��10,0,0,0q�.+��� $��Khách hàng��$��16(�
��������� $��0��$��30(�$
��Bold"����#F44336�� $��↓ 100%��$��12(�
��� � �#   LiveCharts.Wpf.CartesianChart  $��1��$��10,176,87,55q�&    LiveCharts.Wpf.Charts.Base.Chart   Zoom$ X=�
   Pan$ X=�   LegendLocation$	 Top=�$��2��   Series !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series   Title$ Chi nhánh trung tâm��   Fill �#2196F3	   Values	 �� *	SalesData+
   AxisX
    LiveCharts.Wpf.Axis     Title$ Giờ��   Labels9   3LiveCharts.Wpf.Converters.StringCollectionConverter$_ Y08:00,09:00,10:00,11:00,12:00,13:00,14:00,15:00,16:00,17:00,18:00,19:00,20:00,21:00,22:00    AxisY   $ Doanh số (triệu)��   LabelFormatter �� *SalesFormatter+