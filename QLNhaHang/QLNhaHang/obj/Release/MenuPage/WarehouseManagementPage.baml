   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  '    !QLNhaHang.WarehouseManagementPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:QLNhaHang     ���#F0F4F8  � 	Resources     ModernTextBox �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name  ModernComboBox ��Name � ColumnDefinitions	 �� SelectionBoxItem
 �� SelectionBoxItemTemplate �� IsDropDownOpen � AllowsTransparency K� IsItemsHost  ModernButton J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   	Direction �� Triggers P� Property P� Value �� 
TargetName  DeleteButton �� BasedOn �� Triggers��%�  &	        &	 �     &	 �    &	     7  �� " ��.���  � ��$ 40����  � ��
 �������  � �� �� " ��.R��� $ 8��#��z�����#E0E6ED$��1q�._��� $ PART_ContentHost��$
��15,8q��� " ��.���  � ��$ 40����  � ��
 �������  � �� �� " ��.R�� .+��� $ border��$ 8��#��z�����#E0E6ED$��1q�._��  �� $��* ��� $
��Auto �.+��� #��z	 #��z
 $
��15,8q�F� $��1��"����        �@KL @KL р��     	���#666666$��2��$��0,0,15,0q�$��Center=�$��Center=�� #��z $��Bottom=� . .���� 
����������#E0E6ED$��1q�$ 8��$��0,5,0,0q�._��� $	��200��.����  . �� " ��.���  � ��$ 40����  � �� �#2196F3��  � ��
 �������  � ��$ SemiBold"���  � �� �� " ��.R��� $ border��#��z��$ 8��._��� $��Center=�$��Center=�   $ 	#40000000��$ 10Q�$ 3Q�$	 315Q� P�  �@J�IsMouseOver . . ��� $ border�� � �� �#1976D2P�  � �� . . ��� $ border�� � �� �#0D47A1�� " ��8   .���  � �� �#F44336 P�  �@J�IsMouseOver . . ���  � �� �#D32F2FP�  � �� . . ���  � �� �#C62828.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� ���#1976D2$��80��$	��Top=�._��� $��QUẢN LÝ KHO��$��28(�$
��Bold"�
�������$��Center=�$��Center=�   $ 	#40000000��$	 320Q�$ 3Q�$ 4Q�� $��1��$��20q� �� $	��680 ��� $	��500 �.+��� 
�������$ 15��$��10,10,10,10q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._�]� $��20q�.+�� $	��Top=�$��	0,0,20,20q� �� $	��360 ��� $��* ��� $��* �.+��� -    ��Name$ SearchTextBox��#��[  $��0,0,10,0q��� -   $��1��$��Tìm kiếm��#��[ $	��100�����#4CAF50$��	10,0,10,0q�$	��NaN���� -   $��2��$��
Làm mới��#��[ $	��100�����#FF9800$��	10,0,10,0q��� $��Bottom=� �� Orientation$ 
Horizontal=�$��0,20,0,0q�$��Right=�&   System.Windows.Controls.DataGrid  -     Name$ InventoryDataGrid��   AutoGenerateColumns .     
IsReadOnly . 
������ $��1q����#E0E6ED   RowBackground
 �����    AlternatingRowBackground  �#F8F9FA!   HorizontalGridLinesBrush! �#E0E6ED"   VerticalGridLinesBrush" �#E0E6ED$	��549��  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED#   Columns# 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn$   Header$$ Tên Nguyên Liệu��%   Width5  /System.Windows.Controls.DataGridLengthConverter$% * 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& �� *TenNguyenLieu+  $$ Số Lượng��$% * & �� *	SoLuong+  $$ Đơn vị tính��$% * & �� *DonVi+  $$ 
Đơn giá��$% * & �� *DonGia+' �� StringFormat$' 	{0:N0} đ���� $��1��
�������$ 15��$��10,10,15,180q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._��� $��20q�.+��� $��
Nhập Kho��$��20(�$��SemiBold"����#1976D2$��0,0,0,20q��� $��Tên Nguyên liệu:��$��0,0,0,5q�$��Medium"��� -   $ ProductNameTextBox��#��[  $��0,0,0,15q��� $��Số lượng:��$��0,0,0,5q�$��Medium"��� -   $ QuantityTextBox��#��[  $��0,0,0,15q��� $��Đơn vị tính:��$��0,0,0,5q�$��Medium"��� -   $ UnitTextBox��#��[  $��0,0,0,15q��� $��Đơn giá:��$��0,0,0,5q�$��Medium"��� -   $ PriceTextBox��#��[  $��0,0,0,15q��� $ 
Horizontal=�$��0,0,0,10q�.+��� -	   $��Nhập��#��[ ���#4CAF50$��90��$��15,0,0,0q��� -
   $��Hủy��#��[ ���#757575$��90��$��15,0,0,0q��� -   $��Sửa��#��[ ���#2196F3$��15,0,0,0q�$��90���� -   $
��Xóa��#��[ ���#F44336$��15,0,0,0q�$��90���� $ 
Horizontal=�$��Right=�