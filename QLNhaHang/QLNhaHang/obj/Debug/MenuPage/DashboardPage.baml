   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       	QLNhaHang   QLNhaHang.DashboardPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      ���#F0F4F8  � 	Resources  5         MetricCardStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   	Direction  MetricTitleStyle  MetricValueStyle  ModernDatePickerStyle(  "System.Windows.Controls.DatePicker  ModernButtonStyle �� 
TargetType	 �� CornerRadius��%S  &	        &	     &	 �    &	 W    &	 <    �� 5   
   " ��6(   .�5      ��  �@��CornerRadius$ 15��6-   �� 5       �@��Padding$ 20q�6(   �� 5       �@J�Effect 5        5      $ 	#20000000��6'   $ 15Q�69   $ 2Q�6I   $	 315Q�6Y   5      5      �� 5   
   " ��6)   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � ��$ 0,0,0,10q�6'   5      �� 5   
   " ��6)   .�5       ��  � ��$ 28(�6)   �� 5!       � ��$
 Bold"�6+   �� 5"       � ��
 �����6+   5#      �� 5%   
   "  6.   .�5&      ��  � �� �#FFFFFF6+   �� 5'       � ��$ 1q�60   �� 5(       � �� �#E0E6ED6,   �� 5)       � ��$ 40��6'   �� 5*       � ��$
 10,5q�6(   5+      �� 5-   
   " ��6*   .�5.      ��  � ��$ 40��6'   �� 5/       � ��
 �����6+   �� 50       � �� �#1976D26+   �� 51       � ��$ SemiBold"�6+   �� 52       � ��$ 0q�60   �� 53       � ��$
 20,0q�6(   �� 54       � �� 55      �� 56      " ��6&   .R�57      �� #��z��6!   $	 20��58   !   #��z��59   !   #��z��5:   !   ._�5;      �� $��Center=�6/   $��Center=�6L   5<      5=      5?      5@      5A      .��5C      � .+�5E   
   �� ���#1976D2$��80��5F      $	��Top=�5G      $��0,0,0,0q�6)   
 J� Effect
 5H        5I      $ 	#40000000��6#   $	 270Q�65   $ 3Q�6E   $ 4Q�6U   ._�5L      �  � ColumnDefinitions 5M      �� 5N      $��* �6'   �� 5O      $
��Auto �6'   .+�5R      �� $��TỔNG QUAN��6   $��28(�5S      $
��Bold"�5T      
�������$
��20,0q�5V      $��Center=�5W      $��Center=�5X      
 5Y        5Z      $ 	#40000000��6+   $	 320Q�6=   $ 3Q�6M   $ 4Q�6]   5\      �� 5^      $��1��6    �� Orientation$ 
Horizontal=�5_      $
��20,0q�5`      $��Center=�5a      .+�5b      �� $��Center=�5c      
�������$��16(�5e      $��Medium"�5f      ��5b       �� *CurrentUser.HoTen+5f      5g      5h      5i      � 5l   
   $��20,100,20,20q�6    � RowDefinitions 5m      �� 5n      $
��Auto �6    �� 5o      $��* �6    .+�5s      �  5t      �� 5u      $��* �6'   �� 5v      $��* �6'   �� 5w      $��* �6'   .+�5{      �� $��0��6   #��[  5|      ���#4CAF50$��0,0,10,0q�5~      ._�5      �� .+�5�      �� $��Doanh Thu Ngày��6$   #��[ 5�   "   �� 5�      -   5         ��Name$ TotalRevenueTextBlock��5�   $   $��0 VND��5�   "   #��[ 5�   "   5�      5�      �� 5�      $��1��6   #��[  5�      ���#1976D2$
��10,0q�5�      ._�5�      �� .+�5�      �� $��Doanh Thu Tháng��6$   #��[ 5�   "   �� 5�      -   5        $ ActiveOrdersTextBlock��5�   $   $��0 VND��5�   "   #��[ 5�   "   5�      5�      �� 5�      $��2��6   #��[  5�      ���#FF9800$��10,0,0,0q�5�      ._�5�      �� .+�5�      �� $��Tra Cứu Doanh Thu��6$   #��[ 5�   "     5�      -   5          Name$ SelectedDatePicker��5�   %   #��[ 5�   #   $
��0,10q�5�   #   �� 5�      -   5        $��	Tra Cứu��5�   !   #��[ 5�   !   $��0,0,0,10q�5�   !   �� 5�      -   5        $ LookedUpRevenueTextBlock��5�   $   $��0 VND��5�   "   #��[ 5�   "   5�      5�      5�      �� 5�      $��1��6   
�������$	 15��5�      $��0,20,0,0q�5�      
 5�        5�      $ 	#20000000��6'   $ 15Q�69   $ 2Q�6I   $	 315Q�6Y   ._�5�      � $��20q�6    5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   .+�5�      �� $(��"Biểu Đồ Doanh Thu Theo Tuần��6    $��20(�5�      $��SemiBold"�5�      ���#1976D2$��0,0,0,20q�5�      #   LiveCharts.Wpf.CartesianChart  5�      -   5          Name$ RevenueChart��5�   '   $��1��5�   )   &    LiveCharts.Wpf.Charts.Base.Chart   Series 5�      !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series   Title$ 	Doanh Thu��6/      Values 5�   -   �� *RevenueValues+   AxisX 5�         LiveCharts.Wpf.Axis     Title$ Ngày trong tuần��6'      Labels 5�   &   �� *DayOfWeekLabels+   AxisY 5�        $ Tổng Doanh Thu (VNĐ)��6'      LabelFormatter 5�   &   �� *	Formatter+5�      5�      5�      5�      5�      5�      