   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:QLNhaHang.EmployeeControlQLNhaHang.EmployeeControl      QLNhaHang.TableStatusPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:QLNhaHang.EmployeeControl     ���#F0F4F8$
��1325��  � 	Resources     TableButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color	   
BlurRadius
   ShadowDepth �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName  ModernButtonStyle��%  &	        &	 �    �� " ��.���  � ��$	 265����  � ��$	 150����  � ��$ 8q���  � �� �� " ��.R��� $ border��$ 15��#��z��$��0q�   $ 	#20000000��$	 10Q�$
 2Q�._�� .+��� $��22(�$��SemiBold"�
�������$��Center=�$��Center=����� *	Content+ �� *TemplatedParent+ P�  �@J�IsMouseOver . . ��� $ border�� �@J�Effect   $ 	#40000000��$	 15Q�$
 3Q��� " ��.���  � ��$ 45����  � ��$ SemiBold"���  � ��
 �������  � �� �� " ��.R��� $ border��$ 22��#��z��   $ 	#20000000��$	 10Q�$
 2Q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ border�� �@J�Effect   $ 	#40000000��$	 15Q�$
 3Q�.��� $��0,0,45,0q�.+��� ���#1976D2$��80��$	��Top=�._��� $#��QUẢN LÝ TÌNH TRẠNG BÀN��$��28(�$
��Bold"�
�������$��Center=�$��Center=�   $ 	#40000000��   	Direction$	 320Q�$
 3Q�$	 4Q�� $��20,100,45,20q� � ColumnDefinitions �� $��2* ��� $��* �.+��� 
�������$ 	15,0,0,15��$��	0,0,410,0q�$��2��   $ 	#20000000��$	 15Q�$
 2Q�$	 315Q�._��� -    ��Name$ scrollViewer��$��12q�$��-5,0,0,0q�.��� .+��� -   $
��btn1��#��[  ���#4CAF50$��Bàn 1��$	��240���� -   $
��btn2��#��[  ���#4CAF50$��Bàn 2��$	��240���� -   $
��btn3��#��[  ���#4CAF50$��Bàn 3��$	��240���� -   $
��btn4��#��[  ���#4CAF50$��Bàn 4��$	��240���� -   $
��btn5��#��[  ���#4CAF50$��Bàn 5��$	��240���� -   $
��btn6��#��[  ���#4CAF50$��Bàn 6��$	��240���� -   $
��btn7��#��[  ���#4CAF50$��Bàn 7��$	��240���� -	   $
��btn8��#��[  ���#4CAF50$��Bàn 8��$	��240���� -
   $
��btn9��#��[  ���#4CAF50$��Bàn 9��$	��240���� -   $��btn10��#��[  ���#4CAF50$��Bàn 10��$	��240���� -   $��btn11��#��[  ���#4CAF50$��Bàn 11��$	��240���� -   $��btn12��#��[  ���#4CAF50$��Bàn 12��$	��240���� -   $��btn13��#��[  ���#4CAF50$��Bàn 13��$	��240���� -   $��btn14��#��[  ���#4CAF50$��Bàn 14��$	��240���� -   $��btn15��#��[  ���#4CAF50$��Bàn 15��$	��240���� -   $��btn16��#��[  ���#4CAF50$��Bàn 16��$	��240���� -   $��btn17��#��[  ���#4CAF50$��Bàn 17��$	��240���� -   $��btn18��#��[  ���#4CAF50$��Bàn 18��$	��240���� -   $��btn19��#��[  ���#4CAF50$��Bàn 19��$	��240���� -   $��btn20��#��[  ���#4CAF50$��Bàn 20��$	��240���� -   $��btn21��#��[  ���#4CAF50$��Bàn 21��$	��240���� $��1��
�������$ 15��$��	20,0,40,0q�   $ 	#20000000��$	 15Q�$
 2Q�$	 315Q�._�]� $��20q�.+��� $��Chi tiết đơn hàng��$	��Top=�$��20(�$��SemiBold"����#1976D2$��0,0,0,20q��� $	��Top=�$��0,0,0,20q�.+��� -    ��Name$ SelectedTableText��$��24(�$
��Bold"����#2C3E50�� -   $ CurrentTableText��$��16(�$��0,5,0,0q�$��Bàn: Chưa chọn�����#666�� $��Bottom=�.+��� ���#F8F9FA$ 10�� �� Padding$ 15q�$��0,0,0,20q�._�]� .+��� $��Tổng tiền:��$��18(�$��SemiBold"����#2C3E50�� -   $ TotalAmount��$��18(�$
��Bold"����#1976D2$��Right=��� -    ��Name$ PaymentButton��$��THANH TOÁN��#��[ ���#F44336�� -    ��Name$ AvailableTablesCombo��$��40��$��0,10,0,0q�$
��10,5q�$y�	Collapsed=�&   System.Windows.Controls.DataGrid  -     Name$ OrderDetailsGrid��   AutoGenerateColumns .     
IsReadOnly . 
������ $��1q����#E0E6ED   RowBackground
 �����   AlternatingRowBackground �#F8F9FA   GridLinesVisibility$	 All=�   HorizontalGridLinesBrush �#E0E6ED   VerticalGridLinesBrush �#E0E6ED$	��318�� J� RenderTransformOrigin$ 0.391,0.545&�  =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�   '        �� "  .���  � �� �#F5F7FA��  � �� �#2C3E50��  � ��$ SemiBold"���  � ��$ 10q���  � ��$ 0,0,1,1q���  � �� �#E0E6ED    Columns  0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn!   Header$! 	Tên món��"   Width5  /System.Windows.Controls.DataGridLengthConverter$	" 150 1  +System.Windows.Controls.DataGridBoundColumn#   Binding# �� *
TenMonAn+  $! Số lượng��$" 75 # �� *	SoLuong+  $! Thành tiền��$" * # �� *	ThanhTien+$ �� StringFormat$$ 	{0:N0} đ��