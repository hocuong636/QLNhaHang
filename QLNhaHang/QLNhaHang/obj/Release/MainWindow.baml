   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang      QLNhaHang.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:QLNhaHang     -     � Title$  Đăng Nhập��$	��600��$	��900�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency .  � 	Resources    ModernTextBox �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� CornerRadius ��Name  ModernPasswordBox  ModernButton ��Name J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   	Direction   ShadowDepth   
BlurRadius �� Triggers P� Property P� Value �� 
TargetName  NoHoverButton��%  &	        &	 �     &	 �    &	 I    �� " ��.���  � �� �� "	 ��.R��� #��z��$��0,0,0,2q����#1976D2$
 3��._��� $ PART_ContentHost����  � �� �#F5F6F7��  � ��$
 10,8q���  � ��$ 14(��� " G�.���  � �� �� "	 G�.R��� #��z��$��0,0,0,2q����#1976D2$
 3��._��� $ PART_ContentHost����  � �� �#F5F6F7��  � ��$
 10,8q���  � ��$ 14(��� " ��.���  � �� �� "	 ��.R��� $ border��#��z��$
 25��$��0q�._��� $��Center=�$��Center=�   $ 	#40000000��$	 270Q�$ 3Q�$ 5Q� P�  �@J�IsMouseOver . . ��� $ border�� � �� �#1565C0��  �@�Cursor$
 Hand��P�  � �� . . ��� $ border�� � �� �#0D47A1��  � �� �#1976D2��  � ��
 �������  � ��$ 14(���  � ��$ SemiBold"��� " ��.���  � ��
 ���� ��  � ��
 �������  � ��$ 0q���  � �� �� "	 ��.R��� #��z��._��� $��Center=�$��Center=�.���� 
�������$
 20��$��10q�   $ 	#40000000��$	 270Q�$ 3Q�$ 15Q�._��  � ColumnDefinitions �� $��* ��� $	��400 �.+��� $
 	20,0,0,20������  �� ImageSource$+ %/8ed4ae5f7aa646bc06bfec9ff44c932e.jpg�� j� Stretch$ UniformToFill=�._��� ���	#80000000$
 	20,0,0,20��._��� $��Center=�$
��40,0q�.+��� $��Welcome Back!��
�������$��36(�$
��Bold"�$
��Left=��� $)��#Please login to access your account�����	#DDFFFFFF$��16(�$��0,10,0,0q��� -    ��Name$ HDSDBtn��$��TUTORIAL��$��30��$	��100��$��20(�$��-6,5,0,0q�#��[ $
��Left=�� $��1��$��40q�.+��� -   #��[ $��32�����#FF5252$	��Top=�$	��✕��$��	288,0,0,0q�$
��Left=�$��32���� $��Center=�$
��0,20q�.+��� $��Login��$��32(�$
��Bold"�$��0,0,0,40q����#1976D2�� $��Username�����#555555$��12(�$��0,0,0,8q��� -    ��Name$ UsernameTextBox��#��[  $��45��$��0,0,0,20q��� $��Password�����#555555$��12(�$��0,0,0,8q�G� -    G�Name$ PasswordBox��#��[ $��45��$��0,0,0,30q��� -   $ LoginBtn��#��[ $��LOGIN��$��50��$	��200���� -   $ 	ForgetBtn��$��Forget Password ?��$��20��$	��100��$��0,10,0,0q�
������ $��0q����#1976D2