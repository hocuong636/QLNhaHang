   M S B A M L   `   `   `  ����D  @QLNhaHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:QLNhaHang	QLNhaHang  5   	   ;"clr-namespace:QLNhaHang.ConvertersQLNhaHang.Converters  5   	   "    QLNhaHang.FoodManagementPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:QLNhaHang     5   	   9
converters"clr-namespace:QLNhaHang.Converters     5   	   $	��800��5	        � 	Resources  5      /   )QLNhaHang.Converters.BoolToColorConverter   BoolToColorConverter��%   &	          5   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��
Menu Admin��6   $
��Left=�6'   $	��Top=�6B   $��	10,10,0,0q�6Z   $��0��6m   .��5      �� $��
Menu Admin��6   $��24(�6*   $
��Bold"�68   $
��Left=�6J   $
��0,10q�6e   5      �� 5   
    �� Orientation$ Vertical=�6   $��
10,5,236,0q�6,   $��1��6@   .+�5      �� $��Tìm kiếm món ăn��6   $��0,0,0,10q�60   $	��541��6B   .��5      �� $ 
Horizontal=�6   $��10q�66   .+�5      �� $	��300��6   $��30��6*   $��0,0,10,0q�66   $��Center=�6H   .��6c   ��5       �� 5!       �� 
TargetType" ��6$   .�5"   "   ��  �� Property � �� �� Value 5#   &   �� 5$   *    �� 
TargetType" ��6:   .R�5%   .   � .+�5&   2   ��  ��Name$ PART_TextBox��6:   #��z��6P   �� 5'   2   $��Tìm kiếm món ăn...��6<   
�������$��Center=�5(   <   $��5,0,0,0q�6W    J� IsHitTestVisible .  y�5*   <   �� *Text+	 �� RelativeSource	 �� *TemplatedParent+
 �� 	Converter#
 [  5+   /   5,   +   5.   #   5/      .��50   )   51      �� 52      $��
Thêm Món��6   ���#2D4B73
�������$	��100��6X   $��0,0,10,0q�6d   �� 53      $��	Xóa Món��6   ���#FF5555
�������$	��100��6W   54      55      � 57       � ColumnDefinitions 58      �� 59      $��3* �6'   �� 5:      $��1* �6'   .+�5>      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     HeadersVisibility$ Column=�67   $��
0,0,0,-217q�6R   $��2��6�   $
��Left=�6�   $	��544��6�   ��6f   �� *Foods+   Columns 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$	 Mã��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Ma+6P     5A      $ 	Tên Món��6-   $ * 6V    6>   �� *Ten+6V     5B      $ 
Giá (VND)��6-   $ * 6X    6@   �� *Gia+6X     5C      $ Thời Gian Nấu (Phút)��6-   $ * 6k    6K   �� *ThoiGianNau+6k   5E      5F      5G      5H      5I      