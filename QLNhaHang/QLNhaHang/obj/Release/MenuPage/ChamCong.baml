   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:QLNhaHang.MenuPageQLNhaHang.MenuPage  !    QLNhaHang.MenuPage.ChamCong   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:QLNhaHang.MenuPage     ���#F0F4F8$	��790��$
��1325��  � 	Resources     RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  RoundedTextBoxStyle ��Name��%R  &	        &	 �     �� " ��.���  � �� �� " ��.R��� $ 22��#��z��._��� $��Center=�$��Center=���  � ��
 �������  � ��$ Medium"���  � ��$ 15,10q��� " ��.���  � �� �� " ��.R��� $ 5�����#E0E6ED$��1q�
�������._��� $ PART_ContentHost����  � ��$
 10,5q�.��� $��0,0,15,5q� � RowDefinitions �� $
��Auto ��� $��* �.+��� ���#1976D2$��80��$��Center=�$
��Left=�$
��1256��._��� $��QUẢN LÝ CHẤM CÔNG��$��28(�$
��Bold"�
�������$��Center=�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   Color$	 	#40000000��
   	Direction$	
 320Q�   ShadowDepth$ 3Q�   
BlurRadius$ 4Q�� $��1��$��20,20,20,50q� � ColumnDefinitions �� $	��750 ��� $	��430 �.+��� $��0��
�������$ 15��$��0q�   $	 	#20000000��$ 15Q�$ 2Q�$	
 315Q�._�]� $��20q�.+��� $	��Top=� �� Orientation$ 
Horizontal=�$��0,0,0,20q�.+��� -    ��Name$ searchTextBox�� �� TextAlignment$
 Left=�$��Center=�$��40��$	��400��$��10,0,0,0q�$��0,0,0,0q�.��  ��%:   '��       �� " ��.���  �@��CornerRadius$ 20��.���� -   $��Tìm Kiếm��$
��15,0q�$	��120��$��30�����#4CAF50#��[   J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� ��  �� AngleX$ 0.975Q��� T�  T� X$ 0.383Q�   $	 	#40000000��$	
 320Q�$ 2Q�$ 2Q��� -   $��
Làm mới��$
��15,0q�$	��120��$��30�����#FF9800#��[  $ 0.5,0.5&�z�V� .~��� �� $ 0.975Q��� T� $ 0.383Q�   $	 	#40000000��$	
 320Q�$ 2Q�$ 2Q�&   System.Windows.Controls.DataGrid  -   $��
dgChamCong��   AutoGenerateColumns .     
IsReadOnly . 
������ $��0q�   RowBackground
 �����   AlternatingRowBackground �#F5F7FA   CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeRows .     ColumnHeaderHeight$ 50Q�   	RowHeight$ 40Q�   VerticalGridLinesBrush
 ����    HorizontalGridLinesBrush �#E0E6ED  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,0,1q���  � �� �#E0E6ED    Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$! Mã NV��"   Width5  /System.Windows.Controls.DataGridLengthConverter$" 70 1  +System.Windows.Controls.DataGridBoundColumn#   Binding# �� *
MaNhanVien+  $! 	Họ Tên��$	" 200 # �� *HoTen+  $! Ngày��$	" 100 # �� *Ngay+$ �� StringFormat$$ 
dd/MM/yyyy��  $! 
Giờ Vào��$	" 120 # �� *ThoiGianVao+$$ HH:mm��  $! Giờ Ra��$	" 120 # �� *
ThoiGianRa+$$ HH:mm���� $��1��
�������$ 15��$��15,0,0,0q�   $	 	#20000000��$ 15Q�$ 2Q�$	
 315Q�._��� $��20q�.+��� $��Chấm Công��$��20(�$��SemiBold"����#1976D2$��0,0,0,20q��� $��Mã Nhân Viên:��$��0,0,0,5q��� -   $��txtMaNhanVien��$��0,0,0,15q�#��[ � $��0,0,0,20q� �� $��* ��� $��* �.+��� -   $��0��$��Vào Ca��$��30,0,5,0q����#4CAF50#��[  �� -   $��1��$��Kết Thúc Ca��$��5,0,30,0q����#F44336#��[  $��34��$��Center=��� -   $��txtCurrentDateTime��$��16(�$
��Bold"�$��0,0,0,20q�$��Center=��� $��Thông Tin Nhân Viên��$��18(�$��SemiBold"����#1976D2$��0,0,0,15q��� .+��� $��
Họ Tên:�����#455A64$��Medium"��� -	   $ txtHoTen��$��2��$��1��$��30��$��0,0,0,10q�#��[ �� $��Chức Vụ:�����#455A64$��Medium"��� -
   $ 	txtChucVu��$��2��$��1��$��30��$��0,0,0,10q�#��[ �� $��Tổng số giờ làm:�����#455A64$��Medium"��� -   $ txtTGL��$��2��$��1��$��30��$��0,0,0,10q�#��[ 