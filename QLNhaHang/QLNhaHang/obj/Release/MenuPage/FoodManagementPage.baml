   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  "    QLNhaHang.FoodManagementPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:QLNhaHang     ���#F0F4F8  � 	Resources     ModernButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Padding  ModernTextBoxStyle ��Name��%�  &	        &	 �     �� " ��.���  � ��$ 40����  � ��$
 20,0q���  � ��
 �������  � ��$ SemiBold"���  � ��$ 0q���  � �� �� " ��.R��� #��z��$ 20��# z��._��� $��Center=�$��Center=��� " ��.���  � ��$ 40����  � ��$
 15,8q���  � ��
 �������  � ��$ 1q���  � �� �#E0E6ED��  � �� �� " ��.R��� $ 20��#��z��#��z��#��z��._��� $ PART_ContentHost��$	��5,0q�.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� ���#1976D2$��80��$	��Top=�$��0,0,0,0q�._��� $��QUẢN LÝ MÓN ĂN��$��28(�$
��Bold"�
�������$��Center=�$��Center=�	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   Color$
 	#40000000��   	Direction$	 320Q�   ShadowDepth$ 3Q�   
BlurRadius$ 4Q�� $��1��$��20,20,20,20q� � ColumnDefinitions �� $��* ��� $
��Auto �.+��� 
�������$ 15��$��0,0,10,0q�	   $
 	#20000000��$ 15Q�$ 2Q�$	 315Q�._�]� $��20q�.+�� $	��Top=�$��0,0,0,20q� �� $
��Auto ��� $
��Auto �.+��  �� $	��500 ��� $	��100 ��� $	��100 �.+��� -    ��Name$ searchTextBox��#��[ $��0,0,10,0q�.��  ��%:   '��       �� " ��.���  �@��CornerRadius$ 20��.���� -   $��1��$��Tìm Kiếm��#��[  ���#4CAF50$��0,0,5,0q��� -   $��2��$��
Làm Mới��#��[  ���#FF9800$��5,0,0,0q�&   System.Windows.Controls.DataGrid  -   $��dgMonAn��   AutoGenerateColumns .     
IsReadOnly . 
������ $��1q����#E0E6ED   RowBackground
 �����   AlternatingRowBackground �#F8F9FA   HorizontalGridLinesBrush �#E0E6ED   VerticalGridLinesBrush �#E0E6ED  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Tên Món��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
TenMonAn+  $ Mô Tả��$ 2*  �� *MoTa+  $ Giá (VNĐ)��$	 120  �� *Gia+ �� StringFormat$ {0:N0}��   ElementStyle �� " ��.���  � ��$ Right=�  $ Số Lượng��$	 100  �� *	SoLuong+ �� " ��.���  � ��$ Center=��� $��1��
�������$ 15��$��
10,0,0,134q�	   $
 	#20000000��$ 15Q�$ 2Q�$	 315Q�._��� $��15q�.+��� $��Thêm Món Ăn Mới��$��20(�$��SemiBold"����#1976D2$��0,0,0,20q��� $��
Tên Món:��$��0,0,0,5q�$��Medium"��� -   $ txtTenMonAn��#��[ $��0,0,0,10q��� $��	Mô Tả:��$��0,10,0,5q�$��Medium"��� -   $ txtMoTa��#��[ $��80�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . $��0,0,0,10q��� $��Giá (VNĐ):��$��0,10,0,5q�$��Medium"��� -   $ txtGia��#��[ $��0,0,0,10q��� $��Số Lượng:��$��0,10,0,5q�$��Medium"��� -   $ 
txtSoLuong��#��[ $��0,0,0,20q���  �� Orientation$ 
Horizontal=�.+��� -	   $��Thêm��#��[  ���#4CAF50$��90��$��5,0,5,0q��� -
   $��Hủy��#��[  ���#757575$��90��$��0,0,5,0q��� -   $��Sửa��#��[  ���#2196F3$��0,0,5,0q�$��90���� -   $
��Xóa��#��[  ���#F44336$��90��