   M S B A M L   `   `   `  ����@  <UIbot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UIbotUIbot  5   	       UIbot.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:UIbot     5   	     � Title$  
MyBot v1.0��5   	   $	��400��5	   	   $	��700��6   $	��450��5
   	   $	��800��6    � 	Resources 5         resourceImage��%M   &	        �� 5   
   $/��)0-7104_paper-clip-icon-file-work-icon.png��6'   5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��210* �6   �� 5      $
��Auto �6   �� 5      $
��191* �6   .+�5   
   �� $��1��6   $��1��5      $��3��5      $��Stretch=�5      $��Stretch=�65   
���   �5      �� 5    
   -   5         ��Name$ logList��5       $��0��5!      $��Stretch=�5"      $��10,5,5,5q�5#      $��Stretch=�5$      ��5&      �� 5'      .R�5(      �� 
���   �$��5q�61   $��1q�6<    �� CornerRadius$ 10��6P   ._�5)      �� $��10q�6%   .+�5*      �� $��0q�6C   $��15(�6N   ��6(   �� *	FirstName+6N   �� 5+      $��0q�6=   $��20(�6H   ��6(   �� *Msg+6H   �� 5,      $
��10 0q�6>   $��10(�6L   
������� �� TextAlignment$ Right=�6l   ��6(   �� *Time+6l   5-      5.      5/      51      �� 53   
   $��2��6   $��20(�54      $)��#Отправка сообщения��55      $��Stretch=�56      $��5,2,5,5q�57      $��Stretch=�58      .��5:      �� .+�5;      �� -   5         ��Name$ 
TargetSend��5;      $y�	Collapsed=�5=      $��5q�5>      ��5<      ��  �� ElementName$ logList��	 �� Path$	 SelectedItem.Id�5>      �� 5@      -   5        
 ��Name$
 
txtMsgSend��5@      $��20(�6.   $��5q�6<   �� 5B      -   5         ��Name$ 
btnMsgSend��5B      $��20(�6-   $+��%Отправить сообщение��6;   $��5q�6Y   6d   �� 5C      -   5        $ btnFileSend��5C      #��[  6/   $��5,5,0,5q�5D      $��34��6.   $
��Left=�6:   $��54��6U   5E      5F      5G      5H      