   M S B A M L   `   `   `  ����G  CImageSection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ImageSectionImageSection  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       ImageSection.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ImageSection     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5      � -   5         �Name$ grid1��5      .+�5   
   �� $
��Left=�6   $��	31,22,0,0q�6/   $
��Wrap=�6B   $&�� Ширина фрагмента:��6V   $	��Top=�6o   �� 5   
   $
��Left=�6   $��	31,58,0,0q�6/   $
��Wrap=�6B   $&�� Высота фрагмента:��6V   $	��Top=�6o   �� 5   
   $
��Left=�6   $��	31,93,0,0q�6/   $
��Wrap=�6B   $0��*Количество фрагментов:��6V   $	��Top=�6t   �� 5   
   -   5         ��Name$ width��5      $
��Left=�6!   $��23��6<   $��
185,19,0,0q�6H    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $	��120��6�   $	��150��6�   �� 5   
   -   5        $ height��5      $
��Left=�6"   $��23��6=   $��
185,53,0,0q�6I   $
 Wrap=�6]   $	��Top=�6q   $	��120��6�   $	��150��6�   �� 5   
   -   5        $ count��5      $
��Left=�6!   $��23��6<   $��
185,88,0,0q�6H   $
 Wrap=�6\   $	��Top=�6p   $	��120��6�   $��7��6�   �� 5   
   -   5         ��Name$
 but1��5      $ ��Сгенерировать��6   $
��Left=�67   $��
31,128,0,0q�6R   $	��Top=�6f   $	��167��6~   $��25��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ tableNumber��5      $
��Left=�6(   $	��228��6C   $��
19,169,0,0q�6P   $	��Top=�6d   $	��286��6|   $ �� IsSynchronizedWithCurrentItem$ FalseY�6�      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 № картинки��6E   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ CountF�6Y     5      $	 X��6E   $

 Auto 6P    6%   �� $ WidthF�6P     5      $	 Y��6F    6%   �� $ HeightF�6F   5      �� 5   
   $��325,19,9.6,23q�6   .��5      � ! �� IsDeferredScrollingEnabled . .+�5      �� -   5         ��Name$
 icon��5       %� IsVirtualizing .  %� VirtualizationMode$ 	Recycling=�5       %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$ 0 5       ��5   -   �� *PictureFragments+��5!      �� 5"      .R�5#      � 5$      ��5&      �� 5'      .R�5(      �� 
���������6%   �� *width+��6=   �� *height+._�5)   "   � .+�5*   &   �� -   5        ��5*   ,   �� *width+��6D   �� *height+5+   ,   �� 5,   &   ��60   �� *PictureNumber+5-   $   5.      5/      51      52      53      54      56      