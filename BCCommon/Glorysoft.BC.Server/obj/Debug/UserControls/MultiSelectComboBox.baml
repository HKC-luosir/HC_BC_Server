   M S B A M L   `   `   `  ����  Glorysoft.BC.Server:    4Glorysoft.BC.Server.UserControls.MultiSelectComboBox   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �� -   5          ��Name$  MultiSelectCombo��5   	    J� SnapsToDevicePixels .  � OverridesDefaultStyle . $
��Auto=�5   	   $
��Auto=�5   	   ��. $ �� IsSynchronizedWithCurrentItem$
 TrueY�5   	   ��5   
   �� 5      .R�5      �� -   5        ��5      �� *Title+ d� 	IsChecked 5      ��  �� Path$ 
IsSelected� �� Mode$ TwoWay=�
 � Tag 5      �� *FindAncestor+ �� AncestorType" ��5      5      ��5   
   �� 5      	 �� 
TargetType"	 ��6   .R�5      � .+�5      d� 
 d�Name$
 ToggleButton��5      $��2��5      |�.   �� 	ClickMode$ Press=�5      $
��Left=�6+    5   (   �� $ IsDropDownOpen�$ TwoWay=� �� RelativeSource �� *TemplatedParent+��5       �� 5!      .R�5"   "   �  � ColumnDefinitions 5#   &   �� 5$   *   $��* �6;   �� 5%   *   $��18 �6;   .+�5'   &   ��  ��Name$ Border��5(      $��2��5)       �� CornerRadius$ 2��5*      
�������
���   �$��1,1,1,1q�5-      �� 5.   &   $ 
BorderComp��5/      $��0��50      $ 2��51      $��1q�52      
�������
���   �$��0,0,0,0q�55      ._�56   *   �� 
������� �� Padding$ 3q�57   C   ��56   4   �� $
 Text� ��  �� Mode$ FindAncestor=�$ UserControlL�57   C   58   '   F� 59   &    F�Name$ Arrow��5:      $��1��5;      
���   �$��Center=�5=      $��Center=�5>      "����        � 	=  	= � z     	5?      5@   #   5A      5C      � 5D      $��Popup��5E      $��Bottom=�5F       � AllowsTransparency . |�.   �� IsDropDownOpen#��z 5H   ,   $��Slide=�5I      .��5J      � $��DropDown��5K   #    .  � ActualWidth#��z 5M       �� MaxDropDownHeight#��z 5N   #   .+�5O      �� $ DropDownBorder��5P   %   $��1q�5Q   $   
�������
���   �5R   %   �� 5S      $��4,6,4,6q�6+    .  � DataContext 6W   �� .��5T   "   ��  K� IsItemsHost . -  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation$ 	Contained=�6@   5U      5V      5W      5X       �� Triggers 5Y      P� 5Z       P� Property �@��HasItems P� Value .  . �5[      ��  �� 
TargetName$ DropDownBorder��6!    �� Property � �� �� Value$ 95��6R   5\      P� 5]        P� 
SourceName$  Popup��6    �@�AllowsTransparency . . �5^      �� $ DropDownBorder��6!    �@��CornerRadius$ 4��6U   �� 5_      $ DropDownBorder��6!    � ��$ 0,2,0,0q�6O   5`      5b      5d      5e      