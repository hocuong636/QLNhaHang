   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  %    QLNhaHang.InvoiceManagementPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:QLNhaHang     ���#F0F4F8$
��1325��  � 	Resources     SearchBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name  ModernComboBoxStyle d� 	IsChecked �� Mode	 �� RelativeSource
 J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� IsDropDownOpen  ModernButtonStyle ��Name J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   	Direction �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     &	      �� " ��.���  � ��$ 40����  � ��
 �������  � �� �� " ��.R��� $ 20��#��z�����#E0E6ED$��1q�._��� $ PART_ContentHost��$
��15,8q��� " ��.���  � ��$ 40����  � ��
 �������  � �� �� " ��.R��� $ 20��#��z�����#E0E6ED$��1q�._�� .+�d�  �� *IsDropDownOpen+$ TwoWay=�	 �� *TemplatedParent+�� 
 .  #��z #��z $
��15,8q�� #��z $��Bottom=�$��Slide=�.���� 
����������#E0E6ED$��1q�$ 8��._��� $	��200��.���� �� " ��.���  � ��$ 40����  � �� �#2196F3��  � ��
 �������  � ��$ SemiBold"���  � �� �� " ��.R��� $ border��#��z��$ 20��._��� $��Center=�$��Center=�   $ 	#40000000��$ 10Q�$ 3Q�$	 315Q� P�  �@J�IsMouseOver . . ��� $ border�� � �� �#1976D2P�  � �� . . ��� $ border�� � �� �#0D47A1.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� ���#1976D2$��80��$	��Top=�._��  � ColumnDefinitions �� $��* ��� $
��Auto �.+��� $��QUẢN LÝ HÓA ĐƠN��$��28(�$
��Bold"�
�������$��Center=�$��Center=�   $ 	#40000000��$	 320Q�$ 3Q�$ 4Q��� $��1�� �� Orientation$ 
Horizontal=�$
��20,0q�$��Center=�.+��� 
�������$��16(�$��Medium"����� *CurrentUser.HoTen+� $��1��$��20q� �� $	��750 ��� $	��430 �.+��� $��0��
�������$ 15��$��0,0,10,0q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._�]� $��20q�.+��� $	��Top=�$��0,0,0,20q�.+��  �� $	��500 ��� $	��200 ��� $
��Auto �.+��� $��0��._��� -    ��Name$ SearchTextBox��#��[  $��0,0,0,0q��� $��1��._��� -   $ TextBox��#��[  $��	10,0,10,0q��� $��1��._��� -    ��Name$ ComboBox��$��0,0,0,0q�$	��150��$��30��$��Center=�$��Center=�$��Center=����#1976D2$��0q� �� SelectedIndex$ -1�� �� 
IsEditable .  �� StaysOpenOnEdit . .@��� $��Tất cả trạng thái���� $��Đã thanh toán���� $��Chưa thanh toán��&   System.Windows.Controls.DataGrid  -   $��InvoiceListView��    AutoGenerateColumns  .  !   
IsReadOnly! . 
������ $��0q�"   RowBackground
" �����#   AlternatingRowBackground# �#F8F9FA$   VerticalGridLinesBrush$ �#E0E6ED%   HorizontalGridLinesBrush% �#E0E6ED���� *InvoiceList+& �� SelectedItem& �� *SelectedInvoice+  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED'   Columns' 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn(   Header$( Mã ND��)   Width5  /System.Windows.Controls.DataGridLengthConverter$	) 100 1  +System.Windows.Controls.DataGridBoundColumn*   Binding* �� *MaNguoiDung+  $( Ngày Lập��$	) 120 * �� *	NgayLap++ �� StringFormat$+ 
dd/MM/yyyy��  $( Tổng Tiền��$	) 140 * �� *
TongTien+$+ 	{0:N0} đ��  $( 	Số Bàn��$	) 100 * �� *SoBan+  $( Trạng Thái��$) * * �� *	TrangThai+�� $��1��
�������$ 15��$��10,0,0,0q�   $ 	#20000000��$ 15Q�$ 2Q�$	 315Q�._�]� $��20q�.+��� $��Chi Tiết Hóa Đơn��$��20(�$��SemiBold"����#1976D2$	��Top=�$��0,0,0,20q��� $��Bottom=�.+��� $��0,1,0,0q����#E0E6ED, �� Padding$, 	0,20,0,20q�._��� -   - ��Name$- TotalAmountText��$��Tổng tiền: 0 VNĐ��$��18(�$
��Bold"����#2196F3$��Right=��� $ 
Horizontal=�.+��� -   $��In Hóa Đơn��#��[ ���#4CAF50$	��150��$��	20,0,20,0q��� -   $��Xóa Hóa Đơn��#��[ ���#F44336$	��150��$��	20,0,20,0q�  -   $��InvoiceDetailsListView��  .  ! . 
������ $��0q�
" �����# �#F8F9FA$ �#E0E6ED% �#E0E6ED  ��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED'   $( 	Tên Món��$) * * �� *
TenMonAn+  $( SL��$) 60 * �� *	SoLuong+  $( 
Đơn Giá��$	) 100 * �� *DonGia+$+ 	{0:N0} đ��  $( Thành Tiền��$	) 120 * �� *	ThanhTien+$+ 	{0:N0} đ��