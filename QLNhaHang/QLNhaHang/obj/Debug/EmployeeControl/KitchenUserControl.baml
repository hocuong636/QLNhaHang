   M S B A M L   `   `   `  ����  	QLNhaHang2    ,QLNhaHang.EmployeeControl.KitchenUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��10q�6     � ColumnDefinitions  5	   
   �� 5
      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $*��$Danh sách món cần thực hiện:��6   $
��Bold"�69   $��5q�6K   $��0��6V   .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgDishesToPrepare��5         AutoGenerateColumns .     CanUserAddRows .  $��5q�6c      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Tên món��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 170 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *
DishName+6W     5      $ Số lượng��6)   $	 100 6X    6;   �� *
Quantity+6X     5      $ 	Số bàn��6)   $	 100 6Y    69   �� *TableNumber+6Y   5      5      �� 5   
   $(��"Danh sách món cần phục vụ:��6   $
��Bold"�67   $��5q�6I   $��1��6T   .��5        -   5        $��dgDishesToServe��5       .   .  $��5q�6a    5         5!      $ 	Tên món��6)   $	 170 6W    6:   �� *
DishName+6W     5"      $ Số lượng��6)   $	 100 6X    6;   �� *
Quantity+6X     5#      $ 	Số bàn��6)   $	 100 6Y    69   �� *TableNumber+6Y   5%      5&      �� 5)   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��1��6K   $��2��6X   $��5q�6l   .+�5*      �� $��Đã thực hiện xong��6   $	��150��61   $��5q�6=   $��Center=�6H   �� 5+      $!��Đã phục vụ cho khách��6   $	��150��64   $��5q�6@   $��Center=�6K   5,      5-      5.      