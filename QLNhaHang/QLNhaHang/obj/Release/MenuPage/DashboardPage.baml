   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf   	QLNhaHang   QLNhaHang.DashboardPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  ���#F0F4F8  � 	Resources     MetricCardStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   	Direction  MetricTitleStyle  MetricValueStyle  ModernDatePickerStyle(  "System.Windows.Controls.DatePicker  ModernButtonStyle �� 
TargetType	 �� CornerRadius��%  &	        &	 �     &	 	    &	 \    &	 �    �� " ��.���  �@��CornerRadius$ 15����  �@��Padding$ 20q���  �@J�Effect   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q��� " ��.���  � ��$ 16(���  � ��$ SemiBold"���  � ��
 �������  � ��$ 0,0,0,10q��� " ��.���  � ��$ 28(���  � ��$
 Bold"���  � ��
 ������� "  .���  � �� �#FFFFFF��  � ��$ 1q���  � �� �#E0E6ED��  � ��$ 40����  � ��$
 10,5q��� " ��.���  � ��$ 40����  � ��
 �������  � �� �#1976D2��  � ��$ SemiBold"���  � ��$ 0q���  � ��$
 20,0q���  � �� �� " ��.R��� #��z��$	 20��#��z��#��z��._��� $��Center=�$��Center=�.��� .+��� ���#1976D2$��80��$	��Top=�$��0,0,0,0q�._�� 
 � ColumnDefinitions
 �� $��* ��� $
��Auto �.+��� $��TỔNG QUAN��$��28(�$
��Bold"�
�������$
��20,0q�$��Center=�$��Center=� J� Effect   $ 	#40000000��$	 320Q�$ 3Q�$ 4Q��� $��1�� �� Orientation$ 
Horizontal=�$
��20,0q�$��Center=�.+��� $��Center=�
�������$��16(�$��Medium"����� *CurrentUser.HoTen+� $��20,100,20,20q� � RowDefinitions �� $
��Auto ��� $��* �.+�� 
 �� $��* ��� $��* ��� $��* �.+��� $��0��#��[  ���#4CAF50$��0,0,10,0q�._��� .+��� $��Doanh Thu Ngày��#��[ �� -    ��Name$ DayTotalRevenueTextBlock��$��0 VND��#��[ �� $��1��#��[  ���#1976D2$
��10,0q�._��� .+��� $��Doanh Thu Tháng��#��[ �� -   $  MonthTotalRevenueTextBlock��$��0 VND��#��[ �� $��2��#��[  ���#FF9800$��10,0,0,0q�._��� .+��� $��Tra Cứu Doanh Thu��#��[   -     Name$ SelectedDatePicker��#��[ $
��0,10q��� -   $��	Tra Cứu��#��[ $��0,0,0,10q��� -   $ LookedUpRevenueTextBlock��$��0 VND��#��[ �� $��1��
�������$	 15��$��0,20,0,0q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._�� $��20q� �� $
��Auto ��� $��* �.+��� $(��"Biểu Đồ Doanh Thu Theo Tuần��$��20(�$��SemiBold"����#1976D2$��0,0,0,20q�#   LiveCharts.Wpf.CartesianChart  -     Name$ RevenueChart��$��1��&    LiveCharts.Wpf.Charts.Base.Chart   Series !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series   Title$ 	Doanh Thu��   Fill �#1976D2   Values �� *RevenueValues+   AxisX    LiveCharts.Wpf.Axis     Title$ Ngày��   Labels �� *
DaysOfWeek+   AxisY   $ Doanh Thu (VND)��   MinValue$ 0Q�