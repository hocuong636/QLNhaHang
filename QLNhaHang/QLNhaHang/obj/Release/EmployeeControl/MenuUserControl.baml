   M S B A M L   `   `   `  ����   	QLNhaHang/    )QLNhaHang.EmployeeControl.MenuUserControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $
��1325�����#F0F4F8$	��790��  � 	Resources     RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  SearchBoxStyle ��Name��%C  &	        &	 �     �� " ��.���  � �� �� " ��.R��� $ 22��#��z��._��� $��Center=�$��Center=���  � ��
 �������  � ��$ Medium"���  � ��$ 40���� " ��.���  � �� �� " ��.R��� $ 20��
����������#E0E6ED$��1q�._��� $ PART_ContentHost��$
��10,0q�.��� $��0,0,0,15q� � RowDefinitions �� $��80 ��� $��* �.+��� ���#1976D2$��80��$	��Top=�$��0,0,0,0q�._��� $
��MENU��$��28(�$
��Bold"�
�������$��Center=�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   Color$	 	#40000000��
   	Direction$	
 320Q�   ShadowDepth$ 3Q�   
BlurRadius$ 4Q�� $��1��$��20q� � ColumnDefinitions �� $��* ��� $	��420 �.+��� 
�������$ 15��$��	10,0,20,0q�   $	 	#20000000��$ 15Q�$ 2Q�$	
 315Q�._�]� .+��� $	��Top=�
������ $	��791��$��45��$��20,20,20,10q�   $	 	#20000000��$ 10Q�$ 1Q�._��  �� $	��380 ��� $	��100 ��� $	��100 �.+��� -    ��Name$ SearchTextBox��$��0��#��[ $��0q�$��Center=�
������ $��14(�$��	0,-3,0,-2q�$
��Left=�$	��365���� -   $
��Tìm��$��1��$��80��#��[  ���#4CAF50$��Center=�$��0,0,0,0q��� -   $��
Làm mới��$��2��$��80��#��[  ���#FF9800$��Center=�$��0,0,0,0q��� -   $ ScrollViewer��$
��Auto=�$��20,10,20,20q�.���� -    ��Name$ MenuItemsControl��$	��840������ .R�� ���� .R��� $��1q����#E0E6ED$��5q�$ 10��
�������   $	 	#20000000��$ 10Q�$ 1Q�._��� $	��180��$	��270��.+��� $ 10��$	��160��$	��160��$��10q�����  j� Stretch$ UniformToFill=� �� ImageSource �� *	HinhAnh+�� $��SemiBold"�$��14(� �� TextAlignment$ Center=�$
��10,0q����� *
TenMonAn+�� $��Medium"����#1976D2$ Center=�$��5q����� *Gia+ �� StringFormat$ {0:N0} VNĐ���� -   $��Đặt món��$	��120��#��[  ���#4CAF50$��5,0,5,0q�
 � Tag �� �� $��1��
�������$ 15��$��0,0,10,0q�   $	 	#20000000��$ 15Q�$ 2Q�$	
 315Q�._�]� $��20q�.+��� $��Giỏ hàng��$	��Top=�$��20(�$��SemiBold"����#1976D2$��0,0,0,15q��� $��Bottom=�.+��� ���#F8F9FA$ 10�� �� Padding$ 15q�$��0,0,0,15q�._��� .+���  �� Orientation$ 
Horizontal=�$��0,0,0,10q�.+��� $��Chọn bàn:��$��Center=�$��Medium"��� -   $��TableComboBox��$��80��$��10,0,0,0q�$��5q��� -   $��TotalTextBlock��$��Tổng tiền: 0 VNĐ��$
��Bold"�$��16(����#1976D2�� $ 
Horizontal=�$��Center=�.+��� -	   $
��Xóa��$��76��#��[  ���#F44336$��0,0,10,0q�$��35���� -
   $��Báo chế biến��$	��128��#��[  ���#FF9800$��33���� -   $��OrderListView��$��1q����#E0E6ED
������  �� View �� .P��� $��	Tên món�� �� Width$	 200�� �� DisplayMemberBinding �� *
TenMonAn+�� $��SL��$ 60�� �� *	SoLuong+�� $
��Giá��$	 120�� �� *	ThanhTien+