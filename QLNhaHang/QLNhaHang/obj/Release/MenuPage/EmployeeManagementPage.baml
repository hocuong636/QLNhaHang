   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem &     QLNhaHang.EmployeeManagementPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  #localclr-namespace:QLNhaHang  0sys&clr-namespace:System;assembly=mscorlib  
�������   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignFont#���  .���  � ColumnDefinitions �� $
��324* ��� $��0* ���  � RowDefinitions �� $��60 ��� $
��auto �.+�� $��0��$��3�� �� �� .+��� $��1�� �� Orientation$ 
Horizontal=�$��RightToLeft=�.+��� -   $	��150��$��38��$��Thêm nhân viên��$��15(�$��Roboto)�$
��Bold"����	#FF478C10$��85 0 0 0q� J� Clip ��  �� Rect$ 
0,0,150,38� �� RadiusX$ 10Q� �� RadiusY$ 10Q�� $��0��.+�(  "MaterialDesignThemes.Wpf.ColorZone  	   Mode$	 Standard=�
   CornerRadius$
 5��$
��Left=�$��85,0,0,0q�$��Center=�$	��250��.��� ���	#FFF7DFDF �� $
��Auto ��� $��* �.+��� $��0��  MaterialDesignToolButton#���  .��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=� J� Opacity$
 0.56Q��� -    ��Name$ 
txbTimKiem��$��1��$��0 0 10 0q�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm nhân viên ��.  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�$��0q�$	��200��$��LeftToRight=�$��Center=�$y�	Collapsed=��� $��	100 0 0 0q�$��60��$��50��
������ $��0q� �� $ 	0,0,60,50�$ 50Q�$ 60Q�.��  $
 Send=�$��30��$��30��
���   �$	 0.7Q�� $��1��$��3��.+�� $
��1220��$	��500�����#F2F1F1 �� $ 0,0,1220,500�$ 10Q�$ 10Q�.+��� -    ��Name$ 
lvNhanVien��$��Disabled=� � 	Resources ��%F   (��       *,*SystemParameters.VerticalScrollBarWidthKey+)X�10Q� �� View �� ! �� ColumnHeaderContainerStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value
 ��ح���  � ��
 �   ���  � ��$
 Bold"�.P��� $��Mã NV�� �� Width$ 60�� �� DisplayMemberBinding �� *
MaNhanVien+�� $��Tên Nhân Viên��$	 150�� �� *HoTen+�� $
��SĐT��$	 110�� �� *	DienThoai+�� $��Dịa chỉ��$	 230�� �� *DiaChi+�� $��Mã Quyền��$ 70�� �� *	MaQuyen+�� $
��NTNS��$	 100�� ��  �� Path$
 NTNS� �� StringFormat$ 
MM/dd/yyyy���� $��Chức vụ��$	 120�� �� *ChucVu+�� $��Giới tính��$ 70�� �� *
GioiTinh+�� $
��CCCD��$	 100�� �� *CCCD+�� $��Lương��$	 150�� �� *Luong+$ 
{0:N0} VND���� $ 60��$
��Xóa�� �� CellTemplate �� .R��� -   $��25��$��25��$��0q�����  �� ImageSource$' !/GUI;component/Res/delete_vs2.png��