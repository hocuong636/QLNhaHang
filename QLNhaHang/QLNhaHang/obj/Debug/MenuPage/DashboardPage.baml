   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       	QLNhaHang   QLNhaHang.DashboardPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   ���#2196F3 � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��	Dashboard��6   $��20(�6)   $
��Bold"�67   $��10q�6I   
�������6U   �� 5      $��1��6    �� Orientation$ 
Horizontal=�6)   $��10q�6B   .+�5      �� $��Thông báo��6   $��0,0,10,0q�6-   ��6?   �� *ShowNotificationsCommand+��5      �� 5       �� 
TargetType" ��6*   .R�5      � .+�5   "   �� $
��🔔��6,   $��20(�66   A� 5    "   $��15��6*   $��15��65   
���  ��$��Right=�6L   $	��Top=�6h   $��	0,-5,-5,0q�6�   �� 5!   "   $��10(�6O   
�������$��Right=�6p   $	��Top=�6�   $��	0,-5,-5,0q�6�   ��6,   �� *NotificationCount+6�   5"      5#      5%      �� 5&      $��Center=�6?   
���������6   �� *CurrentUser.HoTen+6Z   5'      5(      � 5+   
   $��1��6   $��10q�6    5,      �� 5-      $��* �6#   �� 5.      $��* �6#   �� 5/      $��* �6#   .+�53      �� $��0��6   ���	#FF78DC7C �� CornerRadius$ 5��6<   $��5q�6M   ._�54      �� $��10q�6   .+�55      �� $��Tổng Doanh Thu��6    $��16(�66   
�������6D   �� 56      -   5         ��Name$ TotalRevenueTextBlock��56       $��0 VNĐ��6?   $��24(�6L   $
��Bold"�6Z   
�������6l   57      58      �� 5;      $��1��6   ���#2196F3$ 5��6:   $��5q�6K   ._�5<      �� $��10q�6   .+�5=      �� $��Bàn Đang Phục Vụ��6    $��16(�68   
�������6F   �� 5>      -   5        $ ActiveOrdersTextBlock��5>       $��0��6?   $��24(�6H   $
��Bold"�6V   
�������6h   5?      5@      �� 5C      $��2��6   ���#FFC107$ 5��6:   $��5q�6K   ._�5D      �� $��10q�6   .+�5E      �� $��Tra Cứu Doanh Thu��6    $��16(�69   
�������6G   (  "System.Windows.Controls.DatePicker  5F      -   5          Name$ SelectedDatePicker��5F   !   $	��0,5q�6=   �� 5G      -   5        $��	Tra Cứu��5G      $	��0,5q�6K   �� 5I      -   5        $ LookedUpRevenueTextBlock��5I       $��0 VNĐ��6B   $��20(�6O   $
��Bold"�6]   
�������6o   5J      5K      5L      � 5O   
   $��2��6   $��10q�6   .+�5P      � .+�5Q      �� $	��385��6   ._�5R      #   LiveCharts.Wpf.CartesianChart  -   5          Name$ RevenueChart��5R   )   $��0,0,0,10q�6\   &    LiveCharts.Wpf.Charts.Base.Chart   Series 5T      !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series	   Title$	 	Doanh Thu��6/   
   Values
 6A   �� *RevenueValues+   AxisX 5W         LiveCharts.Wpf.Axis     Title$ Ngày trong tuần��6'      Labels 6?   �� *DayOfWeekLabels+   AxisY 5Z        $ Tổng Doanh Thu (VNĐ)��6'      LabelFormatter 6D   �� *	Formatter+5\      5]      5^      5_      5`      5a      