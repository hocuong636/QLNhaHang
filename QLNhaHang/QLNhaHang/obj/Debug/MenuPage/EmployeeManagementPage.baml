   M S B A M L   `   `   `  ����D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      &     QLNhaHang.EmployeeManagementPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   #localclr-namespace:QLNhaHang  5   	   0sys&clr-namespace:System;assembly=mscorlib  5
      
�������   MaterialDesignBody#���   5   
   $��Regular"�5   
   $��13(�5   
   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   
      TextRenderingMode$
 Auto=�5   
     MaterialDesignFont#���  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��324* �6   �� 5      $��0* �6   �� 5       � RowDefinitions 5   
   �� 5      $��60 �6   6*   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6   $��3��6    5       �� 5!      6%   �� 5"      6%   .+�5$      �� $��1��6    �� Orientation$ 
Horizontal=�6*   $��RightToLeft=�6C   .+�5&      �� -   5        $	��150��5&      $��38��6%   $��Thêm nhân viên��61   $��15(�6J   $��Roboto)�5'      $
��Bold"�6)   ���	#FF478C10$��85 0 0 0q�5(       J� Clip 5)      �� 5*       �� Rect$ 
0,0,150,38�6,    �� RadiusX$ 10Q�6>    �� RadiusY$ 10Q�6K   5+      5-      5/      � 50      $��0��6   .+�52      (  "MaterialDesignThemes.Wpf.ColorZone  	   Mode$	 Standard=�6+   
   CornerRadius$
 5��6;   $
��Left=�6L   $��85,0,0,0q�6g   $��Center=�53   +   $	��250��6G   .��54      � ���	#FFF7DFDF 55      �� 56      $
��Auto �6/   �� 57      $��* �6/   .+�59      �� $��0��6!     MaterialDesignToolButton#���  61   .��5:      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=�66    J� Opacity$
 0.56Q�6D   5;      �� 5<      -   5         ��Name$ 
txbTimKiem��5<   2   $��1��6"   $��0 0 10 0q�5=   !   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm nhân viên ��5>   !   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5?   !   $��0q�5@   !   $	��200��5A   !   $��LeftToRight=�5B   !   $��Center=�5D   !   $y�	Collapsed=�6<   5F      5G      �� 5I      $��	100 0 0 0q�5J      $��60��5K      $��50��6    
������ $��0q�5M       5N      �� 5O      $ 	0,0,60,50�6,   $ 50Q�6=   $ 60Q�6J   5P      .��5R        $
 Send=�6.   $��30��6:   $��30��5S   *   
���   �$	 0.7Q�5T   =   5U      5V      5W      � 5[   
   $��1��6   $��3��6   .+�5\      � $
��1220��6   $	��500��6    ���#F2F1F1 5]      �� 5^      $ 0,0,1220,500�6(   $ 10Q�6<   $ 10Q�6I   5_      .+�5a      �� -   5         ��Name$ 
lvNhanVien��5a      $��Disabled=�6/    � 	Resources 5c      ��%Y   (��       5d   %   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   10Q�6g   5e       �� View 5f      �� 5g      ! �� ColumnHeaderContainerStyle 5i      �� 5j   "    �� 
TargetType" ��6(   .�5k   &   ��  �� Property � �� �� Value
 ��ح�6C   �� 5l   &    � ��
 �   �6C   �� 5m   &    � ��$
 Bold"�6C   5n   #   .P�5p      �� $��Mã NV��6.    �� Width$ 60��6>    �� DisplayMemberBinding 6I   �� *
MaNhanVien+�� 5q      $��Tên Nhân Viên��6-   $	 150��6D    6P   �� *HoTen+�� 5r      $
��SĐT��6-   $	 110��6:    6F   �� *	DienThoai+�� 5s      $��Dịa chỉ��6-   $	 270��6>    6J   �� *DiaChi+�� 5t      $��Mã Quyền��6-   $ 70��6?    6J   �� *	MaQuyen+�� 5u      $
��NTNS��6-   $	 100��6;    6G   ��  �� Path$
 NTNS� �� StringFormat$ 
MM/dd/yyyy���� 5v      $��Chức vụ��6-   $	 120��6>    6J   �� *ChucVu+�� 5w      $��Giới tính��6-   $ 80��6@    6K   �� *
GioiTinh+�� 5x      $
��CCCD��6-   $	 100��6;    6G   �� *CCCD+�� 5y      $��Lương��6-   $	 150��6<    6H   �� *Luong+$ 
{0:N0} VND��5z      5|      5}      5~      5      5�      