   M S B A M L   `   `   `  ����?  ;LAB2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LAB2LAB2  5   	       LAB2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:LAB2     5   	     � Title$  File Manager��5   	   $	��600��6   $	��400��6+   .��5	      � .+�5
   
   o� -   5         o�Name$
 menu��5
      $
��Left=�6   $��20��68   $��	10,0,10,0q�6D   $	��Top=�6W   $	��362��6o    o� 
IsMainMenu . 
�������.@�5      m� $
��File��6   $��20��6%   .@�5      m� -   5        $
��Open��5      6)   m� 5      -   5        $
��Exit��5      6)   5      5      R� 5   
   -   5         R�Name$ treeView��5      $
��Left=�6%   $	��307��6@   $��	10,30,0,0q�6M   $	��Top=�6`   $	��362��6x   ��5      �� 5       �� DataType  	LAB2.File"  6+    �� ItemsSource 6J   �� *Items+.R�5      ��  �� Orientation$ 
Horizontal=�6!    � ContextMenu 5      �� 5      -   5        .@�5   "   m� -   5        $��create��5   +   6;   m� 5   "   -   5        $��delete��5   +   6;   5      .+�5      �� ��6$   �� *Name+5      5      5      �� 5    
   -   5         ��Name$ textBox��5       $
��Left=�6#   $	��181��6>   $��
10,342,0,0q�6K   	 �� TextWrapping$
	 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��362��6�   �� 5!   
   -	   5        
 ��Name$
 label��5!      $�� ��6   $
��Left=�6*   $��
10,528,0,0q�6E   $	��Top=�6Y   $��52��6q   5#      5$      