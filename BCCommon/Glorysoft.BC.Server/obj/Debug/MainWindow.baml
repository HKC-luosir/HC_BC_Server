   M S B A M L   `   `   `  ����N  JGlorysoft.BC.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Glorysoft.BC.Server.ViewModel.ValueConverter,Glorysoft.BC.Server.ViewModel.ValueConverter  5   	   $    Glorysoft.BC.Server.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   P	Converter:clr-namespace:Glorysoft.BC.Server.ViewModel.ValueConverter     5   	   -   5          � Title$  
MainWindow��5      $	��200��6   $	��620��6+    � 
ResizeMode$ CanMinimize=�67   .��5      � $��	0,0,-6,-1q�6   ��5   
   �� 5	       �� ImageSource$9 3/Glorysoft.BC.Server;component/Image/background.png��6    j� Stretch$ UniformToFill=�5
       j� TileMode$
 None=�5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      $��50 �6   .+�5   
   �� -   5         ��Name$ txtLineName��5      $��0q�6'   $��0��6;   $��5,5,5,5q�6H   $��18(�6Y    �� 
IsReadOnly . $��Center=�6�   ��6y   Z� 6�   � 5   
   $��	0,20,0,30q�6   $��2��6"    � ColumnDefinitions 5      �� 5      $��0* �6#   �� 5      $	��55* �6#   �� 5      $��1143* �6#   5a      �� 5b   
   -   5        $��btnSecs��5b      $��Print Port Cache��6    $��2��6;   $
��Left=�6H   $��5,8,0,0q�6c   $	��104��6t   $��37��6�   6�   �� 5c   
   -   5        $
��Exit��5c      $��2��6    $	��100��6-   $��35��69   $��0,0,5,5q�6X   $��Right=�6i   $��Bottom=�6�   �� 5d   
   $y�Hidden=�6   $��CheckBox��6'   $
��Left=�6:   $	��Top=�6U   �� 5e   
   -   5        $��lblTrid��5e   $   $y�Hidden=�6   $
��TRID��63   $
��Left=�6B   $	��Top=�6]   $��	72,55,0,0q�6u   $��1��6�   �� 5f   
   -   5        $��lblDsper��5f   $   $y�Hidden=�6   $��DSPER��64   $
��Left=�6D   $	��Top=�6_   $��	72,85,0,0q�6w   $��1��6�   � 5g   
   $��1��6    5h      �� 5i      $��0* �6#   �� 5j      $	��55* �6#   �� 5k      $��1143* �6#   5�      �� 5�   
   -   5        $��	lblTotsmp��5�   $   $y�Hidden=�6   $��TOTSMP��65   $
��Left=�6F   $	��Top=�6a   $��
72,115,0,0q�6y   $��1��6�   � 5�   
   $
��0,25q�6   $��2��6   $��1��6.    5�      �� 5�      $��0* �6#   �� 5�      $	��55* �6#   �� 5�      $��1143* �6#   .+�5�      �� -   5        $��txtTrid��5�   �   $y�Hidden=�6   $
��Left=�6*   $��23��6E   	 �� TextWrapping$
	 Wrap=�6Q   $	��Top=�6e   $	��120��6}   $��2��6�   $��
101,28,0,0q�6�   6�   �� 5�      -	   5        $��txtDsper��5�   �   $y�Hidden=�6   $
��Left=�6*   $��23��6E   $
	 Wrap=�6Q   $	��Top=�6e   $	��120��6}   $��2��6�   $��
101,56,0,0q�6�   6�   �� 5�      -
   5        $��	txtTotsmp��5�   �   $y�Hidden=�6   $
��Left=�6*   $��23��6E   $
	 Wrap=�6Q   $	��Top=�6e   $	��120��6}   $��2��6�   $��
101,84,0,0q�6�   6�   �� 5�      -   5        $��	txtRepgsz��5�   �   $y�Hidden=�6   $
��Left=�6*   $��23��6E   $
	 Wrap=�6Q   $	��Top=�6e   $	��120��6}   $��2��6�   $��101,112,0,0q�6�   6�   5
     �� 5  
   -   5        $��	lblRepgsz��5  $   $y�Hidden=�6   $��REPGSZ��65   $
��Left=�6F   $	��Top=�6a   $��
72,140,0,0q�6y   $��1��6�   5     5     