   M S B A M L   `   `   `  ����   	QLNhaHang    QLNhaHang.AdminWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Admin Dashboard�� � WindowState$ 	Maximized=� � 
ResizeMode$ 	CanResize=����#F5F6FA � 	Resources    NavButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� CornerRadius
 �� Padding �� Triggers P� Property P� Value �� 
TargetName  ActiveNavButtonStyle �� BasedOn��%)  &	        &	 �    7   �� " ��.���  � ��$ 45����  � ��$
 15,5q���  � ��
 ���� ��  � �� �#B0B9C6��  � ��$ 14(���  � ��$
 Left=���  � �� �� " ��.R��� $ border��#��z��$	 8��$

 15,0q�._�� .+��� $��Center=� P�  �@J�IsMouseOver . . ��� $ border�� � �� �#2E374A��  � ��
 �������  �@�Cursor$
 Hand��P�  � �� . . ��� $ border�� � �� �#1E2736�� " ��8   .���  � �� �#2196F3��  � ��
 �����.���  � ColumnDefinitions �� $	��280 ��� $��* �.+��� ���#1A2035 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ 	#40000000��   
BlurRadius$ 10Q�   ShadowDepth$ 3Q�   	Direction$ 0Q�._�]� .+��� $	��Top=�$��80�����#141829$
 20q�._���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� $��Admin��
�������$��20(�$��SemiBold"�$��0,0,0,0q�$��Center=��� $	��250�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� ��  �� AngleY$ -0.364Q��� T�  T� Y$ -0.794Q�.+��� -   #��[  $��Tổng quan��$��
15,20,15,5q��� -   #��[  $��Quản lý món ăn���� -   #��[  $#��Quản lý tình trạng bàn���� -   #��[  $��Quản lý hóa đơn���� -   #��[  $��Quản lý kho���� -   #��[  $��Quản lý nhân viên���� -   #��[  $��Quản lý chấm công���� -   #��[  $��Quản Lý tài khoản���� -	   #��[  $��Đăng xuất�����#FF5252�� 
�������$	 0,0,0,0��$��	280,0,0,1q�$��2��   $ 	#20000000��$ 15Q�$ 2Q�$ 0Q�._��� -
    ��Name$ ContentArea��$��20q�