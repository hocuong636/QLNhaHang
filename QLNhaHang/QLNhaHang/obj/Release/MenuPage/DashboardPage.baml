   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf   	QLNhaHang   QLNhaHang.DashboardPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  
�������.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�����#2196F3 � ColumnDefinitions �� $��* ��� $
��Auto �.+��� $��	Dashboard��$��20(�$
��Bold"�$��10q�
��������� $��1�� �� Orientation$ 
Horizontal=�$��10q�.+��� $��Thông báo��$��0,0,10,0q����� *ShowNotificationsCommand+����  �� 
TargetType" ��.R�� .+��� $
��🔔��$��20(�A� $��15��$��15��
���  ��$��Right=�$	��Top=�$��	0,-5,-5,0q��� $��10(�
�������$��Right=�$	��Top=�$��	0,-5,-5,0q����� *NotificationCount+�� $��Center=�
����������� *CurrentUser.HoTen+� $��1��$��10q� �� $��* ��� $��* ��� $��* �.+��� $��0�����	#FF78DC7C �� CornerRadius$ 5��$��5q�._��� $��10q�.+��� $��Tổng Doanh Thu��$��16(�
��������� -    ��Name$ TotalRevenueTextBlock��$��0 VNĐ��$��24(�$
��Bold"�
��������� $��1�����#2196F3$ 5��$��5q�._��� $��10q�.+��� $#��Đơn Hàng Đang Phục Vụ��$��16(�
��������� -   $ ActiveOrdersTextBlock��$��0��$��24(�$
��Bold"�
��������� $��2�����#FFC107$ 5��$��5q�._��� $��10q�.+��� $��Tra Cứu Doanh Thu��$��16(�
�������(  "System.Windows.Controls.DatePicker  -     Name$ SelectedDatePicker��$	��0,5q��� -   $��	Tra Cứu��$	��0,5q��� -   $ LookedUpRevenueTextBlock��$��0 VNĐ��$��20(�$
��Bold"�
�������� $��2��$��10q�.+��� 
�������$ 5�� �� Padding$ 10q�$��5q�._��� .+��� $	��300��._�� .+��� 
�������$ 5��$ 10q�$��5q�._��� .+��� $*��$Biểu Đồ Doanh Thu (7 Ngày Qua)��$��16(�$
��Bold"�$��0,0,0,10q�#   LiveCharts.Wpf.CartesianChart  -     Name$ RevenueChart��$	��300��&    LiveCharts.Wpf.Charts.Base.Chart	   Series	    LiveCharts.Wpf.LineSeries     LiveCharts.Wpf.Series
   Title$
 	Doanh Thu��   PointGeometrySize$ 15Q�   Values �� *RevenueValues+   AxisX    LiveCharts.Wpf.Axis     Title$ Ngày��   Labels �� *RevenueLabels+   AxisY   $
 VNĐ��   LabelFormatter �� *	Formatter+