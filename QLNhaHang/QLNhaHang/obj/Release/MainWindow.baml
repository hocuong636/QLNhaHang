   M S B A M L   `   `   `  ���� D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang      QLNhaHang.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:QLNhaHang       � Title$  Đăng Nhập��$	��450��$	��710��.���  J� RenderTransformOrigin$ 0.5,1&�$	��440��$	��Top=�.+��� $	��318��
������� �� CornerRadius$ 6��$��0,10,0,0q�$��Center=�$ 
0.65,0.518&�$	��402��$	��Top=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$	 -90Q�   RenderingBias$ Quality=�   ShadowDepth$ 2Q�   Color$
 Gray��._��� 	 �� Orientation$	 Vertical=�$��20q�.+��� $��Login��$��25(�$��Center=�$	��Top=�$
��Bold"�
 �� Padding$
 10q�� $	��462�� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� $��Tên đăng nhập��$��0��$
��0,15q�$
��Left=��� -    ��Name$ UsernameTextBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=�$��	0,36,0,10q�$��2���� $��Mật khẩu��$��2��$
��0,15q�$
��Left=�G� -    G�Name$ PasswordBox��$��2��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=�$��	0,36,0,62q�$��2���� -    ��Name$ LoginBtn��$��Login��
�������
���   �$
��0,25q�$��35��$	��250��$ 
0.504,0.97&�����  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R���  ��Name$ LoginBorder��#��z��$ 16��._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ LoginBorder�� � ��
 ������� $ LoginBorder�� � ��
 �   �