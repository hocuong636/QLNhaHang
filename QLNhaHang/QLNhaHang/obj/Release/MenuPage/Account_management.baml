   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:QLNhaHang.MenuPageQLNhaHang.MenuPage  +    %QLNhaHang.MenuPage.Account_management   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:QLNhaHang.MenuPage     $	��790�����#F0F4F8$
��1325��  � 	Resources     RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  ModernTabItemStyle ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName  ModernTextBoxStyle ��Name  ModernPasswordBoxStyle��%+  &	        &	 �     &	 =    &	      �� " ��.���  � �� �� " ��.R��� $ 22��#��z��._��� $��Center=�$��Center=���  � ��
 �������  � ��$ Medium"���  � ��$ 45���� " ��.���  � �� �� " ��.R��� $��Border��$��0,0,4,0q�$��0,0,0,2q�
������ 
������ ._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,5q� P�  �@��
IsSelected	 . . ��� $
 Border�� � �� �#1976D2��  � �� �#1976D2P�  �@��
IsSelected	 .  . ���  � �� �#666��  � ��$ 14(���  � ��$ SemiBold"��� " ��.���  � ��$ 40����  � ��$
 10,8q���  � ��
 �������  � ��$ 1q���  � �� �#E0E6ED��  � �� �� " ��.R��� $ 5��#��z��#��z��#��z��._��� $ PART_ContentHost���� " G�.���  � ��$ 40����  � ��$
 10,8q���  � ��
 �������  � ��$ 1q���  � �� �#E0E6ED��  � �� �� " G�.R��� $ 5��#��z��#��z��#��z��._��� $ PART_ContentHost��.��� $��
0,0,105,15q�.+��� ���#1976D2$��80��$	��Top=�$��0,0,0,0q�._��� $��QUẢN LÝ TÀI KHOẢN��$��28(�$
��Bold"�
�������$��Center=�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ 	#40000000��   	Direction$	 320Q�   ShadowDepth$ 3Q�   
BlurRadius$ 4Q��� 
�������$ 15��$��20,100,20,36q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._��� -   
������ $��0q�$��20q�.@��� $��Tạo Tài Khoản Mới��#��[ .���� $
��Auto=�.���� $��20q�.+��� $��Tên Đăng Nhập:��$��0,0,0,5q�$��Medium"��� -   $��txtTenDangNhap��#��[ $��0,0,0,15q��� $��Mật Khẩu:��$��0,0,0,5q�$��Medium"�G� -   $��
txtMatKhau��#��[ $��0,0,0,15q��� $��
Họ Tên:��$��0,0,0,5q�$��Medium"��� -   $��txtHoTen��#��[ $��0,0,0,15q��� $��Email:��$��0,0,0,5q�$��Medium"��� -   $��txtEmail��#��[ $��0,0,0,15q��� $��Điện Thoại:��$��0,0,0,5q�$��Medium"��� -   $��txtDienThoai��#��[ $��0,0,0,15q��� $��Quyền:��$��0,0,0,5q�$��Medium"��� -   $��cmbQuyen��$��40��$
��10,8q�$��0,0,0,20q�.@��� $��Admin���� $��Nhân Viên���� -   $��Tạo Tài Khoản�����	#FF15D21D#��[  $	��200���� $��Quản Lý Mật Khẩu��#��[ .���� $
��Auto=�.���� $��20q�.+��� $C��=Nhập Email hoặc Tên Đăng Nhập để tìm thông tin:��$��16(�$��SemiBold"����#1976D2$��0,0,0,15q��� -	   $��txtRecovery��#��[ $��0,0,0,15q�$%��Email hoặc Tên Đăng Nhập��
��������� -
   $��Tìm Tài Khoản�����#1976D2#��[  $	��200��$��0,0,0,30q��� $��Mật khẩu mới:��$��0,0,0,5q�$��Medium"�G� -   $��txtNewPassword��#��[ $��0,0,0,15q��� $&�� Nhập lại mật khẩu mới:��$��0,0,0,5q�$��Medium"�G� -   $��txtConfirmPassword��#��[ $��0,0,0,20q��� -   $��Cập Nhật Mật Khẩu�����#FF9800#��[  $	��200��$��0,0,0,15q��� -   $��txtNotification��$/��)Thông báo sẽ xuất hiện ở đây.��
�������$��12(�$y�	Collapsed=��� $��Thông Tin Tài Khoản��#��[ .��� $��20q� � RowDefinitions �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -   $��dgNguoiDung��$��0��   AutoGenerateColumns .     HeadersVisibility$ Column=�   CanUserAddRows .     SelectionMode$ Single=�   SelectionUnit$ FullRow=�   
IsReadOnly . 
������ $��1q����#E0E6ED   RowBackground
 �����   AlternatingRowBackground �#F8F9FA   HorizontalGridLinesBrush �#E0E6ED   VerticalGridLinesBrush �#E0E6ED$��0,0,0,20q�  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *MaNguoiDung+  $ Tên Đăng Nhập��$ *  �� *TenDangNhap+  $ 	Họ Tên��$ *  �� *HoTen+  $ Email��$ *  �� *Email+  $ Điện Thoại��$ *  �� *	DienThoai+  $ Quyền��$	 100  �� *
TenQuyen+�� -   $��1��$��Xóa Tài Khoản�����#F44336#��[  $	��200��$��Right=�